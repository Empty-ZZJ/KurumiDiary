using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

namespace config
{
    //ConfigXML configXML = new ConfigXML();
    public class ConfigXML
    {
        public void ���������ļ� (string �ڵ���, string д��ֵ, string �ļ���)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + �ļ���;
            if (!File.Exists(localPath))
            {
                XmlDocument xml = new XmlDocument();
                XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", "");
                XmlElement root = xml.CreateElement("root");
                XmlElement info = xml.CreateElement(�ڵ���);
                info.SetAttribute(�ڵ���, д��ֵ);
                root.AppendChild(info);
                xml.AppendChild(root);
                xml.Save(localPath);
            }
        }

        public void ���������� (string �ڵ���, string д��ֵ, string �ļ���)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + �ļ���;
            if (File.Exists(localPath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(localPath);
                XmlNode root = xml.SelectSingleNode("root");
                XmlElement info = xml.CreateElement(�ڵ���);
                info.SetAttribute(�ڵ���, д��ֵ);
                root.AppendChild(info);
                xml.AppendChild(root);
                xml.Save(localPath);
            }
        }

        public string �������� (string ����ȡ�ڵ�, string �ļ���)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + �ļ���;
            if (File.Exists(localPath))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(localPath);
                XmlNodeList nodeList = xml.SelectSingleNode("root").ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.Name == ����ȡ�ڵ�)
                    {
                        return (xe.GetAttribute(����ȡ�ڵ�));
                    }
                }
            }
            return ("None");
        }

        /// <summary>
        /// �ļ�����Ҫ��׺
        /// </summary>
        /// <param name="����ȡ�ڵ�"></param>
        /// <param name="�ļ���"></param>
        /// <returns></returns>
        public string ��������_Resources (string ����ȡ�ڵ�, string �ļ���)
        {
            TextAsset textAsset = (TextAsset)Resources.Load(�ļ���, typeof(TextAsset));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.Name == ����ȡ�ڵ�)
                {
                    return (xe.GetAttribute(����ȡ�ڵ�));
                }
            }
            return ("None");
        }

        public void ���������� (string �����½ڵ�, string ������ֵ, string �ļ���)
        {
            string localPath = UnityEngine.Application.persistentDataPath + "/" + �ļ���;
            if (File.Exists(localPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(localPath);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.Name == �����½ڵ�)
                    {
                        xe.SetAttribute(�����½ڵ�, ������ֵ);
                        break;
                    }
                }
                xmlDoc.Save(localPath);
            }
        }

        public string ��������_StreamingAssets (string ����ȡ�ڵ�, string �ļ���)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(ReadXmlFromStreamingAssets(�ļ���));
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.Name == ����ȡ�ڵ�)
                {
                    return xe.GetAttribute(����ȡ�ڵ�);
                }
            }
            return ("None");
        }

        public static string ReadXmlFromStreamingAssets (string xmlFileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, xmlFileName);

            if (Application.platform == RuntimePlatform.Android)
            {
                UnityWebRequest www = UnityWebRequest.Get(filePath);
                www.SendWebRequest();

                while (!www.isDone) { }

                return www.downloadHandler.text;
            }
            else
            {
                return File.ReadAllText(filePath);
            }
        }
    }
}