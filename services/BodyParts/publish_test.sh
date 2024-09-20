#!/bin/bash
cd ICIS.WebAPI/
echo "-------------------- 发布中 --------------------"
dotnet publish -c Release -o bin/Publish
cp Dockerfile bin/
cp localtime bin/
cd bin/Publish/
rm -f *.pdb
rm -f appsettings.Development.json
docker build -t registry.apps.dev.szyjian.com/icis-server:test .
echo "-------------------- 生在中 --------------------"
#docker push registry.apps.dev.szyjian.com/icis-server:test
#docker images|grep none|awk '{print $3 }'|xargs docker rmi
echo "-------------------- 操作完成 --------------------" 
