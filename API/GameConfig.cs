using config;
using System.IO;
using System.Xml;
using UnityEngine;

public static class GameConfig
{
    private static readonly XmlDocument xmlDoc;
    private static readonly ConfigXML config = new ConfigXML();
    private static readonly string xmlFilePath;

    static GameConfig ()
    {
        xmlFilePath = Application.persistentDataPath + "/CORECONFIG.xml";
        xmlDoc = new XmlDocument();

        if (File.Exists(xmlFilePath))
        {
            xmlDoc.Load(xmlFilePath);
        }
        else
        {
            InitializeXml();
        }
    }

    private static void InitializeXml ()
    {
        // ��ʼ�� XML �ĵ�
        // ����������趨һЩĬ��ֵ����Ϊ�յļ�ֵ��

        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlNode rootNode = xmlDoc.CreateElement("CORECONFIG");
        xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
        xmlDoc.AppendChild(rootNode);

        xmlDoc.Save(xmlFilePath);
    }

    public static string GetValue (string key, string path)
    {
        if (File.Exists($"{Application.persistentDataPath}/{path}"))//����
        {
            return config.��������(key, path);
        }
        else
        {
            return "None";
        }
    }

    public static void SetValue (string key, string vlaue, string path)
    {
        if (File.Exists($"{Application.persistentDataPath}/{path}"))//����
        {
            if (config.��������(key, path) == "None")
            {
                config.����������(key, vlaue, path);
            }
            else
            {
                config.����������(key, vlaue, path);
            }
        }
        else//������
        {
            config.���������ļ�(key, vlaue, path);
        }
    }

    public static string GetValue (string key)
    {
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName(key);
        if (nodeList.Count > 0)
        {
            return nodeList[0].InnerText;
        }

        return "None";
    }

    public static void SetValue (string key, string value)
    {
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName(key);

        if (nodeList.Count > 0)
        {
            nodeList[0].InnerText = value;
        }
        else
        {
            XmlNode rootNode = xmlDoc.DocumentElement;
            XmlNode newNode = xmlDoc.CreateElement(key);
            newNode.InnerText = value;
            rootNode.AppendChild(newNode);
        }
        xmlDoc.Save(xmlFilePath);
    }
}