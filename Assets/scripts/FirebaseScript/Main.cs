using UnityEngine;
using System.Linq;
using Proyecto26;
using SimpleJSON;

public class Main : MonoBehaviour
{
    /// <summary>
    /// This class is just the implementation of all the functions shown in AuthHandler
    /// - It will sign up a user to Firebase Auth
    /// - It will sign in a user to Firebase Auth
    /// </summary>
    /// 
    public const string url =
        "https://enjoykrub-ae32e-default-rtdb.asia-southeast1.firebasedatabase.app";
    public const string secret = "HDRqy4XcxgYRVXYJsKpV6y4efQhyqa4vocBp2mDl";
    public User user;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnAppStart()
    {
        //AuthHandler.SignUp("tomprs3@mailserp.com", "BestPasswordEver", new User("Tester3", 1, 1));
        // AuthHandler.SignIn("tomprs3@mailserp.com", "BestPasswordEver");
        // Debug.Log(AuthHandler.userId);
    }
    public static void LoginTest()
    {
    }
    
}
