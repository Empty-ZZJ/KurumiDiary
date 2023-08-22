using System;
using TMPro;
using UnityEngine;

/// <summary>
/// ��ʾԲȦ����ҳ�棬������ʵ��������һ�������Ч
/// </summary>
public class ShowLoading
{
    private GameObject _GameObjectLoading;

    /// <summary>
    /// ʵ��������һ�������Ч
    /// </summary>
    /// <param name="_Title">��ʾ�ı�</param>
    public ShowLoading (string _Title = "")
    {
        Debug.Log("��ʼ����");
        _GameObjectLoading = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UI/WaitLoading"));

        if (_Title != string.Empty)
        {
            foreach (var t in _GameObjectLoading.GetComponentsInChildren<Transform>())
            {
                if (t.name == "title")
                {
                    var _textMeshProUGUI = t.GetComponent<TextMeshProUGUI>();
                    _textMeshProUGUI.text = _Title;
                }
            }
        }
    }

    public void SetTitle (string _Title)
    {
        foreach (var t in _GameObjectLoading.GetComponentsInChildren<Transform>())
        {
            if (t.name == "title")
            {
                var _textMeshProUGUI = t.GetComponent<TextMeshProUGUI>();
                _textMeshProUGUI.text = _Title;
            }
        }
    }

    /// <summary>
    /// �رյ�ǰ����
    /// </summary>
    /// <returns></returns>
    public bool KillLoading ()
    {
        try
        {
            Debug.Log("���ټ���");
            MonoBehaviour.Destroy(_GameObjectLoading.gameObject);
            return true;
        }
        catch (Exception ex)
        {
            new PopNewMessage(ex.Message);
            return false;
        }
    }

    /// <summary>
    /// ������ʾ����ҳ��
    /// </summary>
    public bool ReShowLoading ()
    {
        if (_GameObjectLoading == null)
        {
            _GameObjectLoading = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/UI/WaitLoading"));
            return true;
        }
        else
        {
            return false;
        }
    }
}