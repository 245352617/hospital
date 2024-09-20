#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/publish/Report/*

if [ ! -d "$ROOT/publish/Report" ];then
  mkdir -p $ROOT/publish/Report
fi
docker image prune -f

image=registry.szyjian.com/ecis2.0/report:pku

echo "======== 生成发布包 ========"

export PATH=$HOME/.dotnet/tools:$PATH
export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64

# dotnet sonarscanner begin /k:"ECISReport" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet publish -c Release -o $ROOT/publish/Report $ROOT/host/YiJian.Health.Report.HttpApi.Host/YiJian.Health.Report.HttpApi.Host.csproj
# 复制 FastReport 逆向版本（去除水印、打印5页限制）
cp $ROOT/dll/FastReport.dll $ROOT/publish/Report

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

#dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

cp $ROOT/host/YiJian.Health.Report.HttpApi.Host/Dockerfile $ROOT/publish/Report/Dockerfile

cd $ROOT/publish/Report
echo 'dotnet 版本'
dotnet --version
echo '查阅目录文件' 
ls -ahl

docker build -t $image $ROOT/publish/Report

echo "======== 发布容器镜像 ========"
docker save registry.szyjian.com/ecis2.0/report:pku | gzip > /root/data/ecis-report.tar.gz
docker push $image
