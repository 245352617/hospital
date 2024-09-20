## 医院接口和请求客服端

### 1.医嘱支付（缴费）状态变更
method:`POST`

> http://219.134.240.180:51103/api/ecis/recipe/hospital/payment-status-change

```curl
curl -X POST "http://219.134.240.180:51103/api/ecis/recipe/hospital/payment-status-change"
-H  "accept: text/plain"
-H  "Content-Type: application/json-patch+json"
-d "{\"id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\"payStatus\":0}"
```

**请求说明**

| 字段      | 数据类型 | 是否必填 | 备注                                            |
| :-------- | -------- | :------: | ----------------------------------------------- |
| id        | uuid     |    √     | 医嘱的唯一标识符                                |
| payStatus | int      |    √     | 支付状态: 0=未支付,1=已支付,2=部分支付,3=已退费 |

```json
//请求参数示例
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "payStatus": 0
}
```

**响应说明**

| 字段      | 数据类型 | 备注                     |
| --------- | -------- | ------------------------ |
| code      | int      | [统一]状态码             |
| data      | bool     | 返回的数据               |
| success   | string   | [统一]是否成功           |
| message   | string   | [统一]消息提示           |
| errors    | string   | [统一]错误提示           |
| timestamp | int      | [统一]时间戳，一般用不上 |

```json
//返回参数示例
{
    "code": 200,
    "data": true,
    "success": true,
    "message": "操作成功",
    "errors": null,
    "timestamp": 1648521712493
}
```











---



### 药品

```
//数据结构示例
{
  "doctorsAdvice": {
    "id": "",
    "platformType": {},
    "piid": "",
    "patientId": "",
    "patientName": "",
    "applyDoctorCode": "",
    "applyDoctorName": "",
    "applyDeptCode": "",
    "applyDeptName": "",
    "traineeCode": "",
    "traineeName": "",
    "isChronicDisease": true,
    "diagnosis": "",
    "prescribeTypeCode": "",
    "prescribeTypeName": "",
    "itemType": {}
  },
  "items": {
    "id": "",
    "code": "",
    "name": "",
    "isOutDrug": true,
    "medicineProperty": "",
    "toxicProperty": "",
    "prescribeTypeCode": "",
    "prescribeTypeName": "",
    "startTime": "",
    "endTime": "",
    "usageCode": "",
    "usageName": "",
    "speed": "",
    "longDays": 0,
    "actualDays": 0,
    "dosageQty": 0,
    "defaultDosageQty": 0,
    "qtyPerTimes": 0,
    "dosageUnit": "",
    "defaultDosageUnit": "",
    "recieveQty": 0,
    "recieveUnit": "",
    "specification": "",
    "unpack": {},
    "bigPackPrice": 0,
    "bigPackFactor": 0,
    "bigPackUnit": "",
    "smallPackPrice": 0,
    "smallPackUnit": "",
    "smallPackFactor": 0,
    "frequencyCode": "",
    "frequencyName": "",
    "frequencyTimes": 0,
    "frequencyUnit": "",
    "frequencyExecDayTimes": "",
    "pharmacyCode": "",
    "pharmacyName": "",
    "factoryName": "",
    "factoryCode": "",
    "batchNo": "",
    "expirDate": "",
    "isSkinTest": true,
    "skinTestResult": true,
    "materialPrice": 0,
    "isBindingTreat": true,
    "isAmendedMark": true,
    "isAdaptationDisease": true,
    "isFirstAid": true,
    "unit": "",
    "price": 0,
    "insuranceCode": "",
    "insuranceType": {},
    "payTypeCode": "",
    "payType": {},
    "prescriptionNo": "",
    "recipeNo": "",
    "recipeGroupNo": 0,
    "applyTime": "",
    "categoryCode": "",
    "categoryName": "",
    "isBackTracking": true,
    "isRecipePrinted": true,
    "hisOrderNo": "",
    "positionCode": "",
    "positionName": "",
    "execDeptCode": "",
    "execDeptName": "",
    "antibioticPermission": 0,
    "prescriptionPermission": 0,
    "remarks": "",
    "chargeCode": "",
    "chargeName": ""
  }
}
```

> 字段说明

