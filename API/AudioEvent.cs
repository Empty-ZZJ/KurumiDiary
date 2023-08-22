using System;
using UnityEngine;

/// <summary>
/// AudioSource�Ĳ���״̬����������һ��AudioSource������Ա.
/// ʹ�÷�����
/// AudioEvent ae =AudioEvent.AddComponentToGameObject(t1.gameObject);
/// ae.audioSource.clip = clip1;//�Լ����Ƹ�ֵ
/// ae.EventPlayStart += OnEventPlayStart;
/// ae.EventPlayEnd += OnEventPlayEnd;
/// ae.audioSource.Play();
/// </summary>
internal class AudioEvent : MonoBehaviour
{
    /// <summary>
    /// ����ű����ڵ������ϵ�audioSource
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// ���ſ�ʼ�¼�
    /// </summary>
    public event Action<AudioEvent> EventPlayStart;

    /// <summary>
    /// ���Ž����¼�
    /// </summary>
    public event Action<AudioEvent> EventPlayEnd;

    /// <summary>
    /// ��ز���״̬
    /// </summary>
    private bool _lastPlayStatus;

    /// <summary>
    /// ��һ���������������¼�������
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static AudioEvent AddComponentToGameObject (GameObject obj)
    {
        AudioEvent com = obj.GetComponent<AudioEvent>();
        if (com == null)
        {
            com = obj.AddComponent<AudioEvent>();
        }
        return com;
    }

    private void Awake ()
    {
        //���û�����AudioSource�����Ǿ�Ҫ���һ��
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.playOnAwake = false;
        }
        _lastPlayStatus = false;
    }

    /// <summary>
    /// ���һ�µ�ǰ����״̬
    /// </summary>
    private void UpdatePlaySstatus ()
    {
        if (_lastPlayStatus == false && audioSource.isPlaying == true)
        {
            if (EventPlayStart != null)
            {
                EventPlayStart(this);//�����¼�����ʼ����
            }
        }
        if (_lastPlayStatus == true && audioSource.isPlaying == false)
        {
            if (EventPlayEnd != null)
            {
                EventPlayEnd(this);//�����¼�������ֹͣ
            }
        }
        _lastPlayStatus = audioSource.isPlaying;
    }

    public void Update ()
    {
        UpdatePlaySstatus();
    }

    private void OnDestoryed ()
    {
        //������������Ƿ���Ҫ��������ֹͣ�¼���
        if (_lastPlayStatus == true)
        {
            if (EventPlayEnd != null)
            {
                EventPlayEnd(this);//�����¼�������ֹͣ
            }
        }
    }
}