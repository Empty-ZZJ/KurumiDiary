using API;
using config;
using System;
using System.IO;
using UnityEngine;

public class UpdateDiary : MonoBehaviour
{
    private ConfigXML configXML = new ConfigXML();

    private void Start ()
    {
        if (File.Exists(Application.persistentDataPath + "/diary.xml") == true)
        {
            if (configXML.��������("�ռ���һ�θ���ʱ��", "diary.xml") == "None")
            {
                configXML.����������("�ռ���һ�θ���ʱ��", TimestampConverter.DateTimeToTimestamp(DateTime.Now).ToString(), "diary.xml");
            }
            else check_diary();
        }
    }

    public void Set ()
    {
        if (true)
        {
            Debug.Log("I love you");
        }
    }

    public void check_diary ()
    {
        DateTime _now = DateTime.Now;
        if (_now > TimestampConverter.TimestampToDateTime(Convert.ToInt64(configXML.��������("�ռ���һ�θ���ʱ��", "diary.xml"))))
        {
            Debug.Log("�����ռ�");

            configXML.����������("�ռ���һ�θ���ʱ��", TimestampConverter.DateTimeToTimestamp(_now.AddDays(NewRandom.GetRandomInAB(2, 7))).ToString(), "diary.xml");
            Diary_Detail.AddDiaty();
        }
    }
}