| 参数名称                                  | 参数说明                                                     | 请求类型 | 是否必须 | 数据类型                                             | schema                                               |
| ----------------------------------------- | ------------------------------------------------------------ | -------- | -------- | ---------------------------------------------------- | ---------------------------------------------------- |
| yiJian.DoctorsAdvices.Dto.AddPrescribeDto | 医嘱药品操作                                                 | body     | true     | YiJian.DoctorsAdvices.Dto.AddPrescribeDto            | YiJian.DoctorsAdvices.Dto.AddPrescribeDto            |
| doctorsAdvice                             |                                                              |          | false    | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto |
| id                                        |                                                              |          | false    | string                                               |                                                      |
| platformType                              |                                                              |          | true     | YiJian.ECIS.ShareModel.Enums.EPlatformType           | YiJian.ECIS.ShareModel.Enums.EPlatformType           |
| piid                                      | 患者唯一标识                                                 |          | true     | string                                               |                                                      |
| patientId                                 | 患者Id                                                       |          | false    | string                                               |                                                      |
| patientName                               | 患者名称                                                     |          | false    | string                                               |                                                      |
| applyDoctorCode                           | 申请医生编码                                                 |          | true     | string                                               |                                                      |
| applyDoctorName                           | 申请医生                                                     |          | true     | string                                               |                                                      |
| applyDeptCode                             | 申请科室编码                                                 |          | false    | string                                               |                                                      |
| applyDeptName                             | 申请科室                                                     |          | false    | string                                               |                                                      |
| traineeCode                               | 管培生代码                                                   |          | false    | string                                               |                                                      |
| traineeName                               | 管培生名称                                                   |          | false    | string                                               |                                                      |
| isChronicDisease                          | 是否慢性病                                                   |          | false    | boolean                                              |                                                      |
| diagnosis                                 | 临床诊断                                                     |          | false    | string                                               |                                                      |
| prescribeTypeCode                         | 医嘱类型编码                                                 |          | false    | string                                               |                                                      |
| prescribeTypeName                         | 医嘱类型：临嘱、长嘱、出院带药等                             |          | false    | string                                               |                                                      |
| itemType                                  |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   |
| items                                     |                                                              |          | false    | YiJian.DoctorsAdvices.Dto.PrescribeDto               | YiJian.DoctorsAdvices.Dto.PrescribeDto               |
| id                                        |                                                              |          | false    | string                                               |                                                      |
| code                                      | 开方编码                                                     |          | true     | string                                               |                                                      |
| name                                      | 开方名称                                                     |          | true     | string                                               |                                                      |
| isOutDrug                                 | 是否自备药：false=非自备药,true=自备药                       |          | false    | boolean                                              |                                                      |
| medicineProperty                          | 药物属性：西药、中药、西药制剂、中药制剂                     |          | true     | string                                               |                                                      |
| toxicProperty                             | 药理等级：如（毒、麻、精一、精二）\neg: ["k1":"v1","k2":"v2","k3":"v3",...] |          | false    | string                                               |                                                      |
| prescribeTypeCode                         | 医嘱类型编码                                                 |          | true     | string                                               |                                                      |
| prescribeTypeName                         | 医嘱类型：临嘱、长嘱、出院带药等                             |          | true     | string                                               |                                                      |
| startTime                                 | 开始时间                                                     |          | false    | string                                               |                                                      |
| endTime                                   | 结束时间                                                     |          | false    | string                                               |                                                      |
| usageCode                                 | 用法编码                                                     |          | true     | string                                               |                                                      |
| usageName                                 | 用法名称                                                     |          | true     | string                                               |                                                      |
| speed                                     | 滴速                                                         |          | false    | string                                               |                                                      |
| longDays                                  | 开药天数                                                     |          | false    | integer                                              |                                                      |
| actualDays                                | 实际天数                                                     |          | false    | integer                                              |                                                      |
| dosageQty                                 | 每次剂量                                                     |          | true     | number                                               |                                                      |
| defaultDosageQty                          | 每次剂量                                                     |          | false    | number                                               |                                                      |
| qtyPerTimes                               | 每次用量                                                     |          | false    | number                                               |                                                      |
| dosageUnit                                | 剂量单位                                                     |          | true     | string                                               |                                                      |
| defaultDosageUnit                         | 剂量单位                                                     |          | false    | string                                               |                                                      |
| recieveQty                                | 领量(数量)                                                   |          | false    | integer                                              |                                                      |
| recieveUnit                               | 领量单位                                                     |          | false    | string                                               |                                                      |
| specification                             | 包装规格                                                     |          | false    | string                                               |                                                      |
| unpack                                    |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EMedicineUnPack          | YiJian.DoctorsAdvices.Enums.EMedicineUnPack          |
| bigPackPrice                              | 包装价格                                                     |          | false    | number                                               |                                                      |
| bigPackFactor                             | 大包装系数(拆零系数)                                         |          | false    | integer                                              |                                                      |
| bigPackUnit                               | 包装单位                                                     |          | false    | string                                               |                                                      |
| smallPackPrice                            | 小包装单价                                                   |          | false    | number                                               |                                                      |
| smallPackUnit                             | 小包装单位                                                   |          | false    | string                                               |                                                      |
| smallPackFactor                           | 小包装系数(拆零系数)                                         |          | false    | integer                                              |                                                      |
| frequencyCode                             | 频次码                                                       |          | true     | string                                               |                                                      |
| frequencyName                             | 频次                                                         |          | true     | string                                               |                                                      |
| frequencyTimes                            | 在一个周期内执行的次数                                       |          | false    | integer                                              |                                                      |
| frequencyUnit                             | 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时 |          | false    | string                                               |                                                      |
| frequencyExecDayTimes                     | 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。\n日时间点只有一个的时候，格式为：HH:mm:ss.fff。\n日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。\n周时间点一个到多个的时候，格式为：周[一 \| 二 \| 三 \| 四 \| 五 \| 六 \| 日 \| 天]:HH:mm，以分号（;）分割。 |          | false    | string                                               |                                                      |
| pharmacyCode                              | 药房编码                                                     |          | false    | string                                               |                                                      |
| pharmacyName                              | 药房                                                         |          | false    | string                                               |                                                      |
| factoryName                               | 厂家                                                         |          | false    | string                                               |                                                      |
| factoryCode                               | 厂家代码                                                     |          | false    | string                                               |                                                      |
| batchNo                                   | 批次号                                                       |          | false    | string                                               |                                                      |
| expirDate                                 | 失效期                                                       |          | false    | string                                               |                                                      |
| isSkinTest                                | 是否皮试 false=不需要皮试 true=需要皮试                      |          | false    | boolean                                              |                                                      |
| skinTestResult                            | 皮试结果:false=阴性 ture=阳性                                |          | false    | boolean                                              |                                                      |
| materialPrice                             | 耗材金额                                                     |          | false    | number                                               |                                                      |
| isBindingTreat                            | 用于判断关联耗材是否手动删除                                 |          | false    | boolean                                              |                                                      |
| isAmendedMark                             | 是否抢救后补：false=非抢救后补，true=抢救后补                |          | false    | boolean                                              |                                                      |
| isAdaptationDisease                       | 是否医保适应症                                               |          | false    | boolean                                              |                                                      |
| isFirstAid                                | 是否是急救药                                                 |          | false    | boolean                                              |                                                      |
| unit                                      | 单位                                                         |          | false    | string                                               |                                                      |
| price                                     | 单价                                                         |          | false    | number                                               |                                                      |
| insuranceCode                             | 医保目录编码                                                 |          | false    | string                                               |                                                      |
| insuranceType                             |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        |
| payTypeCode                               | 付费类型编码                                                 |          | false    | string                                               |                                                      |
| payType                                   |                                                              |          | false    | YiJian.ECIS.ShareModel.Enums.ERecipePayType          | YiJian.ECIS.ShareModel.Enums.ERecipePayType          |
| prescriptionNo                            | 处方号                                                       |          | false    | string                                               |                                                      |
| recipeNo                                  | 医嘱号,如果前端传"auto" 过来，我自动生产；如果前端传具体值过来，我保存前端传过来的值 |          | false    | string                                               |                                                      |
| recipeGroupNo                             | 医嘱子号（同组下参数修改），提供修改，前端自行操作           |          | false    | integer                                              |                                                      |
| applyTime                                 | 开嘱时间                                                     |          | false    | string                                               |                                                      |
| categoryCode                              | 医嘱项目分类编码                                             |          | true     | string                                               |                                                      |
| categoryName                              | 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托) |          | true     | string                                               |                                                      |
| isBackTracking                            | 是否补录                                                     |          | false    | boolean                                              |                                                      |
| isRecipePrinted                           | 是否打印过                                                   |          | false    | boolean                                              |                                                      |
| hisOrderNo                                | HIS医嘱号                                                    |          | false    | string                                               |                                                      |
| positionCode                              | 位置编码                                                     |          | false    | string                                               |                                                      |
| positionName                              | 位置                                                         |          | false    | string                                               |                                                      |
| execDeptCode                              | 执行科室编码                                                 |          | false    | string                                               |                                                      |
| execDeptName                              | 执行科室名称                                                 |          | false    | string                                               |                                                      |
| antibioticPermission                      | 抗生素权限                                                   |          | false    | integer                                              |                                                      |
| prescriptionPermission                    | 处方权                                                       |          | false    | integer                                              |                                                      |
| remarks                                   | 医嘱说明                                                     |          | false    | string                                               |                                                      |
| chargeCode                                | 收费类型编码                                                 |          | false    | string                                               |                                                      |
| chargeName                                | 收费类型名称                                                 |          | false    | string                                               |                                                      |



