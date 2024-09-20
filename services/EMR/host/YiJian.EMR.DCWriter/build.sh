#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/bin/publish/DCWriter/*

if [ ! -d "$ROOT/bin/publish/DCWriter" ];then
  mkdir -p $ROOT/bin/publish/DCWriter
fi
docker image prune -f

image=registry.szyjian.com/ecis2.0/dcwriter:pku

echo "======== 生成发布包 ========"

export PATH=$HOME/.dotnet/tools:$PATH
export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64

#dotnet sonarscanner begin /k:"DCWriter" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet publish -c Release -o $ROOT/bin/publish/DCWriter $ROOT/YiJian.EMR.DCWriter.csproj

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

#dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

cp $ROOT/Dockerfile $ROOT/bin/publish/DCWriter/Dockerfile

cd $ROOT/bin/publish/DCWriter
echo 'dotnet 版本'
dotnet --version
echo '查阅目录文件' 
ls -ahl

docker build -t $image $ROOT/bin/publish/DCWriter

echo "======== 发布容器镜像 ========"
docker push $image
