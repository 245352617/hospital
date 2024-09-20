#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"
echo "======== 清理构建历史文件 ========"
rm -rf $ROOT/publish/TriageService/*

if [ ! -d "$ROOT/publish/TriageService" ];then
  mkdir -p $ROOT/publish/TriageService
fi

echo "======== 生成发布包 ========"

# export PATH=$HOME/.dotnet/tools:$PATH
# export JAVA_HOME=/usr/lib/jvm/java-11-openjdk-amd64
# 
dotnet sonarscanner begin /k:"ecis-triage" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"

dotnet build $ROOT/services/TriageService/TriageService.csproj

if [ $? -ne 0 ];then
  echo "构建发布包失败"
  exit 1
fi

dotnet sonarscanner end /d:sonar.login="7ac199c604c46c29c3e78c724d8106ad1751df0f"