### 检验

```
//数据结构示例
{
  "doctorsAdvice": {
    "id": "",
    "platformType": {},
    "piid": "",
    "patientId": "",
    "patientName": "",
    "applyDoctorCode": "",
    "applyDoctorName": "",
    "applyDeptCode": "",
    "applyDeptName": "",
    "traineeCode": "",
    "traineeName": "",
    "isChronicDisease": true,
    "diagnosis": "",
    "prescribeTypeCode": "",
    "prescribeTypeName": "",
    "itemType": {}
  },
  "items": [
    {
      "id": "",
      "code": "",
      "name": "",
      "catalogCode": "",
      "catalogName": "",
      "clinicalSymptom": "",
      "specimenCode": "",
      "specimenName": "",
      "specimenPartCode": "",
      "specimenPartName": "",
      "containerCode": "",
      "containerName": "",
      "containerColor": "",
      "specimenDescription": "",
      "specimenCollectDatetime": "",
      "specimenReceivedDatetime": "",
      "reportTime": "",
      "reportDoctorCode": "",
      "reportDoctorName": "",
      "hasReportName": true,
      "positionCode": "",
      "positionName": "",
      "isEmergency": true,
      "isBedSide": true,
      "unit": "",
      "price": 0,
      "insuranceCode": "",
      "insuranceType": {},
      "payTypeCode": "",
      "payType": {},
      "prescriptionNo": "",
      "recipeNo": "",
      "recipeGroupNo": 0,
      "applyTime": "",
      "categoryCode": "",
      "categoryName": "",
      "isBackTracking": true,
      "isRecipePrinted": true,
      "hisOrderNo": "",
      "execDeptCode": "",
      "execDeptName": "",
      "remarks": "",
      "chargeCode": "",
      "chargeName": "",
      "prescribeTypeCode": "",
      "prescribeTypeName": "",
      "startTime": "",
      "endTime": "",
      "recieveQty": 0,
      "recieveUnit": "",
      "items": [
        {
          "targetCode": "",
          "targetName": "",
          "targetUnit": "",
          "price": 0,
          "qty": 0,
          "insuranceCode": "",
          "insuranceType": {}
        }
      ]
    }
  ]
}
```

