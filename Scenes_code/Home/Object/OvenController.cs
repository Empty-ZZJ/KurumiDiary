using System;
using System.Linq;
using UnityEngine;

public class OvenController : MonoBehaviour
{
    public AudioClip open;
    public AudioClip close;
    private bool oven_on = false;
    public GameObject now_obj;
    public int index;
    public int OpenTimes;
    public GameObject Menhera;
    private readonly string[] scentes = { "����һֱ�����عأ�΢��¯�ỵ�İɣ�", "���ǲ�Ҫ�����˰ɣ�", "�����Ļ���΢��¯�ỵ�İɣ�", "��˵����ʮ�������о�ϲ��", "����10000�λ᲻��ը����" };
    private void Start ()
    {
        Debug.Log(this.gameObject.name);
    }

    public void OnMouse ()
    {
        if (oven_on == false)//open
        {
            oven_on = true;
            GetComponent<Animation>().Play("open");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = open;
            now_audio.Play();
            index++;
            if (index == 10000)
            {
                boom();
            }
        }
        else
        {
            oven_on = false;
            GetComponent<Animation>().Play("close");
            AudioSource now_audio = now_obj.GetComponent<AudioSource>();
            now_audio.clip = close;
            now_audio.Play();
            var nowhour = DateTime.Now.Hour;
            if (nowhour >= 23 || (nowhour >= 0 && nowhour < 6))
            {
                OpenTimes++;
                if (OpenTimes > 2)
                {
                    new PopNewMessage(scentes[NewRandom.GetRandomInAB(0, scentes.Count() - 1)]);
                }
                if (OpenTimes >= 10)
                {
                    OpenTimes = 0;
                }
            }
            else
            {
                OpenTimes++;
                if (OpenTimes > 2)
                {
                    new PopNewMessage(scentes[NewRandom.GetRandomInAB(0, scentes.Count() - 1)]);
                }
                if (OpenTimes >= 10 && MenheraArrange.Find_bool_home_menherachan())
                {
                    //ִ������
                    new PopNewMessage("�G����������ô�ˣ�");
                    Menhera.SetActive(true);
                    MenheraController.SetNewState(new StateMachine_Dance());

                    OpenTimes = 0;
                }
            }

        }
    }

    private void boom ()
    {
        throw new NotImplementedException();
    }
}