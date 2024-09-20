namespace YiJian.MasterData.MasterData.Dictionaries;

/// <summary>
/// 电子病历水印对象
/// </summary>
public class EmrWatermarkDto
{
    /// <summary>
    /// 是否开启水印
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 电子病历水印内容
    /// </summary>
    public Watermarking Watermark { get; set; }

}

/// <summary>
/// 电子病历水印内容
/// </summary>
public class Watermarking
{
    /// <summary>
    /// 透明度
    /// </summary>
    public string Alpha { get; set; }
    /// <summary>
    /// 旋转角度
    /// </summary>
    public string Angle { get; set; }
    /// <summary>
    /// 背景颜色
    /// </summary>
    public string BackColorValue { get; set; }
    /// <summary>
    /// 文字样式
    /// </summary>
    public string ColorValue { get; set; }
    /// <summary>
    /// 水印间隔(0-1)
    /// </summary>
    public string DensityForRepeat { get; set; }
    /// <summary>
    /// 文字字体,大小,是否加粗
    /// </summary>
    public string Font { get; set; }
    /// <summary>
    /// base64字符串
    /// </summary>
    public string ImageDataBase64String { get; set; }
    /// <summary>
    /// 是否平铺
    /// </summary>
    public string Repeat { get; set; }
    /// <summary>
    /// 水印文字
    /// </summary>
    public string Text { get; set; }
    /// <summary>
    /// 水印类型(图片或者图片) 
    /// </summary>
    public string Type { get; set; }
}
