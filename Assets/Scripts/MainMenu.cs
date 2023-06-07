using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    private GameObject gameObjectOfCanvasGroup;
    [SerializeField]
    private GameObject menuOptions;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private float timeOfTransition;
    [SerializeField]
    private BooleanVariables globalBooleanVariables;
    [SerializeField]
    private AudioSource menuMusic;

    [Header("Debug")]
    private string firstSceneName = "0_TelaDeFases";

    void Start()
    {
        if (!globalBooleanVariables.showMainMenu)
        {
            gameObjectOfCanvasGroup.SetActive(true);
            globalBooleanVariables.showMainMenu = true;
        }

        menuMusic.Play();
        Invoke("ExecuteTransition", 3f);
    }

    private void ExecuteTransition()
    {
        StartCoroutine(CanvasGroupOpacityTransition(1f, 0f, "FadeOut"));
        Destroy(gameObjectOfCanvasGroup, timeOfTransition + 0.1f);
    }

    private IEnumerator CanvasGroupOpacityTransition(float startTime, float endTime, string transitionType)
    {
        float velocity = (endTime - startTime) / timeOfTransition;
        canvasGroup.alpha = startTime;

        if (transitionType == "FadeIn")
        {
            while(canvasGroup.alpha < endTime)
            {
                canvasGroup.alpha += velocity * Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while (canvasGroup.alpha > endTime)
            {
                canvasGroup.alpha += velocity * Time.deltaTime;
                yield return null;
            }
        }

        canvasGroup.alpha = endTime;
    }

    public void ButtonOfContinue()
    {
        Time.timeScale = 1;
        StringVariables.sceneToLoad = firstSceneName;
        SceneManager.LoadScene("LoadScene");
    }

    public void ButtonOfNewGame()
    {
        SaveSystem.DeleteLevelGame();
        SaveSystem.DeleteGame();
        ButtonOfContinue();
    }

    public void ButtonOfOptions()
    {
        menuOptions.SetActive(true);
    }

    public void ButtonOfExit()
    {
        Application.Quit();
    }
}
