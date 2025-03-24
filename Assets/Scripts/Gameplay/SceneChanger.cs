using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName;
    public float delayTime;

    public void MoveToNextScene()
    {
        StartCoroutine(DelaySceneChange(delayTime));

    }

    public IEnumerator DelaySceneChange(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(nextSceneName);

    }

}
