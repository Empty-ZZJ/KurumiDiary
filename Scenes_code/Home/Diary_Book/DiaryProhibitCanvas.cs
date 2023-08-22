using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryProhibitCanvas : MonoBehaviour
{
    private static int Number = 0;
    public Text _Title;
    public Image _Image;
    public List<Sprite> _AngryPictures;

    private void Start ()
    {
        Number++;
        _Title.text = Get_���ÿ��ռ�_Dialog();
        _Image.sprite = _AngryPictures[NewRandom.GetRandomInAB(1, 4)];
        StartCoroutine(DestroyAfterDelay(3f));
        if (Number >= 5)
        {
            //ִ���ջرʼǵķ���
            Number = 0;
            GameObject.Find("kitchen/Furniture_Kitchencabinet_Forest/PhotoAlbum_new").GetComponent<Diary_button>().button_out();
            Destroy(GameObject.FindWithTag("DiaryCanvas").gameObject);
        }
    }

    private IEnumerator DestroyAfterDelay (float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// ���ز��ÿ��ռǵ�ʱ�����˵��һ�仰��
    /// </summary>
    public string Get_���ÿ��ռ�_Dialog ()
    {
        int temp = NewRandom.GetRandomInAB(1, 6);
        switch (temp)
        {
            case 1:
                return "����͵���ռǣ����ҷ��ֵĻ�...�ߺ�";

            case 2:
                return "����Ǻ��ҵ���˽�����ܿ�";

            case 3:
                return "������������ô��Ů���ӵ���˽��ô����Ȥ����";

            case 4:
                return "����͵��Ů���ӵ��ռ�";

            case 5:
                return "���Ҳ�û��д�ռǣ���ʲôҲû����";

            case 6:
                return "����͵���ռǣ���������Ҳ�����ԡ�";
        }
        return "���Ҳ�û��д�ռǣ���ʲôҲû����";
    }
}