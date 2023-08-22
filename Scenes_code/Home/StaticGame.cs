using config;
using TMPro;
using UnityEngine;

public class StaticGame : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();
    public TextMeshProUGUI txt;

    private void Awake ()
    {
        //DontDestroyOnLoad(this.gameObject);

        if (configXML.��������("Login", "playerinformation.xml") == "True")
        {
            txt.text = "UID:" + configXML.��������("UID", "playerinformation.xml");
        }
        else
        {
            txt.text = "�����û�";
        }
    }
}