using UnityEngine;
using UnityEngine.UI;

public class CheckUpdate : MonoBehaviour
{
    private AsyncTcpClient tcpClient;
    public GameObject F_Progress;
    public GameObject F_whatnew;

    private async void Start ()
    {
        if (IsNetworkReachability())
        {
            F_Progress.SetActive(true);

            start_code.countloading = 10;
            tcpClient = new AsyncTcpClient();

            /*
            await tcpClient.ConnectAsync("103.228.170.38", 800, 3000, () =>
            {
                PopError();
            });*/
            await tcpClient.ConnectAsync(Server.IP, Server.port, 3000, () =>
            {
                PopError();
            });
            start_code.countloading = 20;
            F_Progress.GetComponentInChildren<Text>().text = "���ӵ����������ڼ�����";
            if (tcpClient.IsConnected())
            {
                // ���ӳɹ���������Ϣ�����ջظ�
                string response = await tcpClient.GetAsync("Ver");
                F_Progress.GetComponentInChildren<Text>().text = $"��⵽�汾����   ��ǰ�汾��: {Application.version}   �������汾��: {response}";
                start_code.countloading = 30;
                if (Application.version != response)
                {
                    start_code.countloading = 40;
                    F_whatnew.SetActive(true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
                start_code.countloading = 50;
                // �Ͽ�����

                tcpClient.Disconnect();
            }
            else
            {
                PopError();
                Debug.LogError("Failed to connect to server.");
            }
        }
        else
        {
            PopError();
        }
    }

    public void PopError ()
    {
        GameObject pop = (GameObject)Resources.Load("Prefabs/UI/ScenesUI/intetneterro");
        Instantiate(pop, this.transform);
    }

    protected bool IsNetworkReachability ()
    {
        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                Debug.Log("��ǰʹ�õ��ǣ�WiFi��");
                return true;

            case NetworkReachability.ReachableViaCarrierDataNetwork:
                Debug.Log("��ǰʹ�õ��ǣ��ƶ����磡");
                return true;

            default:
                Debug.Log("��ǰû���������������������ٽ��в�����");
                return false;
        }
    }
}