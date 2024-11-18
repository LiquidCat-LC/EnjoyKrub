using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Threading.Tasks;
using Proyecto26;
using SimpleJSON;

public class StartScene : MonoBehaviour
{
    public const string url =
        "https://enjoykrub-ae32e-default-rtdb.asia-southeast1.firebasedatabase.app";
    public const string secret = "HDRqy4XcxgYRVXYJsKpV6y4efQhyqa4vocBp2mDl";
    public Button startButton;
    public PlayerManager _player;

    void Start()
    {
        if (startButton != null)
        {
            startButton.interactable = false; // ปิดปุ่ม
        }
    }

    //Save1 user123
    public void user123Save()
    {
        StartCoroutine(LoadUserData("user123"));
    }


    private IEnumerator LoadUserData(string userId)
    {
        //string userId = "user123"; // User ID ตัวอย่าง
        yield return StartCoroutine(GetUserLevelCoroutine(userId));

        if (startButton != null)
        {
            startButton.interactable = true; // เปิดปุ่มเมื่อโหลดข้อมูลเสร็จ
        }
    }

    private IEnumerator GetUserLevelCoroutine(string userId)
    {
        string userUrl = $"{url}/users/{userId}.json?auth={secret}";

        var request = RestClient.Get<User>(userUrl);
        bool isCompleted = false;

        request
            .Then(response =>
            {
                User userData = response;
                Debug.Log($"User Level: {userData.levelDay}, Money: {userData.money}");
                _player.level = userData.levelDay;
                _player.CalculateDifficulty(userData.levelDay);
                isCompleted = true;
            })
            .Catch(error =>
            {
                Debug.LogError($"Failed to fetch user data: {error.Message}");
                isCompleted = true;
            });

        yield return new WaitUntil(() => isCompleted);
    }

}
