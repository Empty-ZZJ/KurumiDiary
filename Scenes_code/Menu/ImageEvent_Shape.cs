using UnityEngine;

public class ImageEvent_Shape : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update ()
    {
        // ÿ֡��תһ���Ƕ�
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}