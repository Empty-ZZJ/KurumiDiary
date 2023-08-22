using System;
using UnityEngine;
using UnityEngine.UI;

public class LrcEvent : MonoBehaviour
{
    public Text lyricText; // ��ʾ��ʵ�Text
    public AudioSource audioSource; // ��Ƶ���������

    private static LyricData lyricData; // ������ĸ������
    private static string[] lyricLines; // �ָ��ĸ��������
    private static int currentLyricIndex = 0; // ��ǰ����е�����
    public static bool IsLRc;

    public static void SetLrc (string _json)
    {
        currentLyricIndex = 0;
        IsLRc = true;
        // �������JSON����
        lyricData = JsonUtility.FromJson<LyricData>(_json);
        // �滻���е� \r
        string cleanedLyricText = lyricData.lyric.Replace("\r", "");
        // ������ı��ָ����
        lyricLines = cleanedLyricText.Split('\n');
        Debug.Log(_json);
    }


    public void Update ()
    {
        if (IsLRc == false)
        {
            lyricText.text = string.Empty;
            return;
        }

        try
        {

            float currentTime = audioSource.time;

            // ���м���ʣ��ж��Ƿ���Ҫ��ʾ�µĸ����
            while (currentLyricIndex < lyricLines.Length)
            {

                string lyricLine = lyricLines[currentLyricIndex];
                if (string.IsNullOrEmpty(lyricLine))
                {

                    break;
                }
                else if (!string.IsNullOrEmpty(lyricLine) && lyricLine[lyricLine.Length - 1] == ']')
                {
                    //����һ���Ǹ�����Ϣ������ʱ��+����ı��ĸ�ʽ
                    DisplayLyric(lyricLine);
                    currentLyricIndex++;
                }
                else if (IsLyricTime(lyricLine, currentTime))
                {
                    DisplayLyric(lyricLine);
                    currentLyricIndex++;
                }
                else
                {
                    break;
                }
            }


        }
        catch (Exception)
        {
            //new PopNewMessage(ex.Message);
            lyricText.text = "�޷�ʶ��ĸ�ʸ�ʽ";
        }

    }

    // �жϵ�ǰ������Ƿ���Ҫ��ʾ
    private bool IsLyricTime (string lyricLine, float currentTime)
    {
        string[] parts = lyricLine.Split('[', ']');
        if (parts.Length < 3)
        {
            return false;
        }

        string[] timeParts = parts[1].Split(':');
        if (timeParts.Length < 2)
        {
            return false;
        }

        float lyricTime = float.Parse(timeParts[0]) * 60 + float.Parse(timeParts[1]);
        return currentTime >= lyricTime;
    }

    // ��ʾ����е�UIԪ��
    private void DisplayLyric (string lyricLine)
    {
        string[] parts = lyricLine.Split('[', ']');
        if (parts.Length < 3)
        {
            return;
        }

        lyricText.text = parts[2];
    }
}
// ���ڽ������JSON���ݵ���
[System.Serializable]
public class LyricData
{
    public string lyric;
}