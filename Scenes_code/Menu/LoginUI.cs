using config;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    //��¼��ʽ��Login&email&password
    public ConfigXML configXML = new ConfigXML();

    public Toggle toggle;
    public InputField email;
    public InputField password;
    private bool LoginEn = false;//�Ƿ�ͬ���������½��Ϸ

    public void SetButtonState ()
    {
        Server.LoginUIState = 1;
        if (toggle.isOn) LoginEn = true;
        else LoginEn = false;
        Debug.Log("��ǰ�Ƿ�����¼:   " + toggle.isOn.ToString());
    }

    public async void Button_click_Login ()
    {
        if (LoginEn)
        {
            var waita = new WaitLoadGIf();
            AsyncTcpClient tcpClient = new AsyncTcpClient();
            string _email = email.text;
            try
            {
                if (API.AccountAPI.CheckEmail(ref _email) == "�����ַ�Ϸ�")
                {
                    ShowLoading _showLoading = new ShowLoading();
                    string md5password = API.EncodingAction.GetShA256(password.text);
                    Debug.Log(md5password);
                    await tcpClient.ConnectAsync(Server.IP, Server.port, 3000, () =>
                    {
                        //��ʱ
                        _showLoading.KillLoading();
                        new PopNewMessage("���ӳ�ʱ");
                    });
                    if (tcpClient.IsConnected())
                    {
                        string response = await tcpClient.GetAsync(API.AccountAPI.SplicingStatements("Login", _email, md5password));
                        Debug.Log(response);
                        _showLoading.KillLoading();

                        if (response != "û���ҵ����û�")
                        {
                            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
                            if (regex.IsMatch(response))
                            {
                                new PopNewMessage("��ӭ: " + response);//��¼�ɹ������ص�response���ǵ�¼��UID
                                if (configXML.��������("UID", "playerinformation.xml") != "None")
                                {
                                    configXML.����������("UID", response, "playerinformation.xml");
                                    configXML.����������("Login", "True", "playerinformation.xml");
                                }
                                else
                                {
                                    configXML.����������("UID", response, "playerinformation.xml");
                                    configXML.����������("Login", "True", "playerinformation.xml");
                                }

                                GameObject.Find("MainCanvas/Button_Game_In/GameObject").SetActive(false);
                                var _gameObject = new GameObjectEvent();
                                _gameObject.FindInactiveObjectByName("��ʼ��Ϸ").SetActive(true);
                                _gameObject.FindInactiveObjectByName("ExitUser").SetActive(true);
                                UsersInfo.UsersInfo.UpdateUID_Scene();
                                Destroy(this.gameObject);
                                this.transform.parent.transform.parent.transform.parent.transform.gameObject.SetActive(false);
                            }
                            else
                            {
                                new PopNewMessage("�������������쳣����");
                            }
                        }
                        else { new PopNewMessage("�˺Ż�����������������"); }
                    }
                    else { new PopNewMessage("���ӷ�����ʧ��"); }
                }
                else//���Ϸ�
                {
                    new PopNewMessage("�����ַ���Ϸ�");
                }
            }
            catch (Exception ex) { new PopNewMessage(ex.Message); }
            finally
            {
                waita.Exit();
                tcpClient.Disconnect();
                Destroy(tcpClient);
            }
        }
        else
        {
            new PopNewMessage("����ͬ������");
        }
    }

    public void SetReturn (bool _a)
    {
        GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(_a);
    }
}