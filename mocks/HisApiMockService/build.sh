#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/publish/*

if [ ! -d "$ROOT/publish" ];then
  mkdir -p $ROOT/publish
fi
docker image prune -f

image=192.168.1.162:5000/ecis2.0/ecis-mock:dev

echo "======== 生成发布包 ========"

export PATH=$HOME/.dotnet/tools:$PATH
export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64

#dotnet sonarscanner begin /k:"Recipe" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"
echo "环境变量配置完成"

echo "${ROOT}" 
ls -alh $ROOT

dotnet publish -c Release -o $ROOT/publish $ROOT/HisApiMockService.csproj

echo "Release path"
ls -alh $ROOT/publish

echo "项目打包完成"

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

#dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

cp $ROOT/Dockerfile $ROOT/publish/Dockerfile

docker build -t $image $ROOT/publish

echo "======== 发布容器镜像 ========"
echo $image
docker push $image
