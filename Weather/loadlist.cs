using UnityEngine;
using UnityEngine.UI;

public class loadlist : MonoBehaviour
{
    public Dropdown province;
    public Text text_province;
    private string[] list_province = new string[] { "����", "���", "�Ϻ�", "����", "�ӱ�", "ɽ��", "����", "������", "����", "�㽭", "����", "����", "����", "ɽ��", "����", "����", "����", "�㶫", "����", "�Ĵ�", "����", "����", "����", "����", "�ຣ", "���ɹ�", "����", "����", "����", "�½�", "���", "����" };

    private void Start ()
    {
        for (int i = 0; i < list_province.Length; i++) loadlist_province(list_province[i]);
        text_province.text = list_province[province.value];
    }

    public void loadlist_province (string add_province)
    {
        Dropdown.OptionData OptionData = new Dropdown.OptionData();
        OptionData.text = add_province;
        province.options.Add(OptionData);
    }

    public void load_city ()
    {
        Debug.Log(text_province.text);
    }
}