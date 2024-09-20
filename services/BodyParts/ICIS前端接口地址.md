### 局域网认证地址:
### AIMS局域网接口:

### 外网认证地址:
### 外网接口地址:


#### 

   

<br/>

## 手术排程模块接口

1. **科室名和科室编码数据**
<br/>请求类型：GET
<br/>请求参数(选填)：{DDeptAndName:0785/泌尿} 
<br/>方法名称：api/aims/surgicalScheduling/selectDepartment

        get http://localhost:5000/api/aims/surgicalScheduling/selectDepartment?DeptAndName=0785
		
2. ** 患者信息待排程数据**
<br/>请求类型：GET
<br/>请求参数(选填)：
{
NameAndInpo:刘/0000076551,
DeptIdaAndName:泌尿/0302,
StartTime:2019-01-23 01:37:46.870,
EndTime:2020-11-23 01:37:46.870
} 
<br/>方法名称：api/aims/surgicalScheduling/selectPatientWaitScheduled

        get http://localhost:5000/api/aims/surgicalScheduling/selectPatientWaitScheduled?NameAndInpo=刘&DeptIdaAndName=泌尿&StartTime=2019-01-23%2001%3A37%3A46.870&EndTime=2020-11-23%2001%3A37%3A46.870
		
		
3. ** 病人信息悬浮显示数据**
<br/>请求类型：GET
<br/>请求参数(选填)：
{
OperId:325059
} 
<br/>方法名称：api/aims/surgicalScheduling/selectPatientSuspension
        
		get   http://localhost:5000/api/aims/surgicalScheduling/selectPatientSuspension/325059   

4. ** 角色岗位显示数据**
<br/>请求类型：GET
<br/>请求参数(选填)：
{
StartTime:2020-01-23 01:37:46.870,
EndTime:2020-11-23 01:37:46.870
} 
<br/>方法名称：api/aims/surgicalScheduling/selectMedicalStaff                                                   
        
		get   http://localhost:5000/api/aims/surgicalScheduling/selectMedicalStaff?StartTime=2020-01-23%2001%3A37%3A46.870&EndTime=2020-11-23%2001%3A37%3A46.870  
		
5. ** 手术列表显示数据**
<br/>请求类型：GET
<br/>请求参数(选填)：
{
OperatingRoom:术间7
StartTime:2019-08-23 01:37:46.870,
EndTime:2020-11-23 01:37:46.870
} 
<br/>方法名称：api/aims/surgicalScheduling/selectOperations                                                   
        
		get   http://localhost:5000/api/aims/surgicalScheduling/selectOperations?OperatingRoom=术间7&StartTime=2019-08-23%2001%3A37%3A46.870&EndTime=2020-12-23%2001%3A37%3A46.870

		
		 
		
		
