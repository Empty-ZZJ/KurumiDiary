using UnityEngine;

public class test : MonoBehaviour
{
    private ShowLoading loading;
    public void Start ()
    {

    }
    public void ѹ���ļ� ()
    {
        SynchronizeGameData.CompressGameData();
    }

    public void �������� ()
    {
        SynchronizeGameData.UpdateGameData($"{Application.persistentDataPath}/GameTempData.hoilai");
    }

    public async void �ϴ��������� ()
    {
        await SynchronizeGameData.SendGameDataAsync("10001", $"{Application.persistentDataPath}/GameTempData.hoilai");
    }

    public async void ���ص����� ()
    {
        await SynchronizeGameData.DownloadGameDataAsync("10001", $"{Application.persistentDataPath}/GameTempData.hoilai");
    }

    public void ��ʾ���ؽ���2�� ()
    {
        loading = new ShowLoading("������");
        Invoke("_kill", 2);
    }

    private void _kill ()
    {
        loading.KillLoading();
    }

    public void ��ȡ����Ȧ���� ()
    {
    }
}