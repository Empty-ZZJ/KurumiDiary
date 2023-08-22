using config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Diary_Detail : MonoBehaviour
{
    public const int ��ǰ�汾��Ϸһ���ж����ռ� = 50;
    private readonly ConfigXML configXML = new ConfigXML();
    private AudioSource _Audiosystem;
    public int ��ǰ�����ռǵ�����;
    public int �û��������ռ�ƪ��;
    public TextMeshProUGUI left;
    public TextMeshProUGUI right;
    public TextMeshProUGUI time;
    private int Mode;
    private readonly bool book_state;
    public Sprite[] ���ҵ�ͼƬ = new Sprite[4];
    public GameObject prop_background;
    public Image Image_���ҵ�ͼƬ;
    public Animator animator_system;
    public GameObject Diary;

    public void Awake ()
    {
        _Audiosystem = GameObject.Find("audiosystem/scence_audio").GetComponent<AudioSource>();
        Diary = GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/PhotoAlbum_new");
        if (File.Exists(Application.persistentDataPath + "/diary.xml") == true)
        {
            if (configXML.��������("�û��������ռ�ƪ��", "diary.xml") == "None")
            {
                configXML.����������("�û��������ռ�ƪ��", "1", "diary.xml");
                �û��������ռ�ƪ�� = 1;
            }
            else
            {
                int.TryParse(configXML.��������("�û��������ռ�ƪ��", "diary.xml"), out �û��������ռ�ƪ��);
                ��ǰ�����ռǵ����� = �û��������ռ�ƪ��;
            }
        }
        else { configXML.���������ļ�("�û��������ռ�ƪ��", 1.ToString(), "diary.xml"); �û��������ռ�ƪ�� = 1; ��ǰ�����ռǵ����� = 1; }
        DiaryUpdate(1);
        StartCoroutine(Loading_Diary(��ǰ�����ռǵ�����));
    }

    public static void AddDiaty ()
    {
        ConfigXML _ = new ConfigXML();
        int temp = Convert.ToInt32(_.��������("�û��������ռ�ƪ��", "diary.xml"));
        string time = System.DateTime.Now.Year.ToString() + "." + System.DateTime.Now.Month.ToString() + "." + System.DateTime.Now.Day.ToString();
        if (temp + 1 <= 50)
        {
            _.����������("�û��������ռ�ƪ��", (temp + 1).ToString(), "diary.xml");
            _.����������("diary" + (temp + 1).ToString(), time, "diary.xml");
        }
    }

    public void ButtonLeft ()
    {
        Debug.Log(��ǰ�����ռǵ����� - 1);
        if (��ǰ�����ռǵ����� - 1 >= 1)
        {
            DiaryUpdate(2);
            Mode = 2;
            StartCoroutine(ChangeDiary());
        }
    }

    public void ButtonRight ()
    {
        Debug.Log(��ǰ�����ռǵ����� + 1);
        if (��ǰ�����ռǵ����� + 1 <= �û��������ռ�ƪ��)
        {
            DiaryUpdate(1);
            Mode = 1;
            StartCoroutine(ChangeDiary());
        }
    }

    public IEnumerator ChangeDiary ()
    {
        if (Mode == 1)
        {
            yield return StartCoroutine(Loading_Diary(++��ǰ�����ռǵ�����));
        }
        else
        {
            yield return StartCoroutine(Loading_Diary(--��ǰ�����ռǵ�����));
        }
    }

    public void DiaryUpdate (int mode)
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/UI_Fan"), this.gameObject.transform);
        FanAnimate.mode = mode;
    }

    private string ReadDiatyTime (int diary_index)
    {
        if (File.Exists(Application.persistentDataPath + "/diary.xml") == true)
        {
            if (configXML.��������("diary" + diary_index.ToString(), "diary.xml") == "None")
            {
                string time = System.DateTime.Now.Year.ToString() + "." + System.DateTime.Now.Month.ToString() + "." + System.DateTime.Now.Day.ToString();
                configXML.����������("diary" + diary_index.ToString(), time, "diary.xml");
            }
        }
        return configXML.��������("diary" + diary_index.ToString(), "diary.xml");
    }

    public IEnumerator Loading_Diary (int temp_index)
    {

        yield return null;
        try
        {
            TextAsset textAsset = Resources.Load<TextAsset>("TextAsset/Diary/diary" + temp_index.ToString());
            string originalText = textAsset.text;

            // �����е� "*" �滻���ض�����
            string replacedText = originalText.Replace("*", GameConfig.GetValue("name", "playerinformation.xml").TrimStart());

            // ���ı����зָ������
            string[] lines = replacedText.Split('\n');

            // ����ָ�λ�ã�������һ�룩
            int splitIndex = lines.Length / 2;

            // ����ǰ�벿�ֺͺ�벿�ֵ��ı�
            string[] firstHalfLines = new string[splitIndex];
            string[] secondHalfLines = new string[lines.Length - splitIndex];

            // ��������ָ�Ϊǰ�벿�ֺͺ�벿��
            for (int i = 0; i < splitIndex; i++)
            {
                firstHalfLines[i] = lines[i];
            }

            for (int i = splitIndex; i < lines.Length; i++)
            {
                secondHalfLines[i - splitIndex] = lines[i];
            }

            // ������ת�����ַ���
            string firstHalfText = string.Join("\n", firstHalfLines);
            string secondHalfText = string.Join("\n", secondHalfLines);

            // ���� UI �ı�
            left.text = firstHalfText;
            right.text = secondHalfText;
            time.text = ReadDiatyTime(temp_index);
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            HL.IO.HL_Log.Log(nameof(this.name) + ex.Message, "Error");
        }

    }

    public List<string> SplitByLine (string text)
    {
        List<string> lines = new List<string>();
        byte[] array = Encoding.UTF8.GetBytes(text);
        using (MemoryStream stream = new MemoryStream(array))
        {
            using (var sr = new StreamReader(stream))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    lines.Add(line);
                    line = sr.ReadLine();
                };
            }
        }
        return lines;
    }

    public int Getlist_lenght (string[] temp_list)
    {
        for (int i = 0; i <= temp_list.Length; i++)
        {
            if (temp_list[i] == "-")
            {
                return i - 1;
            }
        }
        return temp_list.Length;
    }

    public string Getlist_content (string[] list, int index_start, int index_finally)
    {
        string fi = "";
        for (int i = index_start; i <= index_finally; i++)
        {
            if (list[i].Contains("*") == true)
            {
                fi += list[i].Replace("*", plotbutton.ReadXML("name", "name", "playerinformation.xml").TrimStart()) + "\n";
            }
            else fi += list[i] + "\n";
        }
        return fi;
    }
}