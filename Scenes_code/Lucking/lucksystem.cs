using config;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class lucksystem : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public GameObject txt_coin;
    public static long nowcoin;
    public static long frequency;

    public void Start ()
    {
        if (File.Exists(Application.persistentDataPath + "/coinsystem.xml") == false)
        {
            configXML.���������ļ�("nowcoin", "0", "coinsystem.xml");
            configXML.����������("frequency", "0", "coinsystem.xml");
        }
        else
        {
            Debug.Log(configXML.��������("nowcoin", "coinsystem.xml"));
            Debug.Log(configXML.��������("frequency", "coinsystem.xml"));
        }
    }

    public void FixedUpdate ()
    {
        long.TryParse(configXML.��������("nowcoin", "coinsystem.xml"), out nowcoin);
        long.TryParse(configXML.��������("frequency", "coinsystem.xml"), out frequency);
        txt_coin.GetComponent<Text>().text = nowcoin.ToString();
    }

    public void add_coin (long wantadd)
    {
        long temp;
        long.TryParse(configXML.��������("nowcoin", "coinsystem.xml"), out temp);
        long finallycoin = temp + wantadd;
        configXML.����������("nowcoin", finallycoin.ToString(), "coinsystem.xml");
    }

    public string find_coin ()
    {
        return configXML.��������("nowcoin", "coinsystem.xml");
    }

    /// <summary>
    /// ����math_add_frequency���齱������С��100ʱ����False����100ʱ�Զ����㣬���ҷ���һ��True��
    /// </summary>
    public bool add_frequency (int math_add_frequenc)
    {
        long temp;
        long.TryParse(configXML.��������("frequency", "coinsystem.xml"), out temp);
        if ((temp + math_add_frequenc) >= 100)
        {
            configXML.����������("frequency", (temp + math_add_frequenc - 100).ToString(), "coinsystem.xml");
            return true;
        }
        else
        {
            configXML.����������("frequency", (temp + math_add_frequenc).ToString(), "coinsystem.xml");
            return false;
        }
    }

    /// <summary>
    /// �ú�����ֹͣʹ�á�
    /// </summary>
    /// <param name="wantreduce"></param>
    /// <returns></returns>
    public static bool reduce_coin (long wantreduce)
    {
        long temp;
        long finallycoin;
        long.TryParse(plotbutton.ReadXML("nowcoin", "nowcoin", "coinsystem.xml"), out temp);
        if (wantreduce <= temp)
        {
            finallycoin = temp - wantreduce;
            plotbutton.UpdateXml("nowcoin", plotbutton.ReadXML("nowcoin", "nowcoin", "coinsystem.xml"), finallycoin.ToString(), "coinsystem.xml");
            return true;
        }
        return false;
    }

    /// <summary>
    /// �ú�����ֹͣʹ�á�
    /// </summary>
    /// <param name="wantadd"></param>
    /// <returns></returns>
    public static bool rest_frequency (int wantadd)
    {
        int temp;
        int.TryParse(plotbutton.ReadXML("frequency", "frequency", "coinsystem.xml"), out temp);
        if (temp + wantadd < 100)
        {
            int finallycoin = temp + wantadd;
            plotbutton.UpdateXml("frequency", plotbutton.ReadXML("frequency", "frequency", "coinsystem.xml"), finallycoin.ToString(), "coinsystem.xml");
            return false;
        }
        else
        {
            plotbutton.UpdateXml("frequency", plotbutton.ReadXML("frequency", "frequency", "coinsystem.xml"), "0", "coinsystem.xml");
            return true;
        }
    }
}