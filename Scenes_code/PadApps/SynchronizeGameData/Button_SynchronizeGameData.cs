using System;
using System.IO;
using System.Threading;
using UnityEngine;

public class Button_SynchronizeGameData : MonoBehaviour
{
    private string DataPath;

    private void Start ()
    {
        DataPath = $"{Application.persistentDataPath}/GameTempData.hoilai";
    }

    public async void Button_Click_SynchronizeGameData ()
    {
        try
        {
            var cts = new CancellationTokenSource();
            if (API.EncodingAction.GetNum(GameConfig.GetValue("UID", "playerinformation.xml")) > 10000)
            {
                var _loading = new ShowLoading("���ڳ�ʼ��");
                if (File.Exists(DataPath))
                    File.Delete(DataPath);
                if (SynchronizeGameData.CompressGameData())
                {
                    _loading.SetTitle("����ͬ��");
                    await SynchronizeGameData.DownloadGameDataAsync(GameConfig.GetValue("UID", "playerinformation.xml"), DataPath);
                    if (SynchronizeGameData.UpdateGameData(DataPath))
                    {
                        new PopNewMessage("ͬ���ɹ�");
                    }
                    else
                    {
                        SynchronizeGameData.CompressGameData();
                        await SynchronizeGameData.SendGameDataAsync(GameConfig.GetValue("UID", "playerinformation.xml"), DataPath);
                    }
                    _loading.KillLoading();
                }
                else
                {
                    _loading.KillLoading();
                    new PopNewMessage("�޷�ȷ���Ĵ浵�ļ�");
                }
                /*
                SynchronizeGameData.UpdateGameData($"{Application.persistentDataPath}/GameTempData.hoilai");
                */
            }
            else
            {
                new PopNewMessage("����δ��¼�����˺������쳣���볢�����µ�½");
            }
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
        }
    }
}