> 字段说明

| 参数名称                            | 参数说明                                                     | 请求类型 | 是否必须 | 数据类型                                             | schema                                               |
| ----------------------------------- | ------------------------------------------------------------ | -------- | -------- | ---------------------------------------------------- | ---------------------------------------------------- |
| yiJian.DoctorsAdvices.Dto.AddLisDto | 检验项信息参数                                               | body     | true     | YiJian.DoctorsAdvices.Dto.AddLisDto                  | YiJian.DoctorsAdvices.Dto.AddLisDto                  |
| doctorsAdvice                       |                                                              |          | false    | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto |
| id                                  |                                                              |          | false    | string                                               |                                                      |
| platformType                        |                                                              |          | true     | YiJian.ECIS.ShareModel.Enums.EPlatformType           | YiJian.ECIS.ShareModel.Enums.EPlatformType           |
| piid                                | 患者唯一标识                                                 |          | true     | string                                               |                                                      |
| patientId                           | 患者Id                                                       |          | false    | string                                               |                                                      |
| patientName                         | 患者名称                                                     |          | false    | string                                               |                                                      |
| applyDoctorCode                     | 申请医生编码                                                 |          | true     | string                                               |                                                      |
| applyDoctorName                     | 申请医生                                                     |          | true     | string                                               |                                                      |
| applyDeptCode                       | 申请科室编码                                                 |          | false    | string                                               |                                                      |
| applyDeptName                       | 申请科室                                                     |          | false    | string                                               |                                                      |
| traineeCode                         | 管培生代码                                                   |          | false    | string                                               |                                                      |
| traineeName                         | 管培生名称                                                   |          | false    | string                                               |                                                      |
| isChronicDisease                    | 是否慢性病                                                   |          | false    | boolean                                              |                                                      |
| diagnosis                           | 临床诊断                                                     |          | false    | string                                               |                                                      |
| prescribeTypeCode                   | 医嘱类型编码                                                 |          | false    | string                                               |                                                      |
| prescribeTypeName                   | 医嘱类型：临嘱、长嘱、出院带药等                             |          | false    | string                                               |                                                      |
| itemType                            |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   |
| items                               | 检验项集合                                                   |          | false    | array                                                | YiJian.DoctorsAdvices.Dto.LisDto                     |
| id                                  |                                                              |          | false    | string                                               |                                                      |
| code                                | 检验项代码                                                   |          | true     | string                                               |                                                      |
| name                                | 检验项名称                                                   |          | true     | string                                               |                                                      |
| catalogCode                         | 检验类别编码                                                 |          | true     | string                                               |                                                      |
| catalogName                         | 检验类别                                                     |          | true     | string                                               |                                                      |
| clinicalSymptom                     | 临床症状                                                     |          | false    | string                                               |                                                      |
| specimenCode                        | 标本编码                                                     |          | true     | string                                               |                                                      |
| specimenName                        | 标本名称                                                     |          | true     | string                                               |                                                      |
| specimenPartCode                    | 标本采集部位编码                                             |          | false    | string                                               |                                                      |
| specimenPartName                    | 标本采集部位                                                 |          | false    | string                                               |                                                      |
| containerCode                       | 标本容器代码                                                 |          | false    | string                                               |                                                      |
| containerName                       | 标本容器                                                     |          | false    | string                                               |                                                      |
| containerColor                      | 标本容器颜色:0=红帽,1=蓝帽,2=紫帽                            |          | false    | string                                               |                                                      |
| specimenDescription                 | 标本说明                                                     |          | false    | string                                               |                                                      |
| specimenCollectDatetime             | 标本采集时间                                                 |          | false    | string                                               |                                                      |
| specimenReceivedDatetime            | 标本接收时间                                                 |          | false    | string                                               |                                                      |
| reportTime                          | 出报告时间                                                   |          | false    | string                                               |                                                      |
| reportDoctorCode                    | 确认报告医生编码                                             |          | false    | string                                               |                                                      |
| reportDoctorName                    | 确认报告医生                                                 |          | false    | string                                               |                                                      |
| hasReportName                       | 报告标识                                                     |          | false    | boolean                                              |                                                      |
| positionCode                        | 位置编码                                                     |          | false    | string                                               |                                                      |
| positionName                        | 位置                                                         |          | false    | string                                               |                                                      |
| isEmergency                         | 是否紧急                                                     |          | false    | boolean                                              |                                                      |
| isBedSide                           | 是否在床旁                                                   |          | false    | boolean                                              |                                                      |
| unit                                | 单位                                                         |          | false    | string                                               |                                                      |
| price                               | 单价                                                         |          | false    | number                                               |                                                      |
| insuranceCode                       | 医保目录编码                                                 |          | false    | string                                               |                                                      |
| insuranceType                       |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        |
| payTypeCode                         | 付费类型编码                                                 |          | false    | string                                               |                                                      |
| payType                             |                                                              |          | false    | YiJian.ECIS.ShareModel.Enums.ERecipePayType          | YiJian.ECIS.ShareModel.Enums.ERecipePayType          |
| prescriptionNo                      | 处方号                                                       |          | false    | string                                               |                                                      |
| recipeNo                            | 医嘱号,如果前端传"auto" 过来，我自动生产；如果前端传具体值过来，我保存前端传过来的值 |          | false    | string                                               |                                                      |
| recipeGroupNo                       | 医嘱子号（同组下参数修改），提供修改，前端自行操作           |          | false    | integer                                              |                                                      |
| applyTime                           | 开嘱时间                                                     |          | false    | string                                               |                                                      |
| categoryCode                        | 医嘱项目分类编码                                             |          | true     | string                                               |                                                      |
| categoryName                        | 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托) |          | true     | string                                               |                                                      |
| isBackTracking                      | 是否补录                                                     |          | false    | boolean                                              |                                                      |
| isRecipePrinted                     | 是否打印过                                                   |          | false    | boolean                                              |                                                      |
| hisOrderNo                          | HIS医嘱号                                                    |          | false    | string                                               |                                                      |
| execDeptCode                        | 执行科室编码                                                 |          | false    | string                                               |                                                      |
| execDeptName                        | 执行科室名称                                                 |          | false    | string                                               |                                                      |
| remarks                             | 医嘱说明                                                     |          | false    | string                                               |                                                      |
| chargeCode                          | 收费类型编码                                                 |          | false    | string                                               |                                                      |
| chargeName                          | 收费类型名称                                                 |          | false    | string                                               |                                                      |
| prescribeTypeCode                   | 医嘱类型编码                                                 |          | true     | string                                               |                                                      |
| prescribeTypeName                   | 医嘱类型：临嘱、长嘱、出院带药等                             |          | true     | string                                               |                                                      |
| startTime                           | 开始时间                                                     |          | false    | string                                               |                                                      |
| endTime                             | 结束时间                                                     |          | false    | string                                               |                                                      |
| recieveQty                          | 领量(数量)                                                   |          | false    | integer                                              |                                                      |
| recieveUnit                         | 领量单位                                                     |          | false    | string                                               |                                                      |
| items                               | 检验小项集合                                                 |          | false    | array                                                | YiJian.DoctorsAdvices.Dto.LisItemDto                 |
| targetCode                          | 小项编码                                                     |          | true     | string                                               |                                                      |
| targetName                          | 小项名称                                                     |          | true     | string                                               |                                                      |
| targetUnit                          | 单位                                                         |          | true     | string                                               |                                                      |
| price                               | 单价                                                         |          | false    | number                                               |                                                      |
| qty                                 | 数量                                                         |          | false    | number                                               |                                                      |
| insuranceCode                       | 医保目录编码                                                 |          | false    | string                                               |                                                      |
| insuranceType                       |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        |



