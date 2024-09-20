pipeline {
    //代理，通常是一个机器或容器
    agent any
    
    //环境变量，类似全局变量
    environment {
        //构建执行者
        BUILD_USER = ""
        TAG = 'dev'
        REGISTRY = '192.168.1.162:5000'
        IMAGE_SAVE_PATH = '${HOME}/images/'
    }
    
    parameters {
        choice(
          description: '你需要选择哪个分支进行构建?',
          name: 'branchName',
          choices: ['sprint/v2.2.9.8', 'main/longgang', "feature/docker"]
        )
        choice(
          description: '你需要选择哪个模块进行更新?',
          name: 'moduleName',
          choices: ['triage', 'patient', 'masterdata', 'report', 'recipe', 'nursing', 'emr', 'dcwriter', 'call', 'handover', 'bodyparts', 'job']
        )
        booleanParam(name: 'autoSave', defaultValue: true, description: '是否保存镜像文件(文件保存在~/images/)?') 
    }
 
    stages {
        stage('Git Checkout') {
            steps {
                script {
                    git branch: "$branchName",
                        credentialsId: 'aliyun',
                        url: 'git@codeup.aliyun.com:szyjian/cis/ecis.git',
                        changelog: true
                }
            }
        }
        stage("Get Module's Building Argument") {
            steps {
                script {
                    switch("$moduleName") {
                        case "triage":
                        env.DOCKERFILE = "Dockerfile.Triage"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/triage"
                        break;
                        case "patient":
                        env.DOCKERFILE = "Dockerfile.Patient"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/szyj-patient"
                        break;
                        case "masterdata":
                        env.DOCKERFILE = "Dockerfile.MasterData"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/szyj-masterdata"
                        break;
                        case "report":
                        env.DOCKERFILE = "Dockerfile.Report"
                        env.IMAGE_NAME = "$REGISTRY/ecisreport/ecis-report"
                        break;
                        case "recipe":
                        env.DOCKERFILE = "Dockerfile.Recipe"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/szyj-recipe"
                        break;
                        case "nursing":
                        env.DOCKERFILE = "Dockerfile.Nursing"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/szyj-nursing"
                        break;
                        case "emr":
                        env.DOCKERFILE = "Dockerfile.EMR"
                        env.IMAGE_NAME = "$REGISTRY/ecisemr/ecisemr"
                        break;
                        case "dcwriter":
                        env.DOCKERFILE = "Dockerfile.EMR"
                        env.IMAGE_NAME = "$REGISTRY/ecisdcwriter/ecisdcwriter"
                        break;
                        case "call":
                        env.DOCKERFILE = "Dockerfile.Call"
                        env.IMAGE_NAME = "$REGISTRY/eciscall/eciscall"
                        break;
                        case "handover":
                        env.DOCKERFILE = "Dockerfile.Handover"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/szyj-handover"
                        break;
                        case "bodyparts":
                        env.DOCKERFILE = "Dockerfile.BodyParts"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/szyj-bodyparts"
                        break;
                        case "job":
                        env.DOCKERFILE = "Dockerfile.Job"
                        env.IMAGE_NAME = "$REGISTRY/ecis2.0/szyj-job"
                        break;
                    }
                }
                echo "当前构建的Dockerfile路径是：$env.DOCKERFILE"
            }
        }
        stage('Build') {
            steps {
                script {
                    sh "docker build -f ${env.DOCKERFILE} -t ${env.IMAGE_NAME}:${env.TAG} ."
                }
            }
        }
        stage('Save') {
            steps {
                script {
                    sh """
                    if [ ${env.autoSave} = true ]
                    then
                        if [ ! -d ${IMAGE_SAVE_PATH} ]
                        then
                            mkdir -p ${IMAGE_SAVE_PATH}
                        fi
                        docker save ${env.IMAGE_NAME}:${env.TAG} | gzip > ${IMAGE_SAVE_PATH}/ecis-${moduleName}.tar.gz
                    else
                        echo '不保存文件'
                    fi
                    """
                }
            }
        }
    }
}
