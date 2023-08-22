using DG.Tweening;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class button : MonoBehaviour
{
    public GameObject mainui;
    public GameObject videoplayer;
    public GameObject ShowCanvas;
    public GameObject none_obj;
    private VideoClip start;
    private VideoClip blue;
    private VideoClip yellow;
    private VideoClip white;

    public GameObject maincmaera;
    public GameObject fudai;
    private Tweener event_creat_one;
    public GameObject obj_text;
    public GameObject obj_photo;
    public GameObject obj_change;

    public bool nowluck;
    public bool bool_buttonluck;
    public int mode;

    public void Start ()
    {
        Input.multiTouchEnabled = true;
        start = Resources.Load<VideoClip>("movie/start");
        blue = Resources.Load<VideoClip>("movie/blue");
        yellow = Resources.Load<VideoClip>("movie/yellow");
        white = Resources.Load<VideoClip>("movie/white");
    }

    private void Update ()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
        {
            if (nowluck == true && bool_buttonluck == true)
            {
                if (mode == 1)
                {
                    one_Judgment_value();
                }
                else if (mode == 10)
                {
                    Ten_Judgment_value();
                }
            }
        }
        return;
    }

    public void Ten_Judgment_value ()
    {
        bool_buttonluck = false;
        if (lucksystem.frequency! < 100)
        {
            videoplayer.GetComponent<VideoPlayer>().clip = blue;
            videoplayer.GetComponent<VideoPlayer>().Play();
            videoplayer.GetComponent<VideoPlayer>().loopPointReached += shouboj_blue;
        }
        else
        {
            videoplayer.GetComponent<VideoPlayer>().clip = yellow;
            videoplayer.GetComponent<VideoPlayer>().Play();
            videoplayer.GetComponent<VideoPlayer>().loopPointReached += shouboj_yellow;
        }
    }

    public void one_Judgment_value ()
    {
        Debug.Log("�жϼ�ֵ");
        bool_buttonluck = false;
        if ((lucksystem.frequency + 1) % 10 == 0)
        {
            if (lucksystem.frequency + 1 == 100)
            {
                videoplayer.GetComponent<VideoPlayer>().clip = yellow;
                videoplayer.GetComponent<VideoPlayer>().Play();
                videoplayer.GetComponent<VideoPlayer>().loopPointReached += shouboj_yellow;
                lucksystem.rest_frequency(1);
                return;
            }
            else
            {
                videoplayer.GetComponent<VideoPlayer>().clip = blue;
                videoplayer.GetComponent<VideoPlayer>().Play();
                videoplayer.GetComponent<VideoPlayer>().loopPointReached += shouboj_blue;
                lucksystem.rest_frequency(1);
                return;
            }
        }
        else
        {
            videoplayer.GetComponent<VideoPlayer>().clip = white;
            videoplayer.GetComponent<VideoPlayer>().Play();
            videoplayer.GetComponent<VideoPlayer>().loopPointReached += shouboj_white;
            lucksystem.rest_frequency(1);
        }
    }

    public void shouboj_blue (VideoPlayer video)
    {
        Debug.Log("blue");
        bool_buttonluck = false;
        ShowCanvas.SetActive(true);
        int temp = NewRandom.GetRandomInAB(1, 2);//[1,3)
        string fiannly = "KOTATSU-" + temp.ToString();
        Debug.Log(fiannly + "   " + fine_obj(fiannly, "blue"));
        if (fine_obj(fiannly, "blue") == false)
        {
            obj_text.GetComponent<Text>().text = ReadXml_Obj(fiannly, fiannly, "blue.xml");
            obj_photo.GetComponent<Image>().sprite = Resources.Load<Sprite>("luck/homescences/blue/" + fiannly);
            plotbutton.UpdateXml("blue-" + fiannly, plotbutton.ReadXML("blue-" + fiannly, "blue-" + fiannly, "bag.xml"), "true", "bag.xml");
        }
        else
        {
            obj_text.GetComponent<Text>().text = ReadXml_Obj(fiannly, fiannly, "blue.xml");
            obj_photo.GetComponent<Image>().sprite = Resources.Load<Sprite>("luck/homescences/blue/" + fiannly);
            obj_change.SetActive(true);
            //lucksystem.add_coin(5);
        }
    }

    public void shouboj_yellow (VideoPlayer video)
    {
        Debug.Log("yellow");
        bool_buttonluck = false;
        ShowCanvas.SetActive(true);
        int temp = NewRandom.GetRandomInAB(1, 2);//[1,3)
        string fiannly = "KOTATSU-" + temp.ToString();
        Debug.Log(fiannly + "   " + fine_obj(fiannly, "yellow"));
        if (fine_obj(fiannly, "yellow") == false)
        {
            obj_text.GetComponent<Text>().text = ReadXml_Obj(fiannly, fiannly, "yellow.xml");
            obj_photo.GetComponent<Image>().sprite = Resources.Load<Sprite>("luck/homescences/yellow/" + fiannly);
            plotbutton.UpdateXml("yellow-" + fiannly, plotbutton.ReadXML("yellow-" + fiannly, "yellow-" + fiannly, "bag.xml"), "true", "bag.xml");
        }
        else
        {
            obj_text.GetComponent<Text>().text = ReadXml_Obj(fiannly, fiannly, "yellow.xml");
            obj_photo.GetComponent<Image>().sprite = Resources.Load<Sprite>("luck/homescences/yellow/" + fiannly);
            obj_change.SetActive(true);
            // lucksystem.add_coin(10);
        }
    }

    public void shouboj_white (VideoPlayer video)
    {
        Debug.Log("white");
        bool_buttonluck = false;
        ShowCanvas.SetActive(true);
        int temp = NewRandom.GetRandomInAB(1, 3);//[1,3)
        string fiannly = "KOTATSU-" + temp.ToString();
        if (fine_obj(fiannly, "white") == false)
        {
            obj_text.GetComponent<Text>().text = ReadXml_Obj(fiannly, fiannly, "white.xml");
            obj_photo.GetComponent<Image>().sprite = Resources.Load<Sprite>("luck/homescences/white/" + fiannly);
            plotbutton.UpdateXml("white-" + fiannly, plotbutton.ReadXML("white-" + fiannly, "white-" + fiannly, "bag.xml"), "true", "bag.xml");
        }
        else
        {
            obj_text.GetComponent<Text>().text = ReadXml_Obj(fiannly, fiannly, "white.xml");
            obj_photo.GetComponent<Image>().sprite = Resources.Load<Sprite>("luck/homescences/white/" + fiannly);
            obj_change.SetActive(true);
            // lucksystem.add_coin(1);
        }
    }

    public void uidown ()
    {
        mainui.SetActive(false);
    }

    public void uion ()
    {
        mainui.SetActive(true);
    }

    public void videodown ()
    {
        videoplayer.SetActive(false);
    }

    public void videoon ()
    {
        videoplayer.SetActive(true);
    }

    public void Single_extraction ()
    {
        if (lucksystem.nowcoin >= 30)
        {
            mode = 1;
            nowluck = true;
            uidown();
            videoon();
            videoplayer.GetComponent<VideoPlayer>().clip = start;
            videoplayer.GetComponent<VideoPlayer>().Play();
            videoplayer.GetComponent<VideoPlayer>().loopPointReached += overpaly;
            lucksystem.reduce_coin(30);
        }
        else
        {
            Debug.Log("Ǯ����");
            DOTween.To(() => none_obj.GetComponent<RectTransform>().anchoredPosition, x => none_obj.GetComponent<RectTransform>().anchoredPosition = x, new Vector2(0, 0), 0.5f);
        }
    }

    private void overpaly (VideoPlayer video)
    {
        Debug.Log("�������");
        bool_buttonluck = true;
    }

    /// <summary>
    /// ����У�����true
    /// ���û�У�����bool�����Ҹ������ݱ�Ϊ��
    /// </summary>
    /// <param name="object_name"></param>
    public bool fine_obj (string object_name, string value)//value��0 1 2
    {
        if (File.Exists(Application.persistentDataPath + "/bag.xml") == true)
        {
            Debug.Log(plotbutton.ReadXML(value + "-" + object_name, value + "-" + object_name, "bag.xml") == "None");
            if (plotbutton.ReadXML(value + "-" + object_name, value + "-" + object_name, "bag.xml") == "None")
            {
                plotbutton.AddXML(value + "-" + object_name, "false", value + "-" + object_name, "bag.xml");
                return false;
            }
            else
            {
                if (plotbutton.ReadXML(value + "-" + object_name, value + "-" + object_name, "bag.xml") == "false")
                {
                    return false;
                }
                else return true;
            }
        }
        else
        {
            plotbutton.CreateXML(value + "-" + object_name, value + "-" + object_name, "false", "bag.xml");
            return false;
        }
    }

    public void Ten_extraction ()
    {
        if (lucksystem.nowcoin >= 900000)
        {
            mode = 10;
            nowluck = true;
            uidown();
            videoon();
            videoplayer.GetComponent<VideoPlayer>().clip = start;
            videoplayer.GetComponent<VideoPlayer>().Play();
            videoplayer.GetComponent<VideoPlayer>().loopPointReached += overpaly;
            lucksystem.reduce_coin(300);
        }
        else
        {
            none_obj.transform.DOMove(new Vector3(720f, 405f, 0f), 1);
        }
    }

    public void creat_one ()
    {
        event_creat_one = UIAnimate.ToMiddle(fudai);
    }

    public void F_camera ()
    {
        maincmaera.transform.DOShakePosition(0.3f, 0.05f);
    }

    public void CreateXML_Obj (string chilenode, string a1, string a2, string alie)
    {
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + alie;
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

    public string ReadXml_Obj (string chilenode, string want, string alie)
    {
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + alie;
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
        return ("None");
    }

    public void UpdateXml_Obj (string a, string b, string c, string alie)
    {
        string localPath = UnityEngine.Application.streamingAssetsPath + "/" + alie;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(localPath);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
        foreach (XmlElement xe in nodeList)
        {
            //�õ��ڵ�������
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