using UnityEngine;

/// <summary>
/// �������ŵ������κ�һ������������ϾͿ���
/// </summary>
public class ResetTouch : MonoBehaviour
{
    public void Awake ()
    {
        SceneCameraMove.SetMoveTrue();
    }
}