using config;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class plotbutton : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();

    // Start is called before the first frame update
    private AsyncOperation operation;

    public GameObject message;

    public GameObject mask;
    public GameObject list;
    public GameObject right_txt;
    public GameObject right_detail;
    public GameObject right_photo;
    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock1_txt;
    public GameObject lock2_txt;

    public void closelist ()
    {
        UIAnimate.ToButtom(list);
        mask.SetActive(false);
        lock1.SetActive(false);
        lock2.SetActive(false);
    }

    public void onda ()
    {
        int i;
        string[] qwe = new string[5];
        string temptxt = this.transform.name;
        qwe = temptxt.Split('t');
        int.TryParse(qwe[1], out i);
        if (i > 1 && i < 13)
        {
            UIAnimate.ToMiddle(message);
            return;
        }
        mask.SetActive(true);
        UIAnimate.ToMiddle(list);
        right_txt.GetComponent<Text>().text = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + i.ToString() + "/title").GetComponent<Text>().text;
        right_detail.GetComponent<Text>().text = configXML.��������_StreamingAssets("plot" + i.ToString(), "config/plot/plotdetail.xml");
        right_photo.GetComponent<Image>().sprite = GameObject.Find("Canvas/Horizontal Scroll View/Viewport/Content/choicewhat" + i.ToString() + "/plotphoto").GetComponent<Image>().sprite;
        if (File.Exists(Application.persistentDataPath + "/plotdone.xml") == true)
        {
            string temp = configXML.��������("plot" + i.ToString(), "plotdone.xml");
            if (temp == "None")
            {
                configXML.����������("plot" + i.ToString(), "false", "plotdone.xml");
            }
            else if (temp == "true")
            {
                lock1.SetActive(true);
                lock2.SetActive(true);
            }
        }
        else
        {
            configXML.���������ļ�("plot" + i.ToString(), "false", "plotdone.xml");
        }

        if (File.Exists(Application.persistentDataPath + "/plotset.xml") == false)
        {
            configXML.���������ļ�("nowplot", i.ToString(), "plotset.xml");
        }
        else
            configXML.����������("nowplot", i.ToString(), "plotset.xml");
        string[] tempprize = new string[5];
        string temp2;
        temp2 = configXML.��������_StreamingAssets("plot" + i.ToString(), "config/plot/plotprize.xml");
        tempprize = temp2.Split("|");
        {
            lock1_txt.GetComponent<Text>().text = "��ʯ��" + tempprize[0];
            lock2_txt.GetComponent<Text>().text = "�øжȡ�" + tempprize[1];
        }
    }

    public void plot_run ()
    {
        StartCoroutine(start_game());
    }

    public static void CreateXML (string chilenode, string a1, string a2, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (!File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", "");//����xml�ļ������ʽΪUTF-8
            XmlElement root = xml.CreateElement("root");//�������ڵ�
            XmlElement info = xml.CreateElement(chilenode);//�����ӽڵ�
            info.SetAttribute(a1, a2);//�����ӽڵ�������������ֵ
            root.AppendChild(info);//���ӽڵ㰴�մ���˳����ӵ�xml
            xml.AppendChild(root);
            xml.Save(localPath);//����xml��·��λ��
        }
    }

    public static void AddXML (string a1, string a2, string newson, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);//����xml�ļ�
            XmlNode root = xml.SelectSingleNode("root");//��ȡ���ڵ�
            XmlElement info = xml.CreateElement(newson);//�����µ��ӽڵ�
            info.SetAttribute(a1, a2);//�������ӽڵ�������������ֵ
            root.AppendChild(info);//���ӽڵ㰴�մ���˳����ӵ�xml
            xml.AppendChild(root);
            xml.Save(localPath);//����xml��·��λ��
        }
    }

    public static string ReadXML (string chilenode, string want, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);//����xml
            XmlNodeList nodeList = xml.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {//�����ӽڵ�
                if (xe.Name == chilenode)
                {
                    return (xe.GetAttribute(want));
                }
            }
        }
        return ("None");
    }

    public static string ReadXML_streaming (string chilenode, string want, string alie)
    {
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(localPath);//����xml
            XmlNodeList nodeList = xml.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {//�����ӽڵ�
                if (xe.Name == chilenode)
                {
                    return (xe.GetAttribute(want));
                }
            }
        }
        return ("None");
    }

    public static void UpdateXml (string a, string b, string c, string alie)
    {
        string localPath = UnityEngine.Application.persistentDataPath + "/" + alie;
        if (File.Exists(localPath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(localPath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                //�õ��ڵ�������Name =С���Ľڵ�
                if (xe.GetAttribute(a) == b)
                {
                    //���½ڵ�����
                    xe.SetAttribute(a, c);
                    break;
                }
            }
            xmlDoc.Save(localPath);
        }
    }

    private IEnumerator start_game ()
    {
        operation = SceneManager.LoadSceneAsync(3);
        yield return operation;
    }
}