#!/bin/bash

command="${1}"  # 命令
service="${@: -1}"  # 服务
is_part=false  # 是否增量更新
version=""

get_help(){
    echo "usage"
    echo "  Options:"
    echo "      -h  --help       帮助"
    echo "      -v  --version    版本号"
    echo "          --part       增量更新"
    echo "  Commands:"
    echo "      update          版本更新"
    echo "      rollback        版本回滚"
    echo "  Services:"
    echo "      triage          预检分诊"
    echo "      masterdata      字典服务"
    echo "      patient         诊疗服务"
    echo "      recipe          医嘱服务"
    echo "      report          报表服务"
    echo "      emr             电子病历服务"
    echo "      nursing         护理服务"
    echo "      handover        交接班服务"
    echo "      dcwriter        都昌电子病历"
    echo "      call            叫号服务"
    echo "  Example:"
    echo "      更新医嘱服务:"
    echo "          ./ecis.sh update --version=20220728 recipe"
    echo "      回滚医嘱更新:"
    echo "          ./ecis.sh rollback recipe"
}

# 获取shell脚本参数
while [ $# -gt 0 ]; do
    case "$1" in
        -h | --help)
            get_help

            exit 0
        ;;
        --part)
            is_part=true
        ;;
        -v=* | --version=*)
            version="${1#*=}"
        ;;
        # 命令
        update | rollback)
        ;;
        # 服务名称
        triage | masterdata | patient | recipe | report | emr | nursing | handover | dcwriter | call)
        ;;
        *)
            printf "unknow argument: $1\n"
            exit 1
    esac
    shift
done

case $command in
    update)
        ./core/update.sh $service $version
        ;;
    rollback)
        ./core/rollback.sh $service
        ;;
    *)
        printf "unknow action"
        exit 1
esac
# printf "command: $command\n"
# printf "service: $service\n"
# printf "is_part: $is_part\n"
# printf "version: $version\n"

