using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FanAnimate : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[13];
    public int index = 1;
    public static bool fan_state;
    public static int mode;
    public AudioClip audio__fan;

    public void Update ()
    {
        if (mode == 1 || mode == 2)
        {
            fan_state = false;
            if (mode == 1)
            {
                index = 1;
                GetComponent<AudioSource>().clip = audio__fan;
                GetComponent<AudioSource>().Play();
                StartCoroutine(Start_fan_mode1(ok));
            }
            else
            {
                index = 12;
                GetComponent<AudioSource>().clip = audio__fan;
                GetComponent<AudioSource>().Play();

                StartCoroutine(Start_fan_mode2(ok));
            }
            mode = 0;
        }
    }

    public IEnumerator Start_fan_mode1 (Action callback)
    {
        // ������С�ڵ���12ʱ��ִ�����²���
        while (index <= 12)
        {
            // ��image����ľ�������Ϊsprites�����ж�Ӧ������ͼ��
            this.GetComponent<Image>().sprite = sprites[index];
            index++;
            // ��ÿ������֮��ȴ�0.05��
            yield return new WaitForSeconds(0.05f);
        }
        // ��fan_state��������Ϊtrue
        fan_state = true;
        // ����ص�������Ϊ�գ���ִ�лص�����
        if (callback != null)
            callback();
    }

    public IEnumerator Start_fan_mode2 (Action callback)
    {
        while (index >= 1)
        {
            this.GetComponent<Image>().sprite = sprites[index];
            index--;
            yield return new WaitForSeconds(0.05f);
        }

        fan_state = true;

        if (callback != null)
            callback();
    }

    private void ok ()
    {
        Debug.Log("��������");
        Destroy(this.gameObject);
    }
}