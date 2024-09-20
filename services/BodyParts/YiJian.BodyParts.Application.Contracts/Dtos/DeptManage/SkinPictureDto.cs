using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 皮肤图库
    /// </summary>
    public class SkinPictureDto
    {
        /// <summary>
        /// 上传时间
        /// </summary>
        public string UploadTime { get; set; }

        public List<SkinPictureItem> pictureItems { get; set; }
    }

    /// <summary>
    /// 图片列表
    /// </summary>
    public class SkinPictureItem
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public string File { get; set; }
    }
}
