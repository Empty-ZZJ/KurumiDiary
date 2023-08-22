using config;
using System;
using System.IO;
using TMPro;
using UnityEngine;

namespace UsersInfo
{
    public class UsersInfo : MonoBehaviour
    {
        private static ConfigXML configXML = new ConfigXML();
        private static GameObject StaticGameObj;

        public static void UpdateUserID_Scene ()
        {
            var _ = new GameObjectEvent();
            if (_.FindInactiveObjectByName("StaticGameObj") == null)
            {
                {
                    StaticGameObj = Instantiate(Resources.Load<GameObject>("Prefabs/StaticGameObj"));
                    DontDestroyOnLoad(StaticGameObj);
                }
                Debug.Log("�����ˣ�" + "StaticGameObj");
            }
            if (!File.Exists(Application.persistentDataPath + "/playerinformation.xml"))
            {
                configXML.���������ļ�("UID", "local", "playerinformation.xml");
                configXML.����������("Login", "False", "playerinformation.xml");
            }
        }

        public static bool UpdateUID_Scene ()
        {
            try
            {
                GameObject _UID = StaticGameObj;
                if (configXML.��������("Login", "playerinformation.xml") == "True")
                {
                    _UID.GetComponentInChildren<TextMeshProUGUI>().text = "UID:" + configXML.��������("UID", "playerinformation.xml");
                }
                else
                {
                    _UID.GetComponentInChildren<TextMeshProUGUI>().text = "�����û�";
                }
                return true;
            }
            catch (Exception ex)
            {
                new PopNewMessage(ex.Message);
                return false;
            }
        }
    }
}