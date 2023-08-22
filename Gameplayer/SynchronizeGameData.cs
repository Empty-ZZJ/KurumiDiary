using System;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ͬ����Ϸ����
/// </summary>
public static class SynchronizeGameData
{
    /// <summary>
    /// ����ǰ�˻�����Ϸ���ݴ����һ��hoilai�ļ�
    /// </summary>
    /// <returns></returns>
    public static bool CompressGameData ()
    {
        try
        {
            string dataPath = Application.persistentDataPath;
            string zipFilePath = Path.Combine(dataPath, "GameTempData.hoilai");

            // ��ȡ.dataPathĿ¼�����е�.xml�ļ�
            string[] xmlFiles = Directory.GetFiles(dataPath, "*.xml");

            // ���û���ҵ��κ�.xml�ļ����������ѹ��
            if (xmlFiles.Length == 0)
            {
                Debug.Log("δ�ҵ�ѹ���ļ�");
                return false;
            }

            using (FileStream zipToCreate = new FileStream(zipFilePath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                {
                    foreach (string xmlFilePath in xmlFiles)
                    {
                        // ��ÿ��.xml�ļ���ӵ�ѹ������
                        string xmlFileName = Path.GetFileName(xmlFilePath);
                        archive.CreateEntryFromFile(xmlFilePath, xmlFileName);
                    }
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            return false;
        }
    }

    public static long GetFileSize (string sFullName)
    {
        long lSize = 0;
        if (File.Exists(sFullName))
            lSize = new FileInfo(sFullName).Length;
        return lSize;
    }

    /// <summary>
    /// ��һ��hoilai�ļ�ͬ��������
    /// </summary>
    /// <returns></returns>
    public static bool UpdateGameData (string zipFilePath)
    {
        try
        {
            Debug.Log(GetFileSize(zipFilePath));
            if (GetFileSize(zipFilePath) <= 0)
            {
                return false;
            }
            string dataPath = Application.persistentDataPath;

            // ɾ�� Application.persistentDataPath �µ�����.xml�ļ�
            string[] xmlFiles = Directory.GetFiles(dataPath, "*.xml");
            foreach (string xmlFilePath in xmlFiles)
            {
                File.Delete(xmlFilePath);
            }

            using (FileStream zipToOpen = new FileStream(zipFilePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string entryFileName = entry.Name;
                        string entryFullPath = Path.Combine(dataPath, entryFileName);

                        // ��ѹ�ļ�
                        entry.ExtractToFile(entryFullPath, true);
                    }
                }
            }
            // ɾ��ѹ������û�е�.xml�ļ�
            xmlFiles = Directory.GetFiles(dataPath, "*.xml");
            foreach (string xmlFilePath in xmlFiles)
            {
                string fileName = Path.GetFileName(xmlFilePath);
                if (!File.Exists(Path.Combine(dataPath, fileName)))
                {
                    File.Delete(xmlFilePath);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static async Task SendGameDataAsync (string uid, string filePath)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(Server.IP, Server.GameDataPort);
                //await client.ConnectAsync("127.0.0.1", Server.GameDataPort);
                using (NetworkStream stream = client.GetStream())
                {
                    // �����������ͣ�����Ϊ5��
                    byte[] requestTypeBytes = System.Text.Encoding.UTF8.GetBytes("UPLOAD");
                    Array.Resize(ref requestTypeBytes, 6); // ����Ϊָ���ĳ���
                    await stream.WriteAsync(requestTypeBytes, 0, requestTypeBytes.Length);

                    // ����UID
                    byte[] uidBytes = System.Text.Encoding.UTF8.GetBytes(uid);
                    Array.Resize(ref uidBytes, 10);
                    await stream.WriteAsync(uidBytes, 0, uidBytes.Length);

                    // �����ļ�����
                    using (FileStream fileStream = File.OpenRead(filePath))
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                    Debug.Log("�ļ��ϴ���ɣ�");
                }
            }
        }
        catch (Exception e)
        {
            // �����쳣
            Debug.Log("�ϴ������г����쳣��" + e.Message);
        }
    }

    public static async Task DownloadGameDataAsync (string uid, string savePath)
    {
        try
        {
            using (TcpClient client = new TcpClient())
            {
                await client.ConnectAsync(Server.IP, Server.GameDataPort);
                //await client.ConnectAsync("127.0.0.1", Server.GameDataPort);
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] requestTypeBytes = System.Text.Encoding.UTF8.GetBytes("GETDAT");
                    Array.Resize(ref requestTypeBytes, 6); // ����Ϊָ���ĳ���
                    await stream.WriteAsync(requestTypeBytes, 0, requestTypeBytes.Length);

                    // ����UID
                    byte[] uidBytes = System.Text.Encoding.UTF8.GetBytes(uid);
                    Array.Resize(ref uidBytes, 10);
                    await stream.WriteAsync(uidBytes, 0, uidBytes.Length);

                    // ����������Ӧ���浽�����ļ�
                    using (FileStream fileStream = File.Create(savePath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }

                    Console.WriteLine($"�ѳɹ��������ݲ����浽��{savePath}");
                }
            }
        }
        catch (Exception e)
        {
            // �����쳣
            Console.WriteLine("�������ݹ����г����쳣��" + e.Message);
        }
    }
}