### 检查

```
//数据结构示例
{
  "doctorsAdvice": {
    "id": "",
    "platformType": {},
    "piid": "",
    "patientId": "",
    "patientName": "",
    "applyDoctorCode": "",
    "applyDoctorName": "",
    "applyDeptCode": "",
    "applyDeptName": "",
    "traineeCode": "",
    "traineeName": "",
    "isChronicDisease": true,
    "diagnosis": "",
    "prescribeTypeCode": "",
    "prescribeTypeName": "",
    "itemType": {}
  },
  "items": [
    {
      "id": "",
      "code": "",
      "name": "",
      "catalogCode": "",
      "catalogName": "",
      "clinicalSymptom": "",
      "medicalHistory": "",
      "partCode": "",
      "partName": "",
      "catalogDisplayName": "",
      "reportTime": "",
      "reportDoctorCode": "",
      "reportDoctorName": "",
      "hasReport": true,
      "positionCode": "",
      "positionName": "",
      "isEmergency": true,
      "isBedSide": true,
      "unit": "",
      "price": 0,
      "insuranceCode": "",
      "insuranceType": {},
      "payTypeCode": "",
      "payType": {},
      "prescriptionNo": "",
      "recipeNo": "",
      "recipeGroupNo": 0,
      "applyTime": "",
      "categoryCode": "",
      "categoryName": "",
      "isBackTracking": true,
      "isRecipePrinted": true,
      "hisOrderNo": "",
      "execDeptCode": "",
      "execDeptName": "",
      "remarks": "",
      "chargeCode": "",
      "chargeName": "",
      "prescribeTypeCode": "",
      "prescribeTypeName": "",
      "startTime": "",
      "endTime": "",
      "recieveQty": 0,
      "recieveUnit": "",
      "items": [
        {
          "targetCode": "",
          "targetName": "",
          "targetUnit": "",
          "price": 0,
          "qty": 0,
          "insuranceCode": "",
          "insuranceType": {}
        }
      ]
    }
  ]
}
```

