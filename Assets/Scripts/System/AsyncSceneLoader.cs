using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoader : MonoBehaviour
{
    public float minLoadingTime = 10f;

    bool hasFadeOut = false;    // 命名需修改

    public Animator ani;

    private void OnEnable()
    {
        ani = GetComponent<Animator>();
        ani.SetTrigger("FadeIn");
    }

    static AsyncSceneLoader singleton = null;
    public static AsyncSceneLoader Singleton
    {
        get
        {
            singleton = FindObjectOfType(typeof(AsyncSceneLoader)) as AsyncSceneLoader;

            if (singleton == null)
            {
                GameObject g = new GameObject("AsyncSceneLoader");
                singleton = g.AddComponent<AsyncSceneLoader>();
            }

            return singleton;
        }

    }

    // 是否完成淡出
    public void HasFadeOut()
    {
        hasFadeOut = true;
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadAsync(name));
    }

    public void LoadScene(int index)
    {

    }

    IEnumerator LoadAsync(string name)
    {
        ani.SetTrigger("FadeOut");

        while (!hasFadeOut)
        {
            yield return null;
        }

        yield return null;  // 避免過快載入
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;

        float t = 0;

        while (async.progress < .9f || t < minLoadingTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}
