#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/publish/MyProjectName/*

if [ ! -d "$ROOT/publish/MyProjectName" ];then
  mkdir -p $ROOT/publish/MyProjectName
fi
docker image prune -f

image=192.168.1.162:5000/ecisMyProjectName/ecisMyProjectName:dev

echo "======== 生成发布包 ========"

export PATH=$HOME/.dotnet/tools:$PATH
export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64

dotnet sonarscanner begin /k:"ECISMyProjectName" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet publish -c Release -o $ROOT/publish/MyProjectName $ROOT/MyCompanyName.MyProjectName.HttpApi.Host.csproj

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

cp $ROOT/Dockerfile $ROOT/publish/MyProjectName/Dockerfile

docker build -t $image $ROOT/publish/MyProjectName

#docker run -it -p 192.168.1.162:57504:80  192.168.1.162:5000/ecisMyProjectName/ecisMyProjectName:dev --name ecis-MyProjectName -rm -v /data/configs/ecis2.0/appsettings.MyProjectName.json:/app/appsettings.json

#docker service rm ecis-MyProjectName
#docker service create -d --name ecis-MyProjectName --network office -p 57503:80 --mount type=bind,src=/data/configs/ecis2.0/appsettings.MyProjectName.json,dst=/app/appsettings.json 192.168.1.162:5000/ecisMyProjectName/ecisMyProjectName:dev

echo "======== 发布容器镜像 ========"
docker push $image
