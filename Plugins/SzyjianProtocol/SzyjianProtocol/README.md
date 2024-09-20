# 急诊系统使用的自定义协议插件

## 安装自定义协议

### 手动安装
* 1. 发布程序，部署模式选择“独立”，目标运行时选择“win-x86”，发布文件选项勾选“生成单个文件”、“启用ReadyToRun编译”、“裁剪未使用的代码”
* 2. 复制 SzyjianProtocol.exe 到 `C:\Program Files\Szyjian\SzyjianProtocol.exe`
* 3. 运行 SzyjianProtocal.reg 添加注册表

### 自动安装
* 1. 使用管理员权限运行install.bat


## 使用自定义协议打开IE

* Web上调用的 url 如下：
` <a href="szyjian:ie&aHR0cDovLzE3Mi4xNi4xNzUuMjI1OjgwODAvQkJNU1dlYi93ZWxjb21lMS5kbz9kZXBhcnROYW1lMT3mgKXor4rnp5EmZGVwYXJ0Q29kZTE9MTkwMiZkZXBhcnROYW1lMj0mZGVwYXJ0Q29kZTI9JmVtcGxveWVlTm89YWRtaW4mZW1wbG95ZWVOYW1lPeeuoeeQhuWRmCZwYXRpZW50SUQ9NjUyMDAwMjAwNTImZGVwYXJ0VHlwZT1PJmRvY3RvclR5cGU9RDImdGltZT0mYmVkTm89JmRpYWdub3Npcz1COTkueDAxJTJDUjUwLjgwMHgwMDImb3BlcmF0aW9uPSZUb2tlbj0=">Open With IE</a>`

* szyjian 是协议名称，ie 是调用的程序，&后面的是调用 ie 打开的url（使用base64转码）

* 前端字符串转base64参考

```javascript
let uurl = `http://172.16.175.225:8080/BBMSWeb/welcome1.html?departName1=${departName1}&departCode1=${departCode1}&departName2=&departCode2=&employeeNo=admin&employeeName=${employeeName}&patientID=${patientID}&departType=O&doctorType=D2&time=&bedNo=${bedNo}&diagnosis=${diagnosis}&operation=&Token=`
const base64Url = this.utf8_to_b64(uurl)
window.open(`szyjian://ie&${base64Url}`, '_blank', 'noopener,noreferrer')

utf8_to_b64(str) {
    return btoa(
    encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, function (match, p1) {
        return String.fromCharCode("0x" + p1);
    })
    );
}
```
