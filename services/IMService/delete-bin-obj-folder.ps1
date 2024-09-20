<#
文件：clear.ps1
用途：用于删除bin、obj目录的脚本
创建：2021-09-22 杨华
#>
Get-ChildItem .\ -include bin,obj -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }