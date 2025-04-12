using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float dayDuration = 360f;
    private float elapsedTimer = 0f; 
    public TMP_Text timeText;
    private Coroutine dayCoroutine;
    public SceneRunning _Loading;

    [Header("Set up UI")]
    public Slider daySlider;

    void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        elapsedTimer = 0f;

        if (daySlider != null)
        {
            
            daySlider.minValue = 0;
            daySlider.maxValue = dayDuration;
            daySlider.value = 0;
        }


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
        while (elapsedTimer < dayDuration)
        {
            elapsedTimer += Time.deltaTime;
            elapsedTimer = Mathf.Clamp(elapsedTimer, 0, dayDuration); 

            if (daySlider != null)
            {
                daySlider.value = elapsedTimer;
            }

            UpdateTimeUI(dayDuration - elapsedTimer);

            yield return null;
        }

        OnDayEnd();
    }

    void UpdateTimeUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        if (timeText != null)
        {
            timeText.text = $"Time Left: {minutes:00}:{seconds:00}";
        }
    }

    void OnDayEnd()
    {
        if (_Loading != null)
        {
            _Loading.LoadSceneWithLoading("Demo");
        }

        Debug.Log("The day has ended.");
    }

}
