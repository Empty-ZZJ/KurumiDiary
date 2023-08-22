using UnityEngine;

public class IKControl : MonoBehaviour
{
    public Animator anim;      //����
    public Transform head;     //ͷ��
    public Transform lefthand;  //����
    public Transform righthand; //����
    public Transform leftfoot;  //���
    public Transform rightfoot; //�ҽ�



    private void OnAnimatorIK (int layerIndex)
    {
        anim.SetLookAtWeight(1);
        anim.SetLookAtPosition(head.position); //ͷ������
        /*
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, lefthand.position); //����λ��
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKPosition(AvatarIKGoal.RightHand, righthand.position); //����λ��
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftfoot.position); //���λ��
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightfoot.position); //�ҽ�λ��
        */

    }
}
