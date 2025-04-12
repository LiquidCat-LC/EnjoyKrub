using System;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsSessionTracker : MonoBehaviour
{
    private DateTime sessionStart;
    private string userId;


    async void Start()
    {
        await UnityServices.InitializeAsync();

        sessionStart = DateTime.UtcNow;
        userId = Guid.NewGuid().ToString(); // สร้าง user_id แบบสุ่ม

        var sessionStartEvent = new CustomEvent("session_start");
        sessionStartEvent["time_stampp"] = sessionStart.ToString("o");
        sessionStartEvent["user_id"] = userId;

        Debug.Log("Generated user ID: " + userId);

        AnalyticsService.Instance.RecordEvent(sessionStartEvent);
    }

    void OnApplicationQuit()
{
    DateTime sessionEnd = DateTime.UtcNow;
    TimeSpan sessionLength = sessionEnd - sessionStart;
    double sessionMinutes = sessionLength.TotalMinutes;

    string lengthRange = GetSessionLengthRange(sessionMinutes);

    var sessionEndEvent = new CustomEvent("session_end");
    sessionEndEvent["session_length_min"] = sessionMinutes;
    sessionEndEvent["length_range"] = lengthRange;
    sessionEndEvent["time_stampp"] = sessionEnd.ToString("o");

    AnalyticsService.Instance.RecordEvent(sessionEndEvent);
}

private string GetSessionLengthRange(double minutes)
{
    if (minutes <= 1)
        return "0-1";
    else if (minutes <= 5)
        return "2-5";
    else if (minutes <= 10)
        return "6-10";
    else if (minutes <= 20)
        return "11-20";
    else
        return "20+";
}
}


public static class AnalyticsHelper
{
    public static void TrackLevelAttempt(string levelId, bool isSuccess)
    {
        var attemptEvent = new CustomEvent("level_attempt");
        attemptEvent["Level_id"] = levelId;
        attemptEvent["result"] = isSuccess ? "success" : "fail";

        AnalyticsService.Instance.RecordEvent(attemptEvent);
        Debug.Log($"[Analytics] Level Attempt - ID: {levelId}, Result: {(isSuccess ? "success" : "fail")}");
    }

    public static void TrackRetry(string levelId)
    {
        var attemptEvent = new CustomEvent("level_attempt");
        attemptEvent["Level_id"] = levelId;
        attemptEvent["result"] = "retry";

        AnalyticsService.Instance.RecordEvent(attemptEvent);
        Debug.Log($"[Analytics] Level Attempt - ID: {levelId}, Result: retry");
    }
}



