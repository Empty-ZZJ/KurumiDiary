using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class niudan : MonoBehaviour
{
    public GameObject niudan_obj;
    public GameObject niudan_ball;
    public GameObject maincamera;

    //------------------------------------------------------------------------------------------
    private Vector3 shakeRate = new Vector3(0.005f, 0.005f, 0.005f);//��������

    private float shakeTime = 2f;//������ʱ��
    private float shakeDertaTime = 0.08f;//�������

    //------------------------------------------------------------------------------------------
    private AsyncOperation operation;

    private Tweener ThreadScense;

    public int i;

    public void when_niudan_start ()
    {
        StartCoroutine(Shake_Coroutine());
        niudan_ball.transform.DOLocalRotate(new Vector3(0, -720, 0), 2, RotateMode.FastBeyond360);
        ThreadScense = maincamera.transform.DOMove(new Vector3(2.77900004f, 0.577000022f, 3.17700005f), 2);
        ThreadScense.OnComplete(loadscenses);
    }

    public IEnumerator Shake_Coroutine ()
    {
        var oriPosition = niudan_obj.gameObject.transform.position;
        for (float i = 0; i < shakeTime; i += shakeDertaTime)
        {
            niudan_obj.gameObject.transform.position = oriPosition +
                Random.Range(-shakeRate.x, shakeRate.x) * Vector3.right +
                Random.Range(-shakeRate.y, shakeRate.y) * Vector3.up +
                Random.Range(-shakeRate.z, shakeRate.z) * Vector3.forward;
            yield return new WaitForSeconds(shakeDertaTime);
        }
        niudan_obj.gameObject.transform.position = oriPosition;
    }

    private void loadscenses ()
    {
        Debug.Log("�鿨");
        StartCoroutine(start_game());
    }

    private IEnumerator start_game ()
    {//�첽������Ϸ����
        operation = SceneManager.LoadSceneAsync(4);
        yield return operation;
    }
}