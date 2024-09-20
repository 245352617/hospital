Write-Output "===================================================================="
Write-Output "=                        院前急救-分诊服务打包发布         "
Write-Output "===================================================================="
$OLD = Get-Location
$ROOT = Split-Path -Parent $MyInvocation.MyCommand.Definition
$REGISTRY = '192.168.1.162:5000/ecis'
$TAG = 'dev'
$PUBLISH_DIR = "$ROOT\publish"
if ($args.Count -ge 1) {
    $REGISTRY = $args[0]
}
if ($args.Count -ge 2) {
    $TAG = $args[1]
}

function Quit($exitCode) {
    Write-Output "清理无用镜像"
    wsl -u root docker image prune -f
    Set-Location -Path $OLD -PassThru
    exit $exitCode
}

Write-Output "当前工作目录： $ROOT"
if (-not (Test-Path -Path $PUBLISH_DIR)) {
    mkdir $PUBLISH_DIR
}
else {
    Write-Output "清理历史文件"
    Get-ChildItem $PUBLISH_DIR -Include * -Recurse | Remove-Item -Recurse
}

Write-Output "代码编译"
dotnet publish -c Release -o "$PUBLISH_DIR" "${ROOT}\TriageService.csproj"
if (-not $?) {
    Write-Output "编译失败"
    Quit(1)
}

Write-Output "制作镜像"
Set-Location -Path $ROOT -PassThru
wsl -u root docker build -t "$REGISTRY/triagesrv:$TAG" .
if (-not $?) {
    Write-Output "镜像制作失败"
    Quit(1)
}

Write-Output "发布镜像"
wsl -u root docker push "$REGISTRY/triagesrv:$TAG"
if (-not $?) {
    Write-Output "镜像推送失败"
    Quit(1)
}

Quit(0)