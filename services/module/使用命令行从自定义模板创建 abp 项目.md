

## 使用命令行从自定义模板创建 abp 项目



1、将 ECIS 项目中 `services\module` 目录压缩为 `module-4.4.3.zip`（其中`4.4.3`为abp-cli版本号 ）

2、如果未安装ABP CLI,第一步是安装ABP CLI

```shell
dotnet tool install -g Volo.Abp.Cli
```

3、新建项目空文件夹（Call）

4、使用 `abp new` 命令在空文件夹中创建新解决方案（其中 `D:\codes\yijian.ecis\services\module` 为 `module-4.4.3.zip` 文件所在目录）：

```shell
abp new YiJian.ECIS.Call -t module -ts D:\codes\yijian.ecis\services\module --local-framework-ref --abp-path
```

5、编译项目，确保编译通过

6、在外层项目（YiJian.ECIS）新建项目 `Call` 文件夹并添加 `YiJian.ECIS.Call.HttpApi.Host` 项目

