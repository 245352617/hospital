using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace YiJian.ECIS.Core.FastReport
{
    /// <summary>
    /// 用于支持FastReport打印模板设计
    /// </summary>
    public static class FastReportUtils
    {
        /// <summary>
        /// 获取用于FastReport打印模板设计的xml结构和数据文件
        /// 对于枚举值，需要添加 XmlEnum 以序列化成整型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetXmlSchemalAndDataString<T>(T data)
        {
            // 获取返回DTO的XSD结构文件
            string xsd = FastReportUtils.GetXmlSchemalString(typeof(T), nameof(T));

            // 把数据序列化成xml
            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings()
            {
                Indent = true, // Indent it so we can see it better
                Encoding = Encoding.UTF8,
            };
            using var sw = new StringWriter();
            using var xw = new FastReportXmlWriter(XmlWriter.Create(sw, settings));
            serializer.Serialize(xw, data);
            XElement xrootData = XElement.Parse(sw.ToString());

            // 返回的xml根节点（包含结构跟数据，结构必须在前面）
            XElement xroot = new XElement(xrootData.Name);
            // 获取返回数据的xsd内容
            XElement xelement = XElement.Parse(xsd);
            xroot.Add(xelement);
            foreach (var item in xrootData.Elements())
            {
                xroot.Add(item);
            }
            XDocument xdoc = new XDocument(xroot);
            using StringWriter stringWriter = new StringWriter();
            xdoc.Save(stringWriter);

            return stringWriter.ToString();
        }

        /// <summary>
        /// 获取用于FastReport打印模板设计的xml数据文件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rootName"></param>
        /// <returns></returns>
        public static string GetXmlSchemalString(Type type, string rootName)
        {
            using var stringWriter = new FastReportStringWriter();
            XmlNamespaceManager nsm = new XmlNamespaceManager(new NameTable());
            nsm.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
            XmlSchema xmlSchema = new XmlSchema();
            var rootElement = GetXmlSchemaElements(type, rootName);
            xmlSchema.Items.Add(rootElement);
            xmlSchema.Write(stringWriter, nsm);
            string xsd = stringWriter.ToString();

            return xsd;
        }

        /// <summary>
        /// 获取单一节点的xml结构信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static XmlSchemaElement GetXmlSchemaElements(System.Type type, string elementName)
        {
            // .NET 数据类型映射到 XSD 数据类型的字典。(xml的字段类型, 是否允许为空)
            Dictionary<System.Type, (string, bool)> typeMap = new Dictionary<System.Type, (string, bool)>
            {
                { typeof(char), ("string", true) },
                { typeof(string), ("string", true) },
                { typeof(int), ("integer", false) },
                { typeof(short), ("short", false) },
                { typeof(long), ("long", false) },
                { typeof(float), ("float", false) },
                { typeof(double), ("double", false) },
                { typeof(decimal),  ("decimal", false) },
                { typeof(uint), ("unsignedInt", false) },
                { typeof(ushort), ("unsignedShort", false) },
                { typeof(ulong), ("unsignedLong", false) },
                { typeof(bool), ("boolean", true) },
                { typeof(DateTime), ("dateTime", true) },
                { typeof(Guid), ("string", true) },
            };
            XmlSchemaElement rootElement = new XmlSchemaElement();
            rootElement.Name = elementName;
            if (typeMap.ContainsKey(type))
            {
                rootElement.SchemaTypeName = new XmlQualifiedName(typeMap[type].Item1, "http://www.w3.org/2001/XMLSchema");
                rootElement.IsNillable = typeMap[type].Item2;
                return rootElement;
            }
            // IsNullableType
            if (type == null ? false : (type.IsArray == false && type.FullName.StartsWith("System.Nullable`1[") == true))
            {
                //// Nullable 类型的在xsd在校验xml时不通过，暂时用string类型处理
                //rootElement = GetXmlSchemaElements(type.GenericTypeArguments[0], elementName);
                // 不指定类型，标识nil
                rootElement.IsNillable = true;
                return rootElement;
            }
            // 枚举类型特殊处理，目前采用枚举值（整型），可以做配置根据实际情况支持枚举名或枚举值
            if (type.BaseType == typeof(Enum))
            {
                return GetXmlSchemaElements(typeof(int), elementName);
                //// 支持枚举名
                //return GetXmlSchemaElements(typeof(string), elementName);
            }
            // 未预期的数值类型的统一使用int
            if (type.BaseType == typeof(ValueType))
            {
                return GetXmlSchemaElements(typeof(int), elementName);
            }
            // 未预期的值类型，不做类型映射
            if (!type.IsClass)
            {
                return rootElement;
            }

            // 前面的流程已经处理了值类型/基本类型，到这里只需要处理非基本类型（class、List等）
            XmlSchemaComplexType rootXmlSchemaComplexType = new XmlSchemaComplexType();
            XmlSchemaSequence rootXmlSchemaSequence = new XmlSchemaSequence();
            rootXmlSchemaComplexType.Particle = rootXmlSchemaSequence;
            rootElement.SchemaType = rootXmlSchemaComplexType;

            var propertyInfos = type.GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                // 属性名称，如果有设置XmlElement标识，则使用XmlElement标识
                string propertyName = propertyInfo.Name.ToCamelCase();
                var attribute = propertyInfo.GetCustomAttribute(typeof(XmlElementAttribute));
                if (attribute is not null)
                {
                    propertyName = (attribute as XmlElementAttribute).ElementName;
                }
                if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetInterfaces().Contains(typeof(IEnumerable))
                    && propertyInfo.PropertyType.GenericTypeArguments.Length == 1)
                {// 如果属性是列表，则使用列表元素的数据类型
                    var childElement = GetXmlSchemaElements(propertyInfo.PropertyType.GenericTypeArguments[0], propertyName);
                    // 给列表默认设置 unbounded 属性
                    childElement.MaxOccursString = "unbounded";
                    rootXmlSchemaSequence.Items.Add(childElement);
                }
                else
                {// 如果属性不是列表，则使用当前属性的数据类型
                    var childElement = GetXmlSchemaElements(propertyInfo.PropertyType, propertyName);
                    rootXmlSchemaSequence.Items.Add(childElement);
                }
            }

            return rootElement;
        }
    }
}
