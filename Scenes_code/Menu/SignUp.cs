using config;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour
{
    //ע���ʽ��SignUp&email&password&name
    private ConfigXML configXML = new ConfigXML();

    public InputField email;
    public InputField password;
    public Toggle toggle;
    public Text Image_VerificationCode;
    private bool SignUpEn;//�Ƿ�ͬ�������ע���˺�
    private bool bool_VerificationCode;//�Ƿ�ͨ����֤��
    private string VerificationCode = "";//��֤��
    public GameObject Panel_Code;
    public InputField input_VerificationCode;

    public void SetButtonState ()
    {
        Server.LoginUIState = 2;
        if (toggle.isOn) SignUpEn = true;
        else SignUpEn = false;
        Debug.Log("��ǰ�Ƿ���ע��:   " + toggle.isOn.ToString());
    }

    public void Button_click_VerificationCode ()
    {
        if (input_VerificationCode.text.ToLower() == VerificationCode.ToLower())
        {
            // ��֤����ȷ
            GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(true);
            bool_VerificationCode = true;
            Panel_Code.SetActive(false);
        }
        else
        {
            new PopNewMessage("��֤�����");
        }
    }

    public void SetReturnOK ()
    {
        GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(true);
    }

    public async void Button_click_SignUp ()
    {
        if (SignUpEn)//ͬ��������
        {
            if (bool_VerificationCode == false)
            {
                GameObject.Find("SignCanvas/SignUp/SignUpBack/Button/return").SetActive(false);
                VerificationCode = API.AccountAPI.CreatNew_VerificationCode();
                Image_VerificationCode.GetComponent<Text>().text = VerificationCode;
                Panel_Code.SetActive(true);
                return;
            }
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
                        new PopNewMessage("���ӳ�ʱ,������");
                    });
                    if (tcpClient.IsConnected())
                    {
                        string response = await tcpClient.GetAsync(API.AccountAPI.SplicingStatements("SignUp", _email, md5password));
                        Debug.Log(response);
                        //-1��ע�����
                        //-2:ע��ʧ��
                        //>=10000��ע��ɹ�
                        _showLoading.KillLoading();
                        if (Convert.ToInt32(response) > 10000)
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
                            GameObjectEvent _gameObject = new GameObjectEvent();
                            _gameObject.FindInactiveObjectByName("��ʼ��Ϸ").SetActive(true);
                            _gameObject.FindInactiveObjectByName("ExitUser").SetActive(true);
                            UsersInfo.UsersInfo.UpdateUID_Scene();
                            this.transform.parent.transform.parent.transform.gameObject.transform.parent.transform.gameObject.SetActive(false);
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            //new NewMessage();
                            switch (response)
                            {
                                case "-1":
                                    new PopNewMessage("�����Ѿ�ע��");
                                    break;

                                case "-2":
                                    new PopNewMessage("ע��ʧ��");
                                    break;

                                default:
                                    new PopNewMessage("δ֪����");
                                    break;
                            }
                        }
                    }
                    else { new PopNewMessage("���ӷ�����ʧ��"); }
                }
                else
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
            new PopNewMessage("����ͬ��������ע��");
        }
    }

    public void UpdateCode ()
    {
        VerificationCode = API.AccountAPI.CreatNew_VerificationCode();
        Image_VerificationCode.GetComponent<Text>().text = VerificationCode;
    }
}