> 字段说明

| 参数名称                             | 参数说明                                                     | 请求类型 | 是否必须 | 数据类型                                             | schema                                               |
| ------------------------------------ | ------------------------------------------------------------ | -------- | -------- | ---------------------------------------------------- | ---------------------------------------------------- |
| yiJian.DoctorsAdvices.Dto.AddPacsDto | 检查项信息参数                                               | body     | true     | YiJian.DoctorsAdvices.Dto.AddPacsDto                 | YiJian.DoctorsAdvices.Dto.AddPacsDto                 |
| doctorsAdvice                        |                                                              |          | false    | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto |
| id                                   |                                                              |          | false    | string                                               |                                                      |
| platformType                         |                                                              |          | true     | YiJian.ECIS.ShareModel.Enums.EPlatformType           | YiJian.ECIS.ShareModel.Enums.EPlatformType           |
| piid                                 | 患者唯一标识                                                 |          | true     | string                                               |                                                      |
| patientId                            | 患者Id                                                       |          | false    | string                                               |                                                      |
| patientName                          | 患者名称                                                     |          | false    | string                                               |                                                      |
| applyDoctorCode                      | 申请医生编码                                                 |          | true     | string                                               |                                                      |
| applyDoctorName                      | 申请医生                                                     |          | true     | string                                               |                                                      |
| applyDeptCode                        | 申请科室编码                                                 |          | false    | string                                               |                                                      |
| applyDeptName                        | 申请科室                                                     |          | false    | string                                               |                                                      |
| traineeCode                          | 管培生代码                                                   |          | false    | string                                               |                                                      |
| traineeName                          | 管培生名称                                                   |          | false    | string                                               |                                                      |
| isChronicDisease                     | 是否慢性病                                                   |          | false    | boolean                                              |                                                      |
| diagnosis                            | 临床诊断                                                     |          | false    | string                                               |                                                      |
| prescribeTypeCode                    | 医嘱类型编码                                                 |          | false    | string                                               |                                                      |
| prescribeTypeName                    | 医嘱类型：临嘱、长嘱、出院带药等                             |          | false    | string                                               |                                                      |
| itemType                             |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   |
| items                                | 检查项集合                                                   |          | false    | array                                                | YiJian.DoctorsAdvices.Dto.PacsDto                    |
| id                                   |                                                              |          | false    | string                                               |                                                      |
| code                                 | 检查项代码                                                   |          | true     | string                                               |                                                      |
| name                                 | 检查项名称                                                   |          | true     | string                                               |                                                      |
| catalogCode                          | 检查类别编码                                                 |          | true     | string                                               |                                                      |
| catalogName                          | 检查类别                                                     |          | true     | string                                               |                                                      |
| clinicalSymptom                      | 临床症状                                                     |          | false    | string                                               |                                                      |
| medicalHistory                       | 病史简要                                                     |          | false    | string                                               |                                                      |
| partCode                             | 检查部位编码                                                 |          | false    | string                                               |                                                      |
| partName                             | 检查部位                                                     |          | false    | string                                               |                                                      |
| catalogDisplayName                   | 分类类型名称 例如心电图申请单、超声申请单                    |          | false    | string                                               |                                                      |
| reportTime                           | 出报告时间                                                   |          | false    | string                                               |                                                      |
| reportDoctorCode                     | 确认报告医生编码                                             |          | false    | string                                               |                                                      |
| reportDoctorName                     | 确认报告医生                                                 |          | false    | string                                               |                                                      |
| hasReport                            | 报告标识                                                     |          | false    | boolean                                              |                                                      |
| positionCode                         | 位置编码                                                     |          | false    | string                                               |                                                      |
| positionName                         | 位置                                                         |          | false    | string                                               |                                                      |
| isEmergency                          | 是否紧急                                                     |          | false    | boolean                                              |                                                      |
| isBedSide                            | 是否在床旁                                                   |          | false    | boolean                                              |                                                      |
| unit                                 | 单位                                                         |          | false    | string                                               |                                                      |
| price                                | 单价                                                         |          | false    | number                                               |                                                      |
| insuranceCode                        | 医保目录编码                                                 |          | false    | string                                               |                                                      |
| insuranceType                        |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        |
| payTypeCode                          | 付费类型编码                                                 |          | false    | string                                               |                                                      |
| payType                              |                                                              |          | false    | YiJian.ECIS.ShareModel.Enums.ERecipePayType          | YiJian.ECIS.ShareModel.Enums.ERecipePayType          |
| prescriptionNo                       | 处方号                                                       |          | false    | string                                               |                                                      |
| recipeNo                             | 医嘱号,如果前端传"auto" 过来，我自动生产；如果前端传具体值过来，我保存前端传过来的值 |          | false    | string                                               |                                                      |
| recipeGroupNo                        | 医嘱子号（同组下参数修改），提供修改，前端自行操作           |          | false    | integer                                              |                                                      |
| applyTime                            | 开嘱时间                                                     |          | false    | string                                               |                                                      |
| categoryCode                         | 医嘱项目分类编码                                             |          | true     | string                                               |                                                      |
| categoryName                         | 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托) |          | true     | string                                               |                                                      |
| isBackTracking                       | 是否补录                                                     |          | false    | boolean                                              |                                                      |
| isRecipePrinted                      | 是否打印过                                                   |          | false    | boolean                                              |                                                      |
| hisOrderNo                           | HIS医嘱号                                                    |          | false    | string                                               |                                                      |
| execDeptCode                         | 执行科室编码                                                 |          | false    | string                                               |                                                      |
| execDeptName                         | 执行科室名称                                                 |          | false    | string                                               |                                                      |
| remarks                              | 医嘱说明                                                     |          | false    | string                                               |                                                      |
| chargeCode                           | 收费类型编码                                                 |          | false    | string                                               |                                                      |
| chargeName                           | 收费类型名称                                                 |          | false    | string                                               |                                                      |
| prescribeTypeCode                    | 医嘱类型编码                                                 |          | true     | string                                               |                                                      |
| prescribeTypeName                    | 医嘱类型：临嘱、长嘱、出院带药等                             |          | true     | string                                               |                                                      |
| startTime                            | 开始时间                                                     |          | false    | string                                               |                                                      |
| endTime                              | 结束时间                                                     |          | false    | string                                               |                                                      |
| recieveQty                           | 领量(数量)                                                   |          | false    | integer                                              |                                                      |
| recieveUnit                          | 领量单位                                                     |          | false    | string                                               |                                                      |
| items                                | 检查小项集合                                                 |          | false    | array                                                | YiJian.DoctorsAdvices.Dto.PacsItemDto                |
| targetCode                           | 小项编码                                                     |          | true     | string                                               |                                                      |
| targetName                           | 小项名称                                                     |          | true     | string                                               |                                                      |
| targetUnit                           | 单位                                                         |          | true     | string                                               |                                                      |
| price                                | 单价                                                         |          | false    | number                                               |                                                      |
| qty                                  | 数量                                                         |          | false    | number                                               |                                                      |
| insuranceCode                        | 医保目录编码                                                 |          | false    | string                                               |                                                      |
| insuranceType                        |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        |



