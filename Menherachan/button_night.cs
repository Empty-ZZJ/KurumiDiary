using TMPro;
using UnityEngine;

public class button_night : MonoBehaviour
{
    public GameObject dialog;
    public GameObject txt;
    public Animator night_move;
    private float duration = 3.45f;

    public void button_menhera ()
    {
        if (dialog.activeSelf == true) return;
        night_move.Play("night_button");
        dialog.SetActive(true);
        txt.SetActive(true);
        int a = NewRandom.GetRandomInAB(1, 4);
        switch (a)
        {
            case 1:
                txt.GetComponent<TextMeshProUGUI>().text = "ʱ�����ˣ���Ҳ�����Ϣ��";
                break;

            case 2:
                txt.GetComponent<TextMeshProUGUI>().text = "������......";
                break;

            case 3:
                txt.GetComponent<TextMeshProUGUI>().text = "����..����Ծ���...";
                break;

            case 4:
                txt.GetComponent<TextMeshProUGUI>().text = "������....";
                break;

            case 5:
                txt.GetComponent<TextMeshProUGUI>().text = "��û˯��Ҫ�����Ϣѽ";
                break;

            case 6:
                txt.GetComponent<TextMeshProUGUI>().text = "��..��ô�ˣ�";
                break;
        }
        Invoke("MethodName", duration);
    }

    private void MethodName ()
    {
        dialog.SetActive(false);
    }
}