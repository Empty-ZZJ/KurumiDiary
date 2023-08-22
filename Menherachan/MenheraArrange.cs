using UnityEngine;

public class MenheraArrange : MonoBehaviour
{
    private readonly int today = System.DateTime.Now.Day;
    private int xml_day;
    private bool today_mode;

    private void Awake ()
    {
        if (GameConfig.GetValue("day", "arrange.xml") != "None")
        {
            int.TryParse(GameConfig.GetValue("day", "arrange.xml"), out xml_day);
            if (today == xml_day) today_mode = true; else today_mode = false;
        }
        else
        {
            GameConfig.SetValue("day", System.DateTime.Now.Day.ToString(), "arrange.xml");
            today_mode = false;
        }
        if (today_mode == false) Main_core_arrange_today_menherachan();
    }

    public void Main_core_arrange_today_menherachan ()
    {
        if (GetMode(1, 2) == 1)
        {
            UpdateMode("�Ƿ����", "��");
            UpdateMode("�����ʼʱ��", GetMode(13, 14).ToString());
            UpdateMode("�������ʱ��", GetMode(15, 16).ToString());
        }
        else UpdateMode("�Ƿ����", "��");

        if (GetMode(1, 3) == 1)
        {
        }
    }

    private int GetMode (int min, int max)
    {
        return NewRandom.GetRandomInAB(min, max);
    }

    private void UpdateMode (string son, string wantdate)
    {
        if (GameConfig.GetValue(son, "arrange.xml") != "None")
        {
            GameConfig.SetValue(son, wantdate, "arrange.xml");
        }
        else
        {
            GameConfig.SetValue(son, wantdate, "arrange.xml");
        }
    }

    public static string ReadMode (string want)
    {
        return GameConfig.GetValue(want, "arrange.xml");
    }

    /// <summary>
    /// ���ﲻ�������ж�����Ϊ���õ����������ʱ��϶��Ǵ���"�Ƿ����"����ڵ�ģ�����ֻ���Ƿ������𰸡�Main:
    /// ���ص�ǰ�����Ƿ��ڼҡ�
    /// </summary>
    /// <returns></returns>
    public static bool Find_bool_home_menherachan ()
    {
        int time = System.DateTime.Now.Hour;
        if (ReadMode("�Ƿ����") == "��")
        {
            int from, to;
            int.TryParse(ReadMode("�����ʼʱ��"), out from);
            int.TryParse(ReadMode("�������ʱ��"), out to);
            if (time >= from && time <= to) return false; else return true;
        }
        else
        {
            return true;
        }
    }
}