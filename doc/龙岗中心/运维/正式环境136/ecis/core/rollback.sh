compose_file="/data/docker/docker-compose.infra.yml"
image_tag="dev"
image_masterdata="192.168.1.162:5000/ecis2.0/szyj-masterdata"
image_triage="192.168.1.162:5000/prehospital/ptriagesrv"
image_patient="192.168.1.162:5000/ecis2.0/szyj-patient"
image_recipe="192.168.1.162:5000/ecis2.0/szyj-recipe"
image_report="192.168.1.162:5000/ecisreport/ecis-report"
image_emr="192.168.1.162:5000/ecisemr/ecisemr"
image_dcwriter="192.168.1.162:5000/ecisdcwriter/ecisdcwriter"
service=$1
image_name=""


rollback() {
    service=$1
    echo 1. 回滚镜像文件
    docker tag "${image_name}:bak" "${image_name}:${image_tag}"
    echo 2. 回滚服务
        docker-compose -f ${compose_file} up -d --build --force-recreate "ecis-${service}" 2>/dev/null
    echo 3. 回滚完成
}


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


rollback $service
