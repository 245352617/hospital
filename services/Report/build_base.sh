#!/usr/bin/env sh
ROOT=$(cd "$(dirname $0)";pwd)
echo "当前工作目录: $ROOT"

image=192.168.1.162:5000/ecisreport/ecis-report-base:dev
docker build -t $image $ROOT/DockerBase

echo "======== 发布容器镜像 ========"
docker push $image
