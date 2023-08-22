using UnityEngine;

public class PopNewMessage
{
    /// <summary>
    /// ������ã�����Ļ�Ϸ�����һ����Ϣ
    /// </summary>
    /// <param name="_message"></param>
    public PopNewMessage (string _message)
    {
        Debug.Log(_message);
        CoreMessage._message = _message;
        GameObject _new = Resources.Load<GameObject>("Prefabs/UI/NewMessage");
        Object.Instantiate(_new);
    }
}