using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OPController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Image imageDisplay;
    public GameObject mainCanvas;
    public GameObject logo;
    public List<VideoClip> videoList;
    public List<Sprite> imageList;

    private int currentVideoIndex = 0;
    private int currentImageIndex = 0;

    void Start ()
    {
        PlayNextMedia();
    }

    void PlayNextMedia ()
    {
        if (currentVideoIndex < videoList.Count)
        {

            PlayVideo();
        }
        else if (currentImageIndex < imageList.Count)
        {
            logo.SetActive(true);
            videoPlayer.gameObject.SetActive(false);
            PlayImage();
        }
        else
        {
            // ������Ƶ��ͼƬ�Ѿ��������
            GameObject.Find("LogoCanvas").GetComponent<Logo>().Fide();
        }
    }

    void PlayVideo ()
    {
        VideoClip video = videoList[currentVideoIndex];
        videoPlayer.clip = video; // �������Ѿ���һ����ΪvideoPlayer��VideoPlayer���
        videoPlayer.Play(); // ������Ƶ
        currentVideoIndex++;
        // ������Ƶ�ĳ����������ӳٵ��õ�ʱ�䣬ȷ������Ƶ������ɺ������һ��ý��
        Invoke(nameof(PlayNextMedia), (float)video.length);
    }


    void PlayImage ()
    {
        Sprite image = imageList[currentImageIndex];
        // ��imageDisplay����ʾͼƬ
        imageDisplay.sprite = image;
        Debug.Log("������ʾͼƬ");

        currentImageIndex++;
        Invoke(nameof(PlayNextMedia), 3f); // ����ÿ��ͼƬ��ʾ3����
    }
}
