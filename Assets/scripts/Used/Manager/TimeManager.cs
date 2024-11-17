using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float dayDuration = 180f;
    private float dayTimer;
    public TMP_Text timeText;
    private Coroutine dayCoroutine;
    public SceneManager sceneManager;

    [Header("Set up UI")]
    [SerializeField] Image DayBar;

    void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        dayTimer = dayDuration;
        dayCoroutine = StartCoroutine(DayCountdown());
    }

    public void PauseDay()
    {
        if (dayCoroutine != null)
        {
            StopCoroutine(dayCoroutine);
            dayCoroutine = null;
        }
    }

    public void ResumeDay()
    {
        if (dayCoroutine == null)
        {
            dayCoroutine = StartCoroutine(DayCountdown());
        }
    }

    IEnumerator DayCountdown()
    {
        while (dayTimer > 0)
        {
            dayTimer -= Time.deltaTime;
            DayBar.fillAmount = dayTimer / dayDuration;
            UpdateTimeUI(dayTimer);
            yield return null;
        }

        SceneManager.LoadScene(1);
        Debug.Log("The day has ended.");

    }

    void UpdateTimeUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = $"Time Left: {minutes:00}:{seconds:00}";
    }

}
