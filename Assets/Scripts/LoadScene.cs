using JetBrains.Annotations;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        StartScene();
    }

    public void StartScene()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        this.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        this.slider.value = 0.1f;
        yield return new WaitForSeconds(1);
        this.slider.value = 0.2f;
        yield return new WaitForSeconds(1);
        this.slider.value = 0.3f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(StringVariables.sceneToLoad);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 9f);

            slider.value += progress;

            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