### 诊疗项



```
//数据结构示例
{
  "doctorsAdvice": {
    "id": "",
    "platformType": {},
    "piid": "",
    "patientId": "",
    "patientName": "",
    "applyDoctorCode": "",
    "applyDoctorName": "",
    "applyDeptCode": "",
    "applyDeptName": "",
    "traineeCode": "",
    "traineeName": "",
    "isChronicDisease": true,
    "diagnosis": "",
    "prescribeTypeCode": "",
    "prescribeTypeName": "",
    "itemType": {}
  },
  "items": {
    "id": "",
    "code": "",
    "name": "",
    "unit": "",
    "price": 0,
    "otherPrice": 0,
    "specification": "",
    "frequencyCode": "",
    "frequencyName": "",
    "feeTypeMainCode": "",
    "feeTypeSubCode": "",
    "insuranceCode": "",
    "insuranceType": {},
    "payTypeCode": "",
    "payType": {},
    "prescriptionNo": "",
    "recipeNo": "",
    "recipeGroupNo": 0,
    "applyTime": "",
    "categoryCode": "",
    "categoryName": "",
    "isBackTracking": true,
    "isRecipePrinted": true,
    "hisOrderNo": "",
    "positionCode": "",
    "positionName": "",
    "execDeptCode": "",
    "execDeptName": "",
    "remarks": "",
    "chargeCode": "",
    "chargeName": "",
    "prescribeTypeCode": "",
    "prescribeTypeName": "",
    "startTime": "",
    "endTime": "",
    "recieveQty": 0,
    "recieveUnit": ""
  }
}
```

> 字段说明

