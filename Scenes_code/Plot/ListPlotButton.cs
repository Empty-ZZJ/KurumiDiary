using UnityEngine;

public class ListPlotButton : MonoBehaviour
{
    public static int Plotindex;

    public void Button_On_core ()
    {

        int.TryParse(this.name.Split('t')[1], out Plotindex);
        if (Plotindex > 2)
        {
            new PopNewPopup("����", "δ���ڷ������ϼ������þ�����Ϣ������������ڵ�ǰ�����ı���δ���������µġ��ɵȴ��������¡�");
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/Plot/DetailPlotCanvas"));
        }
        Debug.Log(Plotindex);

    }
}