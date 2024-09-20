#!/usr/bin/env sh

# 第一个参数，服务名称: triage/patient/recipe/report/nursing/emr/dcwriter/handover/call/bodyparts
SERVICE=""
B_SAVE_IMAGE=n
IMAGE_SAVE_PATH="${HOME}/images"

main() {
	# 处理输入
	if [ -z $SERVICE ];
	then
		set_service_by_no
		set_bsave_by_choose
	fi
	# build IMAGE
	ROOT=$(cd "$(dirname $0)";pwd)
	IMAGE=""
	DOCKERFILE=""
	build_image $SERVICE
	if [ $B_SAVE_IMAGE = "Y" ]
	then
		echo "======== 保存镜像文件 ========"
		save_image
	else
		echo "======== 不保存镜像文件 ========"
	fi
}

set_service_by_no() {
	read -p "请选择需要构建的服务(1. triage; 2. masterdata; 3. patient; 4. recipe; 5. report; 6.emr; 7.nursing; 8.handover; 9.dcwriter; 10. call; 11. bodyparts; 12. report-base)：" CHOOSE_NO
	case $CHOOSE_NO in
		1)
			SERVICE="triage"
			;;
		2)
			SERVICE="masterdata"
			;;
		3)
			SERVICE="patient"
			;;
		4)
			SERVICE="recipe"
			;;
		5)
			SERVICE="report"
			;;
		6)
			SERVICE="emr"
			;;
		7)
			SERVICE="nursing"
			;;
		8)
			SERVICE="handover"
			;;
		9)
			SERVICE="dcwriter"
			;;
		10)
			SERVICE="call"
			;;
		11)
			SERVICE="bodyparts"
			;;
		12)
			SERVICE="report.base"
			;;
		"")
			set_service_by_no
	esac
}

set_bsave_by_choose() {
	read -p "是否保存镜像(Y/n)：" CHOOSE_SAVE
	case $CHOOSE_SAVE in
		"y" | "Y")
			B_SAVE_IMAGE=Y
			;;
		"n" | "N")
			B_SAVE_IMAGE=n
			;;
		# 默认保存
		"")
			B_SAVE_IMAGE=Y
			;;
	esac
}

build_image() {
	switch_module $moduleName
	if [ -z "$IMAGE" ];
	then
		echo "脚本执行错误，镜像名称为空"
		exit;
	fi
	
	echo "======== 生成发布包 ========"
	
	docker build -f $ROOT/$DOCKERFILE -t $IMAGE $ROOT
	
	# echo "======== 发布容器镜像 ========"
	# docker push $IMAGE
}

save_image() {
	if [ ! -d "${IMAGE_SAVE_PATH}" ]
	then
		mkdir -p "${IMAGE_SAVE_PATH}"
	fi
	echo "======== 文件路径：${IMAGE_SAVE_PATH}/ecis-${SERVICE}.tar.gz ========"
	docker save ${IMAGE} | gzip > "${IMAGE_SAVE_PATH}/ecis-${SERVICE}.tar.gz"
}

switch_module() {
	case $SERVICE in
		triage)
			IMAGE=registry.szyjian.com/ecis2.0/triage:pku
			DOCKERFILE="Dockerfile.Triage"
			;;
		masterdata)
			IMAGE=registry.szyjian.com/ecis2.0/masterdata:pku
			DOCKERFILE="Dockerfile.MasterData"
			;;
		patient)
			IMAGE=registry.szyjian.com/ecis2.0/patient:pku
			DOCKERFILE="Dockerfile.Patient"
			;;
		recipe)
			IMAGE=registry.szyjian.com/ecis2.0/recipe:pku
			DOCKERFILE="Dockerfile.Recipe"
			;;
		report)
			IMAGE=registry.szyjian.com/ecis2.0/report:pku
			DOCKERFILE="Dockerfile.Report"
			;;
		emr)
			IMAGE=registry.szyjian.com/ecis2.0/emr:pku
			DOCKERFILE="Dockerfile.EMR"
			;;
		nursing)
			IMAGE=registry.szyjian.com/ecis2.0/nursing:pku
			DOCKERFILE="Dockerfile.Nursing"
			;;
		handover)
			IMAGE=registry.szyjian.com/ecis2.0/handover:pku
			DOCKERFILE="Dockerfile.Handover"
			;;
		dcwriter)
			IMAGE=registry.szyjian.com/ecis2.0/dcwriter:pku
			DOCKERFILE="Dockerfile.DCWriter"
			;;
		call)
			IMAGE=registry.szyjian.com/ecis2.0/call:pku
			DOCKERFILE="Dockerfile.Call"
			;;
		bodyparts)
			IMAGE=registry.szyjian.com/ecis2.0/bodyparts:pku
			DOCKERFILE="Dockerfile.BodyParts"
			;;
		report-base)
			IMAGE=registry.szyjian.com/ecis2.0/report-base:dev
			DOCKERFILE="Dockerfile.Report.Base"
			;;
		*)
			echo "服务不存在"
			exit 1
	esac
}

get_help(){
	echo "usage"
	echo "  Options:"
	echo "    -h  --help    帮助"
	echo "        --save    保存文件"
	echo "  Services:"
	echo "    triage        预检分诊"
	echo "    masterdata    字典服务"
	echo "    patient       诊疗服务"
	echo "    recipe        医嘱服务"
	echo "    report        报表服务"
	echo "    report.base   报表基础镜像"
	echo "    emr           电子病历服务"
	echo "    nursing       护理服务"
	echo "    handover      交接班服务"
	echo "    dcwriter      都昌电子病历"
	echo "    call          叫号服务"
	echo "    bodyparts     皮肤服务"
	echo "  Example:"
	echo "    更新医嘱服务:"
	echo "      ./build.sh recipe"
	echo "    更新医嘱服务并保存文件:"
	echo "      ./build.sh recipe --save"
}

# if [ $# -lt 1 ]; 
# then
#	 get_help
#	 exit 0
# fi

# 获取shell脚本参数
while [ $# -gt 0 ]; do
	case "$1" in
		-h | --help)
			get_help
			exit 0
		;;
		--save)
			B_SAVE_IMAGE=Y
		;;
		# 服务名称
		triage | masterdata | patient | recipe | report | emr | nursing | handover | dcwriter | call | bodyparts | report.base)
			SERVICE=$1
		;;
		*)
			printf "unknow argument: $1\n"
			exit 1
	esac
	shift
done

main
