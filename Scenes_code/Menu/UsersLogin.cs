using config;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsersLogin : MonoBehaviour
{
    public GameObject Login;
    public GameObject StartGame;
    public GameObject exitUser;
    private ConfigXML configXML = new ConfigXML();

    private void Awake ()
    {
        UsersInfo.UsersInfo.UpdateUserID_Scene();
        if (configXML.��������("Login", "playerinformation.xml") == "None")
        {
            configXML.����������("Login", "False", "playerinformation.xml");
        }
        else if (configXML.��������("Login", "playerinformation.xml") == "True")
        {
            Login.SetActive(false);
            StartGame.SetActive(true);
            exitUser.SetActive(true);
            if (configXML.��������("UID", "playerinformation.xml") != "local")
                new PopNewMessage($"��ӭ��{configXML.��������("UID", "playerinformation.xml")}");
            else { new PopNewMessage("��ӭ���������û�"); }
        }
    }

    public void ���ص�¼ ()
    {
        configXML.����������("UID", "local", "playerinformation.xml");
        UsersInfo.UsersInfo.UpdateUID_Scene();
        configXML.����������("Login", "True", "playerinformation.xml");
        var _gameObject = new GameObjectEvent();
        _gameObject.FindInactiveObjectByName("ExitUser").SetActive(true);
    }

    public void ExitUser ()
    {
        exitUser.SetActive(false);
        Login.SetActive(true);
        StartGame.SetActive(false);
        configXML.����������("UID", "local", "playerinformation.xml");
        configXML.����������("Login", "False", "playerinformation.xml");
        UsersInfo.UsersInfo.UpdateUID_Scene();
    }

    public void CoreStartGame ()
    {
        if (!File.Exists(Application.persistentDataPath + "/playerinformation.xml"))
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Account/NameSet"));
            return;
        }
        else if (configXML.��������("name", "playerinformation.xml") == "None")
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Account/NameSet"));
            return;
        }
        else
        {
            GameObject load = Resources.Load<GameObject>("Prefabs/UI/LoadingEvent");
            Instantiate(load);
            StartCoroutine(start_game());
            AsyncOperation operation = new AsyncOperation();
            IEnumerator start_game ()
            {
                operation = SceneManager.LoadSceneAsync(1);
                yield return operation;
            }
        }
    }
}