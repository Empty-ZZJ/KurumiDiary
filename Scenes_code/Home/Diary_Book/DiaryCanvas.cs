using System;
using UnityEngine;

public class DiaryCanvas : MonoBehaviour
{
    public void Button_Click_CloseDiary ()
    {
        GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/PhotoAlbum_new").GetComponent<Diary_button>().button_out();
    }

    public void CreatDiary (Transform _transform)
    {
        Debug.Log(MenheraArrange.Find_bool_home_menherachan().ToString());
        int _hour = DateTime.Now.Hour;
        if (_hour >= 23 || (_hour >= 0 && _hour < 6))//�ǲ������ϣ����ϲ����ڲ��ڼҶ����Կ��ռ�
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/DiaryDetail"), _transform);
        else//��������
        {
            if (MenheraArrange.Find_bool_home_menherachan())//�ڼ�
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/DiaryProhibitCanvas"));
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Prefabs/UI/ScenesUI/Diary/DiaryDetail"), _transform);
            }
        }
    }
}