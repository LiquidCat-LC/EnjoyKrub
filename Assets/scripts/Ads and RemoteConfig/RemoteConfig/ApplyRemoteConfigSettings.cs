using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class ApplyRemoteConfigSettings : MonoBehaviour
{
    public static ApplyRemoteConfigSettings Instance {get; private set;}
    
    public bool HalloweenMenu = false;
    
    public Halloweenevent Halloweenevent;
    
    public struct userAttributes
    {
        public int score;
    }
    public struct appAttributes {}

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
                await InitializeRemoteConfigAsync();
        }

        userAttributes uaStruct = new userAttributes
        {
            score = 10
        };
        
        RemoteConfigService.Instance.FetchConfigs(uaStruct, new appAttributes());
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());

        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded this session; using cached values from a previous session.");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("New setting loaded this session; update values accordingly.");
                
                HalloweenMenu = RemoteConfigService.Instance.appConfig.GetBool("HalloweenMenu");
                
                //setupHalloween.SetHalloweenTheme(isHalloween);
                Halloweenevent.HalloweenTheme(HalloweenMenu);
                
                break;
        }
    }
}
