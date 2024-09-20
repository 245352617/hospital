#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/publish/IMService/*

if [ ! -d "$ROOT/publish/IMService" ];then
  mkdir -p $ROOT/publish/IMService
fi
docker image prune -f

image=192.168.1.162:5000/ecis-im/ecis-im:dev

echo "======== 生成发布包 ========"

export PATH=$HOME/.dotnet/tools:$PATH
export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64

dotnet sonarscanner begin /k:"ECISIMService" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet publish -c Release -o $ROOT/publish/IMService $ROOT/YiJian.ECIS.IMService.HttpApi.Host.csproj

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

cp $ROOT/Dockerfile $ROOT/publish/IMService/Dockerfile

docker build -t $image $ROOT/publish/IMService

#docker run -it -p 192.168.1.162:57504:80  192.168.1.162:5000/ecisIMService/ecisIMService:dev --name ecis-IMService -rm -v /data/configs/ecis2.0/appsettings.IMService.json:/app/appsettings.json

#docker service rm ecis-IMService
#docker service create -d --name ecis-IMService --network office -p 57503:80 --mount type=bind,src=/data/configs/ecis2.0/appsettings.IMService.json,dst=/app/appsettings.json 192.168.1.162:5000/ecisIMService/ecisIMService:dev

echo "======== 发布容器镜像 ========"
docker push $image
