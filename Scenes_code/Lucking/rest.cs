using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rest : MonoBehaviour
{
    private AsyncOperation operation;

    public void loadscenses ()
    {
        Debug.Log("�鿨");
        StartCoroutine(start_game());
    }

    private IEnumerator start_game ()
    {
        operation = SceneManager.LoadSceneAsync(4);
        yield return operation;
    }
}