using System.Collections.Generic;

namespace YiJian.ECIS.Core.Utils;

/// <summary>
/// swagger xml 文档扩展
/// </summary>
public class SwaggerXmlDoc
{
    private static SwaggerXmlDoc instance;

    /// <summary>
    /// swagger xml 文档扩展
    /// </summary>
    private SwaggerXmlDoc()
    {

    }

    /// <summary>
    ///  
    /// </summary>
    /// <returns></returns>
    public static SwaggerXmlDoc GetInstance()
    {
        // 如果类的实例不存在则创建，否则直接返回
        if (instance == null)
        {
            instance = new SwaggerXmlDoc();
        }
        return instance;
    }

    /// <summary>
    /// 返回swagger xml 文档路径
    /// </summary>
    /// <param name="ns">项目命名空间路径：如[Product.IMedicine]</param>
    /// <returns></returns>
    public List<string> XmlDocs(string ns)
    {
        var xmlDocs = new List<string>();

        xmlDocs.Add(ns + ".HttpApi.xml");
        xmlDocs.Add(ns + ".HttpApi.Host.xml");
        xmlDocs.Add(ns + ".EntityFrameworkCore.xml");
        xmlDocs.Add(ns + ".Domain.xml");
        xmlDocs.Add(ns + ".Domain.Shared.xml");
        xmlDocs.Add(ns + ".Application.xml");
        xmlDocs.Add(ns + ".Application.Contracts.xml");
        xmlDocs.Add("YiJian.ECIS.Core.xml");
        xmlDocs.Add("YiJian.ECIS.ShareModel.xml");

        return xmlDocs;
    }


}
