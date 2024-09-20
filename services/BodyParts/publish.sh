cd ICIS.WebAPI/

echo "-------------------- 发布中 --------------------"

dotnet publish -c Release -o bin/Publish

cp Dockerfile bin/
cp localtime bin/
cd bin/Publish/

rm -f *.pdb

rm -f appsettings.Development.json

cd ..

docker build -t registry.apps.dev.szyjian.com/icis-server .

echo "-------------------- 生在中 --------------------"

docker push registry.apps.dev.szyjian.com/icis-server
cd ../../
kubectl delete -f service.yml

docker ps -a | grep "Exited" | awk '{print $1 }'|xargs docker stop

docker ps -a | grep "Exited" | awk '{print $1 }'|xargs docker rm

docker images|grep none|awk '{print $3 }'|xargs docker rmi

kubectl create -f service.yml

echo "-------------------- 操作完成 --------------------" 
