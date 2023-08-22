using config;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoveDegree : MonoBehaviour
{
    private static ConfigXML configXML = new ConfigXML();
    public GameObject Obj_lovedegree;
    public GameObject text_lovedegree;
    public static int loveValue;
    public static int loveLevel;

    private void Start ()
    {
        if (!File.Exists(Application.persistentDataPath + "/lovedegree.xml"))
        {
            configXML.���������ļ�("loveValue", "0", "lovedegree.xml");
            configXML.����������("loveLevel", "0", "lovedegree.xml");
        }
        else
        {
            int.TryParse(configXML.��������("loveValue", "lovedegree.xml"), out loveValue);
            int.TryParse(configXML.��������("loveLevel", "lovedegree.xml"), out loveLevel);
        }
    }

    public void FixedUpdate ()
    {
        int temp = 0;
        int.TryParse(configXML.��������("loveValue", "lovedegree.xml"), out temp);
        UpdateLovedegreeLevel();
        string _level = configXML.��������("loveLevel", "lovedegree.xml");
        if (_level != "None")
        {
            text_lovedegree.GetComponent<Text>().text = _level;
        }
        else text_lovedegree.GetComponent<Text>().text = 0.ToString();
        Obj_lovedegree.GetComponent<Image>().fillAmount = temp / 100f;
    }

    public static void AddLoveValue (int value)
    {
        loveValue += value;
        if (loveValue >= 100 && loveLevel < 10)
        {
            loveLevel++;
            loveValue -= 100;
        }
        else if (loveLevel == 10 && loveValue > 100)
        {
            loveValue = 100;
        }
        UpdateLovedegreeLevel();
    }

    public static void UpdateLovedegreeLevel ()
    {
        configXML.����������("loveValue", loveValue.ToString(), "lovedegree.xml");
        configXML.����������("loveLevel", loveLevel.ToString(), "lovedegree.xml");
    }

    public static int GetLoveDegree ()
    {
        return loveLevel;
    }

    public static int GetLoveValue ()
    {
        return loveValue;
    }
}