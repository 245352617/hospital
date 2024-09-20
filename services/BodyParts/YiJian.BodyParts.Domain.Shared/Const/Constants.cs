namespace YiJian.BodyParts.Domain.Shared.Const
{
    public class Constants
    {
        #region IcuParaModule表相关
        /// <summary>
        /// 观察项对应的模块编码
        /// </summary>
        public static readonly string MouduleCodeObservation = "0";

        /// <summary>
        /// 出入量对应的模块编码
        /// </summary>
        public static readonly string MouduleCodeInOut = "0";
        #endregion

        #region NursingWorkStaistics相关

        // // 0：医嘱，1：护理记录，2：系统项目，3：其他，4：诊断

        /// <summary>
        /// 医嘱
        /// </summary>
        public static readonly string nursingWorkStaisticsDateSourceAdvice = "0";

        /// <summary>
        /// 护理记录
        /// </summary>
        public static readonly string nursingWorkStaisticsDateSourceNurseRecord = "1";

        /// <summary>
        /// 系统项目
        /// </summary>
        public static string nursingWorkStaisticsDateSourceSys = "2";

        /// <summary>
        /// 其他
        /// </summary>
        public static string nursingWorkStaisticsDateSourceOther = "3";

        /// <summary>
        /// 诊断
        /// </summary>
        public static string nursingWorkStaisticsDateSourceDiag = "4";

        /// <summary>
        /// 护理标识
        /// </summary>
        public static string nursingWorkStaisticsModuleType4Nursing = "NursingWorkStaistics";

        /// <summary>
        /// 护理标识
        /// </summary>
        public static string nursingWorkStaisticsModuleType4Equipment = "Equipmentstatistics";

        /// <summary>
        /// 科室概览
        /// </summary>
        public static string nursingWorkStaisticsModuleType4DeptOverview = "DeptOverview";

        #endregion

        #region SysPara中的相关

        /// <summary>
        /// 是否显示质量登记表菜单
        /// </summary>
        public static string enableQualityRegisterMenu = "EnableQualityRegisterMenu";

        /// <summary>
        /// 是否显示质量登记表菜单
        /// </summary>
        public static string ieTabIcon = "IETabIcon";

        /// <summary>
        /// 有修改特护单权限的护士编码
        /// </summary>
        public static string paraCodeExamineTHDStaffCodes = "CanExamineTHDStaffCodes";

        /// <summary>
        /// 是否启用护理记录的皮肤记录
        /// </summary>
        public static string paraCodeEnableSkinRecord = "EnableSkinRecord";

        /// <summary>
        /// 是否启用护理记录
        /// </summary>
        public static string paraCodeEnableNureEventAudit = "EnableNureEventAudit";

        /// <summary>
        /// 有修改护理记录权限的护士编码
        /// </summary>
        public static string paraCodeCanModifyNursingRecordStaffCodes = "CanModifyNursingRecordStaffCodes";

        #region SysPara中的参数类别：D-科室级 S-系统级别
        /// <summary>
        /// 科室参数：D
        /// </summary>
        public static string paraTypeDept = "D";
        /// <summary>
        /// 第统参数：S
        /// </summary>
        public static string paramTypeSys = "S";
        # endregion
        #endregion

        # region 病人在科状态
        /// <summary>
        /// 1：在科；0：出科；2，取消入科，3:待入科，4:待出科
        /// </summary>
        public static int patientInDeptStatusIn = 1;
        /// <summary>
        /// 1：在科；0：出科；2，取消入科，3:待入科，4:待出科
        /// </summary>
        public static int patientInDeptStatusOut = 0;
        #endregion
        
        # region 病人入科方式
        /// <summary>
        /// 1:正常入科  入院/入科转icu实床
        /// </summary>
        public static int patStateNormal = 1;
        /// <summary>
        /// 紧急入科
        /// </summary>
        public static int patStateUrgent = 2;
        /// <summary>
        /// 虚床转实床
        /// </summary>
        public static int patStateVirture2Real = 3;
        /// <summary>
        /// 紧急入科后，接收消息更新后
        /// </summary>
        public static int patStateUrgent2Normal = 4;

        #endregion

        #region 同步设备定时器相关配置
        /// <summary>
        /// 同步设备定时器频率
        /// </summary>
        public static int syncDeviceFrequency = 10;
        /// <summary>
        /// 同步设备定时器的开始时间与结束时间的区间范围
        /// </summary>
        public static int syncDeviceRange = 60;
        #endregion

        #region 全局状态
        public static int validStateUseful = 1;
        public static int validStateUseless = 0;
        #endregion

        #region 无创血压的获取方式
        /// <summary>
        /// 从外部获取
        /// </summary>
        public static string NibpTakeTimeSourceOut = "OUT";

        /// <summary>
        /// 从内部获取
        /// </summary>
        public static string NibpTakeTimeSourceInner = "INNER";
        #endregion

        #region 医嘱类型（长嘱不是临嘱）
        /// <summary>
        /// 长期医嘱
        /// </summary>
        public static string AdviceTypeLongTerm = "1";

        /// <summary>
        /// 临时医嘱
        /// </summary>
        public static string AdviceTypeTemporary = "0";

        #endregion

        #region 医嘱执行的操作类型
        /// <summary>
        /// 开始
        /// </summary>
        public static string OperationTypeStart = "S";

        /// <summary>
        /// 暂停
        /// </summary>
        public static string OperationTypePause = "P";

        /// <summary>
        /// 恢复
        /// </summary>
        public static string OperationTypeResume = "R";

        /// <summary>
        /// 更新(加速、减速、加推)
        /// </summary>
        public static string OperationTypeUpdate = "E";

        /// <summary>
        /// 完成
        /// </summary>
        public static string OperationTypeFinish = "F";
        #endregion

        #region 护理类型
        /// <summary>
        /// 观察项
        /// </summary>
        public static string NusingRecordTypeVs = "VS";

        /// <summary>
        /// 出入量
        /// </summary>
        public static string NusingRecordTypeIo = "IO";

        /// <summary>
        /// 医嘱
        /// </summary>
        public static string NusingRecordTypeOrder = "ORDER";

        /// <summary>
        /// PICCO
        /// </summary>
        public static string NusingRecordTypePicco = "PC";

        /// <summary>
        /// BP
        /// </summary>
        public static string NusingRecordTypeBp = "BP";

        /// <summary>
        /// EM
        /// </summary>
        public static string NusingRecordTypeEm = "EM";
        #endregion

        #region 医嘱的执行状态
        /// <summary>
        /// 待执行: 计划执行在当前时间内，但没有超过当前时间
        /// </summary>
        public static string OrderExcuteFlagWait = "W";

        /// <summary>
        /// 未执行：计划执行时间在当班时间内，但超过了当班时间
        /// </summary>
        public static string OrderExcuteFlagNotExec = "NE";

        /// <summary>
        /// 执行中
        /// </summary>
        public static string OrderExcuteFlagExcuting = "E";

        /// <summary>
        /// 暂停
        /// </summary>
        public static string OrderExcuteFlagPause = "P";

        /// <summary>
        /// 完成
        /// </summary>
        public static string OrderExcuteFlagFinish = "F";
        #endregion
    }
}