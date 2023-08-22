using UnityEngine;
using UnityEngine.UI;

public class GetFPS : MonoBehaviour
{
    private float _updateInterval = 1f;//�趨����֡�ʵ�ʱ����Ϊ1��
    private float _accum = .0f;//�ۻ�ʱ��
    private int _frames = 0;//��_updateIntervalʱ���������˶���֡
    private float _timeLeft;
    private string fpsFormat;

    private void Start ()
    {
        _timeLeft = _updateInterval;
    }

    private void Update ()
    {
        _timeLeft -= Time.deltaTime;
        //Time.timeScale���Կ���Update ��LateUpdate ��ִ���ٶ�,
        //Time.deltaTime��������㣬������һ֡��ʱ��
        //������ɵõ���Ӧ��һ֡���õ�ʱ��
        _accum += Time.timeScale / Time.deltaTime;
        ++_frames;//֡��

        if (_timeLeft <= 0)
        {
            float fps = _accum / _frames;
            //Debug.Log(_accum + "__" + _frames);
            fpsFormat = System.String.Format("{0:F2}", fps);//������λС��
            _timeLeft = _updateInterval;
            _accum = .0f;
            _frames = 0;
        }

        gameObject.GetComponent<Text>().text = "FPS:" + fpsFormat;
    }
}