using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class TestAnalyticsEvent : MonoBehaviour
{
    public bool wasSentSuccessfully = false;
    void Awake()
    {
        Initialize();
    }
    
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();

            var testEvent = new CustomEvent("test_event");
            testEvent["debug_value"] = "hello_analytics";

            AnalyticsService.Instance.RecordEvent(testEvent);

            wasSentSuccessfully = true;
            Debug.Log("✅ Sent test_event to Unity Analytics");
        }
        catch (System.Exception e)
        {
            wasSentSuccessfully = false;
            Debug.LogError("❌ Failed to send test_event: " + e.Message);
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Sending test_event...");
            var testEvent = new CustomEvent("test_event");
            testEvent["debug_value"] = "Press";
            AnalyticsService.Instance.RecordEvent(testEvent);
            Debug.Log("test_event sent.");

        }
    }

    private async void Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }
}
