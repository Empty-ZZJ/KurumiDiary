using UnityEngine;

public class Draggable3D : MonoBehaviour
{
    public float ��������Ļ��Χ;

    private Vector3 targetPosition; // �����Ŀ��λ��

    private void Update ()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // ����ָ��ʼ�Ӵ���Ļʱ
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject) // �����ָ�������˸�����
                    {
                        // �������Ŀ��λ������Ϊ��ǰλ��
                        targetPosition = transform.position;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved) // ����ָ����Ļ���ƶ�ʱ
            {
                if (IsTouchingThisObject(touch.fingerId)) // �����ָ����������彻��
                {
                    // ����Ļ����ת��Ϊ��������
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    Plane plane = new Plane(Vector3.forward, transform.position);
                    float distance;
                    if (plane.Raycast(ray, out distance))
                    {
                        // �������Ŀ��λ������Ϊ��ָ��λ��
                        targetPosition = ray.GetPoint(distance);

                        // ���������ܹ��ƶ������ƫ����
                        Bounds bounds = GetComponent<Collider>().bounds;
                        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).x - bounds.extents.x + ��������Ļ��Χ; // ��������Ļ��Χ
                        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).x + bounds.extents.x - ��������Ļ��Χ; // ��������Ļ��Χ
                        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).y - bounds.extents.y + ��������Ļ��Χ; // ��������Ļ��Χ
                        float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(transform.position.z - Camera.main.transform.position.z))).y + bounds.extents.y - ��������Ļ��Χ; // ��������Ļ��Χ

                        // ��������λ�������������������
                        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
                        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

                        // ʹ�� Lerp ����ƽ���ƶ�����
                        transform.position = targetPosition;
                    }
                }
            }
        }
    }

    private bool IsTouchingThisObject (int fingerId)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.fingerId == fingerId && Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out RaycastHit hit) && hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }
}