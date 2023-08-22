using System.Collections;
using UnityEngine;

public class PlotAudioManager : MonoBehaviour
{
    public AudioSource Audio_Back;

    private void Start ()
    {
        StartCoroutine(AudioCallBack());
    }

    private IEnumerator AudioCallBack ()
    {
        Debug.Log(Audio_Back.isPlaying);
        while (Audio_Back.isPlaying)
        {
            yield return new WaitForSecondsRealtime(0.1f);//�ӳ����һ��ִ��
        }
        int index = NewRandom.GetRandomInAB(1, 4);
        Audio_Back.clip = Resources.Load<AudioClip>($"Audio/����ƪ��_{index.ToString()}");
        Audio_Back.Play();
        StartCoroutine(AudioCallBack());
    }
}