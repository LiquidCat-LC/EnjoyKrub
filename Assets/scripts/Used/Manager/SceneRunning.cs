using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneRunning : MonoBehaviour
{
    public PlayerManager _player;

    [Header("Loading UI")]
    public GameObject loadingPanel;
    public Image loadingProgressBar;
    public TMP_Text loadingProgressText;

    //กดเริ่มเกมหลังLoad save
    public void StartGame()
    {
        LoadSceneWithLoading("Demo");
    }
    public void LoadSceneWithLoading(string sceneName)
    {
        loadingPanel.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingProgressBar != null)
                loadingProgressBar.fillAmount = progress;

            if (loadingProgressText != null)
                loadingProgressText.text = $"{(progress * 100):0}%";

            yield return null;
        }
    }
}