| 参数名称                              | 参数说明                                                     | 请求类型 | 是否必须 | 数据类型                                             | schema                                               |
| ------------------------------------- | ------------------------------------------------------------ | -------- | -------- | ---------------------------------------------------- | ---------------------------------------------------- |
| yiJian.DoctorsAdvices.Dto.AddTreatDto | 新增诊疗项操作                                               | body     | true     | YiJian.DoctorsAdvices.Dto.AddTreatDto                | YiJian.DoctorsAdvices.Dto.AddTreatDto                |
| doctorsAdvice                         |                                                              |          | false    | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto | YiJian.DoctorsAdvices.Dto.ModifyDoctorsAdviceBaseDto |
| id                                    |                                                              |          | false    | string                                               |                                                      |
| platformType                          |                                                              |          | true     | YiJian.ECIS.ShareModel.Enums.EPlatformType           | YiJian.ECIS.ShareModel.Enums.EPlatformType           |
| piid                                  | 患者唯一标识                                                 |          | true     | string                                               |                                                      |
| patientId                             | 患者Id                                                       |          | false    | string                                               |                                                      |
| patientName                           | 患者名称                                                     |          | false    | string                                               |                                                      |
| applyDoctorCode                       | 申请医生编码                                                 |          | true     | string                                               |                                                      |
| applyDoctorName                       | 申请医生                                                     |          | true     | string                                               |                                                      |
| applyDeptCode                         | 申请科室编码                                                 |          | false    | string                                               |                                                      |
| applyDeptName                         | 申请科室                                                     |          | false    | string                                               |                                                      |
| traineeCode                           | 管培生代码                                                   |          | false    | string                                               |                                                      |
| traineeName                           | 管培生名称                                                   |          | false    | string                                               |                                                      |
| isChronicDisease                      | 是否慢性病                                                   |          | false    | boolean                                              |                                                      |
| diagnosis                             | 临床诊断                                                     |          | false    | string                                               |                                                      |
| prescribeTypeCode                     | 医嘱类型编码                                                 |          | false    | string                                               |                                                      |
| prescribeTypeName                     | 医嘱类型：临嘱、长嘱、出院带药等                             |          | false    | string                                               |                                                      |
| itemType                              |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   | YiJian.DoctorsAdvices.Enums.EDoctorsAdviceItemType   |
| items                                 |                                                              |          | false    | YiJian.DoctorsAdvices.Dto.TreatDto                   | YiJian.DoctorsAdvices.Dto.TreatDto                   |
| id                                    |                                                              |          | false    | string                                               |                                                      |
| code                                  | 诊疗项代码                                                   |          | true     | string                                               |                                                      |
| name                                  | 检查项名称                                                   |          | true     | string                                               |                                                      |
| unit                                  | 单位                                                         |          | false    | string                                               |                                                      |
| price                                 | 单价                                                         |          | false    | number                                               |                                                      |
| otherPrice                            | 其它价格                                                     |          | false    | number                                               |                                                      |
| specification                         | 规格                                                         |          | false    | string                                               |                                                      |
| frequencyCode                         | 默认频次代码                                                 |          | false    | string                                               |                                                      |
| frequencyName                         | 频次                                                         |          | false    | string                                               |                                                      |
| feeTypeMainCode                       | 收费大类代码                                                 |          | false    | string                                               |                                                      |
| feeTypeSubCode                        | 收费小类代码                                                 |          | false    | string                                               |                                                      |
| insuranceCode                         | 医保目录编码                                                 |          | false    | string                                               |                                                      |
| insuranceType                         |                                                              |          | false    | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        | YiJian.DoctorsAdvices.Enums.EInsuranceCatalog        |
| payTypeCode                           | 付费类型编码                                                 |          | false    | string                                               |                                                      |
| payType                               |                                                              |          | false    | YiJian.ECIS.ShareModel.Enums.ERecipePayType          | YiJian.ECIS.ShareModel.Enums.ERecipePayType          |
| prescriptionNo                        | 处方号                                                       |          | false    | string                                               |                                                      |
| recipeNo                              | 医嘱号,如果前端传"auto" 过来，我自动生产；如果前端传具体值过来，我保存前端传过来的值 |          | false    | string                                               |                                                      |
| recipeGroupNo                         | 医嘱子号（同组下参数修改），提供修改，前端自行操作           |          | false    | integer                                              |                                                      |
| applyTime                             | 开嘱时间                                                     |          | false    | string                                               |                                                      |
| categoryCode                          | 医嘱项目分类编码                                             |          | true     | string                                               |                                                      |
| categoryName                          | 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托) |          | true     | string                                               |                                                      |
| isBackTracking                        | 是否补录                                                     |          | false    | boolean                                              |                                                      |
| isRecipePrinted                       | 是否打印过                                                   |          | false    | boolean                                              |                                                      |
| hisOrderNo                            | HIS医嘱号                                                    |          | false    | string                                               |                                                      |
| positionCode                          | 位置编码                                                     |          | false    | string                                               |                                                      |
| positionName                          | 位置                                                         |          | false    | string                                               |                                                      |
| execDeptCode                          | 执行科室编码                                                 |          | false    | string                                               |                                                      |
| execDeptName                          | 执行科室名称                                                 |          | false    | string                                               |                                                      |
| remarks                               | 医嘱说明                                                     |          | false    | string                                               |                                                      |
| chargeCode                            | 收费类型编码                                                 |          | false    | string                                               |                                                      |
| chargeName                            | 收费类型名称                                                 |          | false    | string                                               |                                                      |
| prescribeTypeCode                     | 医嘱类型编码                                                 |          | true     | string                                               |                                                      |
| prescribeTypeName                     | 医嘱类型：临嘱、长嘱、出院带药等                             |          | true     | string                                               |                                                      |
| startTime                             | 开始时间                                                     |          | false    | string                                               |                                                      |
| endTime                               | 结束时间                                                     |          | false    | string                                               |                                                      |
| recieveQty                            | 领量(数量)                                                   |          | false    | integer                                              |                                                      |
| recieveUnit                           | 领量单位                                                     |          | false    | string                                               |                                                      |
