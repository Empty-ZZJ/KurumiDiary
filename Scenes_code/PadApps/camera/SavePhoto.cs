using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SavePhoto : MonoBehaviour
{
    private const string SCREENSHOT_DIR = "Menhera";
    private const string SCREENSHOT_EXT = ".png";
    public GameObject CameraUI;
    public void Save ()
    {

        StartCoroutine(CaptureScreenshot());
    }
    private IEnumerator CaptureScreenshot ()
    {
        // �ȴ�һ֡����ȷ����Ļ�Ѿ���Ⱦ���
        yield return new WaitForEndOfFrame();

        try
        {
            // ���������浽����
            Texture2D screenshotTex = ScreenCapture.CaptureScreenshotAsTexture();
            byte[] bytes = screenshotTex.EncodeToPNG();

            // ���Ŀ¼�Ƿ���ڣ��粻�����򴴽�
            string screenshotPath = Path.Combine(GetScreenshotDir(), GetScreenshotName());
            string screenshotDir = Path.GetDirectoryName(screenshotPath);
            if (!Directory.Exists(screenshotDir))
            {
                Directory.CreateDirectory(screenshotDir);
            }

            File.WriteAllBytes(screenshotPath, bytes);
            string[] paths = new string[1];
            paths[0] = screenshotPath;
            ScanFile(paths);
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            HL.IO.HL_Log.Log($"{this.name}  {ex.Message}", "Error");
        }
        finally
        {

        }


    }


    private string GetScreenshotDir ()
    {
        string dcimPath = "/storage/emulated/0/DCIM/";

        // ��� DCIM �ļ����Ƿ���ڣ��粻�����򴴽�֮
        if (!Directory.Exists(dcimPath))
        {
            Directory.CreateDirectory(dcimPath);
        }

        // ƴ��Ŀ���ļ���·��
        string screenshotDir = System.IO.Path.Combine(dcimPath, SCREENSHOT_DIR);

        if (!Directory.Exists(screenshotDir))
        {
            Directory.CreateDirectory(screenshotDir);
        }

        return screenshotDir;
    }

    private string GetScreenshotName ()
    {
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        return "screenshot_" + timestamp + SCREENSHOT_EXT;
    }

    private void ScanFile (string[] path)
    {
        using (AndroidJavaClass PlayerActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject playerActivity = PlayerActivity.GetStatic<AndroidJavaObject>("currentActivity");
            using (AndroidJavaObject Conn = new AndroidJavaObject("android.media.MediaScannerConnection", playerActivity, null))
            {
                Conn.CallStatic("scanFile", playerActivity, path, null, null);
            }
        }
    }
}