using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using StreamJsonRpc;
using YiJian.EMR.Templates.Dto;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 电子病历水印模型
    /// </summary> 
    public class EmrWatermarkModel
    {
        /// <summary>
        /// 是否开启水印
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 水印
        /// </summary> 
        [JsonProperty("Watermark")]
        public WatermarkModel Watermark { get; set; }
    }

    /// <summary>
    /// 水印
    /// </summary>
    public class WatermarkModel
    {

        /// <summary>
        /// 透明度
        /// </summary> 
        [JsonProperty("Alpha")]
        public string Alpha { get; set; } = "50";

        /// <summary>
        /// 倾斜角度
        /// </summary> 
        [JsonProperty("Angle")]
        public string Angle { get; set; } = "325";

        /// <summary>
        /// 背景颜色
        /// </summary> 
        [JsonProperty("BackColorValue")]
        public string BackColorValue { get; set; } = "White";

        /// <summary>
        /// 文字样式
        /// </summary> 
        [JsonProperty("ColorValue")]
        public string ColorValue { get; set; } = "#898989";

        /// <summary>
        /// 水印间隔(0-1)
        /// </summary> 
        [JsonProperty("DensityForRepeat")]
        public string DensityForRepeat { get; set; } = "0.5";

        /// <summary>
        /// 文字字体,大小,是否加粗
        /// </summary> 
        [JsonProperty("Font")]
        public string Font { get; set; } = "楷体, 24, style=Bold";

        /// <summary>
        /// base64字符串
        /// </summary> 
        [JsonProperty("ImageDataBase64String")]
        public string ImageDataBase64String { get; set; } = "";

        /// <summary>
        /// 是否平铺
        /// </summary> 
        [JsonProperty("Repeat")]
        public string Repeat { get; set; } = "True";

        /// <summary>
        /// 水印文字
        /// </summary> 
        [JsonProperty("Text")]
        public string Text { get; set; } = "龙岗中心医院";

        /// <summary>
        /// 水印类型(图片或者图片) 
        /// </summary> 
        [JsonProperty("Type")]
        public string Type { get; set; } = "Text";
    }

}
