using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto26;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            Debug.Log(
                 $"User Info - ID: {_player.userNowList.FirstOrDefault()?.IDname}, Level: {_player.level}, Money: {_player.moneyCollect}"
            );
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
                Debug.Log($"Fetched User Level: {userData.levelDay}, Money: {userData.money}");

                // อัปเดตใน PlayerManager
                _player.userNowList.Clear();
                _player.userNowList.Add(userData);
                _player.level = userData.levelDay;
                _player.moneyCollect = userData.money;

                _player.CalculateDifficulty(_player.level);

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
