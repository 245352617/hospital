#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/bin/publish/*

if [ ! -d "$ROOT/bin/publish" ];then
  mkdir -p $ROOT/bin/publish
fi
docker image prune -f

image=registry.szyjian.com/ecis2.0/call:pku

echo "======== 生成发布包 ========"

export PATH=$HOME/.dotnet/tools:$PATH

#dotnet sonarscanner begin /k:"ECISCALL" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet publish -c Release -o $ROOT/bin/publish $ROOT/YiJian.ECIS.Call.HttpApi.Host.csproj

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

cp $ROOT/Dockerfile $ROOT/bin/publish/Dockerfile

docker build -t $image $ROOT/bin/publish

echo "======== 发布容器镜像 ========"
docker save registry.szyjian.com/ecis2.0/call:pku | gzip > /root/data/ecis-call.tar.gz
docker push $image
