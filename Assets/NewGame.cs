using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {

    public Slider slider;
    public GameObject loadingScreen;
    public GameObject menuScreen;
    public Text text;

    public void LoadFirstScene()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingScreen.SetActive(true);
        menuScreen.SetActive(false);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            text.text = progress * 100f + " %";

            yield return null;
        }

    }
}

