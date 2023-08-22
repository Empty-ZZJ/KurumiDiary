using UnityEngine;

public class MiniGameRunStruct_Jump : MonoBehaviour
{
    /// <summary>
    /// ��Ϸ�Ľ���״̬
    /// </summary>
    public static bool GameState = false;

    /// <summary>
    /// ��Ϸ�ĳ�ʼ�ٶ�
    /// </summary>
    public const float GameInit_Speed = 400;

    /// <summary>
    /// ��ǰ����Ϸ�ٶ�
    /// </summary>
    public static float GameSpeed = 400;

    /// <summary>
    /// ��ʱ�����ϴμӿ��ٶȵ����ڵ�ʱ��
    /// </summary>
    public static float timer = 0.0f;

    /// <summary>
    /// ʱ����
    /// </summary>
    public static float incrementInterval = 10.0f;

    /// <summary>
    /// ���ӵ�ֵ
    /// </summary>
    public static int incrementValue = 50;

    /// <summary>
    /// ��Ϸ������ٶ�
    /// </summary>
    public static int maxGameSpeed = 900;

    private void Awake ()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/MiniGame/MiniGame_Jump_Menu"));
    }

    private void Update ()
    {
        if (MiniGameRunStruct_Jump.GameState)
        {
            timer += Time.deltaTime;
            if (timer > incrementInterval)
            {
                timer -= incrementInterval;
                if (GameSpeed + incrementValue <= maxGameSpeed)
                {
                    GameSpeed += incrementValue;
                    Debug.Log("����  ��ǰ�ٶȣ�" + GameSpeed.ToString());
                }
                else
                {
                    GameSpeed = maxGameSpeed;
                }
            }
        }
    }
}