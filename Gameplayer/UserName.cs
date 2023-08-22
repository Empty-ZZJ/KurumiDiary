using config;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public InputField txtname;
    private AsyncOperation operation;

    public void buttonon ()
    {
        txtname.text = this.transform.name;
    }

    public void custom_name ()
    {
        string wantname = txtname.text;

        if (bool_name_true(wantname))
        {
            if (File.Exists(Application.persistentDataPath + "/playerinformation.xml") == true)
            {
                if (configXML.��������("name", "playerinformation.xml") == "None")
                {
                    configXML.����������("name", wantname, "playerinformation.xml");
                }
                else
                {
                    configXML.����������("name", wantname, "playerinformation.xml");
                }
            }
            else
            {
                configXML.���������ļ�("name", wantname, "playerinformation.xml");
            }
            Destroy(this.gameObject.transform.parent.transform.parent.gameObject);
        }
        else
        {
            new PopNewMessage("�����ǳƲ��Ϸ�");
        }
    }

    /// <summary>
    /// ����û����Ƿ���Ч
    /// </summary>
    /// <param name="boolname"></param>
    /// <returns></returns>
    private bool bool_name_true (string boolname)
    {
        if (boolname == "None")
        {
            return false;
        }
        // ����û��������Ƿ�С�ڵ���1
        if (boolname.Length <= 1)
        {
            return false;
        }

        // ����Ƿ���������ַ�����������ַ�
        for (int i = 0; i < boolname.Length; i++)
        {
            if (!char.IsLetterOrDigit(boolname[i]) && boolname[i] != '_')
            {
                return false;
            }
        }

        // ���û���ֻ��һ���ַ�ʱ��������Ƿ��Ǵ�����
        if (boolname.Length == 1 && char.IsDigit(boolname[0]))
        {
            return false;
        }

        // �û�����Ч
        return true;
    }

    public static void Update_name (string wantname)
    {
        if (File.Exists(Application.persistentDataPath + "/playerinformation.xml") == true)
        {
            if (plotbutton.ReadXML("name", "name", "playerinformation.xml") == "None")
            {
                plotbutton.AddXML("name", wantname, "name", "playerinformation.xml");
            }
            else
            {
                plotbutton.UpdateXml("name", plotbutton.ReadXML("name", "name", "playerinformation.xml"), wantname, "playerinformation.xml");
            }
        }
    }

    public static string Get_name ()
    {
        if (File.Exists(Application.persistentDataPath + "/playerinformation.xml") == true)
        {
            if (plotbutton.ReadXML("name", "name", "playerinformation.xml") == "None")
            {
                return "None";
            }
            else
            {
                return plotbutton.ReadXML("name", "name", "playerinformation.xml");
            }
        }
        else return "None";
    }
}