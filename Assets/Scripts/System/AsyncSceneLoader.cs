using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JacDev.Audio;

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
        StartCoroutine(LoadAsync(name, 0f));
    }
    public void LoadScene(string name, float delay = 0f)
    {
        StartCoroutine(LoadAsync(name, delay));
    }

    // public void LoadScene(int index)
    // {
    //     StartCoroutine(LoadAsync(SceneManager.GetSceneByBuildIndex(index).name));
    // }

    IEnumerator LoadAsync(string name, float delay)
    {
        ani.SetTrigger("FadeOut");

        AudioHandler.Singleton.PauseBgm();
        GameObject soundObject = AudioHandler.Singleton.PlaySound("loading", 0f);

        while (!hasFadeOut)
        {
            if (Time.timeScale == 0)
                yield return new WaitForSecondsRealtime(.01f);
            else
                yield return null;
        }

        // 避免過快載入
        if (Time.timeScale == 0)
            yield return new WaitForSecondsRealtime(.01f);
        else
            yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;

        float t = 0;

        while (async.progress < .9f || t < minLoadingTime)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        async.allowSceneActivation = true;
        Time.timeScale = 1;
        Destroy(soundObject);
    }
}
