#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/publish/TriageService/*

if [ ! -d "$ROOT/publish/TriageService" ];then
  mkdir -p $ROOT/publish/TriageService
fi
docker image prune -f

image=registry.szyjian.com/ecis2.0/triage:pku

echo "======== 生成发布包 ========"

# export PATH=$HOME/.dotnet/tools:$PATH
# export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64
# 
# dotnet sonarscanner begin /k:"PreHospital-Aggregate" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet publish -o $ROOT/publish/TriageService $ROOT/TriageService.csproj

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

#dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

cp $ROOT/Dockerfile $ROOT/publish/TriageService/Dockerfile

docker build -t $image $ROOT/publish/TriageService 
docker save $image | gzip > /data/images/ecis/triagesrv.tar.gz
echo "======== 发布容器镜像 ========"
docker push $image

#chmod 755 ./SamJan.MicroService.PreHospital/src/SamJan.MicroService.PreHospital.AggregateService/build.sh
#./SamJan.MicroService.PreHospital/src/SamJan.MicroService.PreHospital.AggregateService/build.sh

#docker service rm prehospital-aggregate
#docker service create -d --name prehospital-aggregate --network office -p 59001:80 \
#--mount type=bind,src=/data/configs/prehospital/appsettings.Aggregate.json,dst=/app/appsettings.Docker.json \
#--env ASPNETCORE_ENVIRONMENT=Docker 192.168.1.162:5000/prehospital/paggregatesrv:dev
