compose_file="/data/docker/docker-compose.yml"
base_path="/data/package/ecis/"
image_tag="dev"
image_masterdata="192.168.1.162:5000/ecis2.0/szyj-masterdata"
image_triage="192.168.1.162:5000/prehospital/ptriagesrv"
image_patient="192.168.1.162:5000/ecis2.0/szyj-patient"
image_recipe="192.168.1.162:5000/ecis2.0/szyj-recipe"
image_report="192.168.1.162:5000/ecisreport/ecis-report"
image_emr="192.168.1.162:5000/ecisemr/ecisemr"
image_dcwriter="192.168.1.162:5000/ecisdcwriter/ecisdcwriter"

update_full() {
    # 容器名称
    contianer_name="ecis-${service}"
    # 镜像名称
    image_full_name="${image_name}:${image_tag}"
    # 备份的镜像名称
    image_backup_name="${image_name}:bak"
    # 备份镜像的临时备份
    image_backup_backup_name="${image_backup_name}-bak"
    # 镜像包路径
    image_path="${base_path}/${version}/ecis-${service}.tar.gz"

    echo 1. 备份旧版本
    OLDIMAGEID=$(docker inspect --format="{{.Image}}" $contianer_name)
    docker tag $image_backup_name $image_backup_backup_name
    docker tag $image_full_name $image_backup_name
    # echo 2. 下载更新包
    # rm "${image_path}" 1>/dev/null 2>/dev/null
    # wget -P ${image_path} http://192.168.241.101:3003/ecis/${service}.tar.gz 2>/dev/null
    echo 3. 载入新版镜像包
    docker load < ${image_path} >/dev/null
    NEWIMAGEID=$(docker image inspect --format="{{.Id}}" ${image_full_name})
    if [ "$OLDIMAGEID" == "$NEWIMAGEID" ];
    then
        docker tag $image_backup_backup_name $image_backup_name
        docker rmi $image_backup_backup_name 1>/dev/null 2>/dev/null
        echo 镜像文件与旧版相同，更新失败
        return 0
    fi
    echo 4. 更新docker容器
    docker rm -f ${contianer_name} 1>/dev/null 2>/dev/null
    docker-compose -f ${compose_file} up -d ${contianer_name} 1>/dev/null 2>/dev/null
    echo 5. 更新完成
    echo 6. 检查服务是否正常启动
    RUNNING=$(docker inspect --format="{{.State.Running}}" ${contianer_name}) 2>/dev/null
    if [ "$RUNNING" == "true" ];
    then
        echo "  服务启动成功"
    else
        echo "  服务启动失败"
    fi
}

service=$1
version=$2
image_name=""
case $service in
    masterdata)
        image_name="$image_masterdata"
        ;;
    triage)
        image_name="$image_triage"
        ;;
    patient)
        image_name="$image_patient"
        ;;
    recipe)
        image_name="$image_recipe"
        ;;
    report)
        image_name="$image_report"
        ;;
    emr)
        image_name="$image_emr"
        ;;
    dcwriter)
        image_name="$image_dcwriter"
        ;;
    *)
        echo "服务不存在"
        exit 1
esac

# 全量更新
update_full $service $version
