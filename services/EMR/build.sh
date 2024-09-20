#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/publish/EMR/*

if [ ! -d "$ROOT/publish/EMR" ];then
  mkdir -p $ROOT/publish/EMR
fi
docker image prune -f

image=registry.szyjian.com/ecis2.0/emr:pku

echo "======== 生成发布包 ========"

export PATH=$HOME/.dotnet/tools:$PATH
export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64

#dotnet sonarscanner begin /k:"ECISEMR" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet publish -c Release -o $ROOT/publish/EMR $ROOT/host/YiJian.EMR.HttpApi.Host/YiJian.EMR.HttpApi.Host.csproj

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

#dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

cp $ROOT/host/YiJian.EMR.HttpApi.Host/Dockerfile $ROOT/publish/EMR/Dockerfile

cd $ROOT/publish/EMR
echo 'dotnet 版本'
dotnet --version
echo '查阅目录文件' 
ls -ahl

docker build -t $image $ROOT/publish/EMR

echo "======== 发布容器镜像 ========"
docker save registry.szyjian.com/ecis2.0/emr:pku | gzip > /root/data/ecis-emr.tar.gz
docker push $image
