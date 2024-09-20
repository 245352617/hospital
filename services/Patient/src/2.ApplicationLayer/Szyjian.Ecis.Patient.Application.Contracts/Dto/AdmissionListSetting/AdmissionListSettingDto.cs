namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class AdmissionListSettingDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 列值
        /// </summary>
        public string ColumnValue { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public string ColumnWidth { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNo { get; set; }

        /// <summary>
        /// 显示 0：不显示  1：显示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 单元格内文本是否换行 0：不换行，鼠标移上显示更多  1：换行，展示所有数据
        /// </summary>
        public bool ShowOverflowTooltip { get; set; }

        /// <summary>
        /// 列名(默认值)
        /// </summary>
        public string DefaultColumnName { get; set; }

        /// <summary>
        /// 列宽(默认值)
        /// </summary>
        public int DefaultColumnWidth { get; set; }

        /// <summary>
        /// 序号(默认值)
        /// </summary>
        public int DefaultSequenceNo { get; set; }

        /// <summary>
        /// 显示(默认值) 0：不显示  1：显示
        /// </summary>
        public bool DefaultVisible { get; set; }

        /// <summary>
        /// 单元格内文本是否换行(默认值) 0：不换行，鼠标移上显示更多  1：换行，展示所有数据
        /// </summary>
        public bool DefaultShowOverflowTooltip { get; set; }
    }
}