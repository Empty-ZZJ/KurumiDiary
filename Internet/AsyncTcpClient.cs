using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncTcpClient : MonoBehaviour
{
    private TcpClient client;
    private byte[] buffer = new byte[1024];
    private string response;

    // ���ӵ�������
    public async Task ConnectAsync (string ipAddress, int port, int timeoutInMs, Action onTimeout = null)
    {
        try
        {
            client = new TcpClient();
            var connectTask = client.ConnectAsync(ipAddress, port);

            if (await Task.WhenAny(connectTask, Task.Delay(timeoutInMs)) != connectTask)
            {
                // ���ӳ�ʱ
                if (onTimeout != null)
                    onTimeout();
                return;
            }

            await connectTask;
            Debug.Log("Connected to server.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to connect to server: {e}");
        }
    }

    // ������Ϣ�����ջظ�
    public async Task<string> GetAsync (string message)
    {
        if (client == null || !client.Connected)
        {
            Debug.LogError("Not connected to server.");
            return null;
        }

        try
        {
            // ������Ϣ
            NetworkStream stream = client.GetStream();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            await stream.WriteAsync(data, 0, data.Length);

            // �첽���ջظ�
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            response = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return response;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to send/receive message: {e}");
            return null;
        }
    }

    // �Ͽ�����
    public void Disconnect ()
    {
        if (client != null)
        {
            client.Close();
            Debug.Log("Disconnected from server.");
        }
    }

    // �ڽű�������ʱ�Ͽ�����
    private void OnDisable ()
    {
        Disconnect();
    }

    public bool IsConnected ()
    {
        return client != null && client.Connected;
    }
}