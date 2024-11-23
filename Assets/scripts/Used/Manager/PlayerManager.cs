using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto26;
using SimpleJSON;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public const string url =
        "https://enjoykrub-ae32e-default-rtdb.asia-southeast1.firebasedatabase.app";
    public const string secret = "HDRqy4XcxgYRVXYJsKpV6y4efQhyqa4vocBp2mDl";
    public static string _userId;
    public string userCurrent;
    public List<User> userNowList = new List<User>();

    [Header("Overall")]
    public int TotalCostumer_Success;
    public int TotalCostumer_Fail;
    public int moneyCollect;

    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // ป้องกันการสร้างออบเจ็กต์ซ้ำ
        }
    }

    #region Test
    public void inputID(string userID)
    {
        string userUrl = $"{url}/users/{userID}.json?auth={secret}";

        User newUser = new User(userID, 1, 100); // ใช้ userID ใน User object ด้วย

        RestClient
            .Put(userUrl, newUser)
            .Then(response =>
            {
                Debug.Log($"User data for {userID} saved successfully.");
            })
            .Catch(error =>
            {
                Debug.LogError($"Failed to save user data: {error.Message}");
            });
    }

    public void updateUser(string userId)
    {
        string userUrl = $"{url}/users/{userId}.json?auth={secret}";

        User updatedUser = new User(userId, level, moneyCollect);

        RestClient
            .Put(userUrl, updatedUser)
            .Then(response =>
            {
                Debug.Log($"User data for {userId} updated successfully!");
            })
            .Catch(error =>
            {
                Debug.LogError($"Failed to update user data: {error.Message}");
            });
    }
    #endregion
    public void checkResult(string userId)
    {
        if (TotalCostumer_Fail <= allowedMistakes)
        {
            level++; // เพิ่มเลเวล
            string userUrl = $"{url}/users/{userId}.json?auth={secret}";

            User updatedUser = new User(userId, level, moneyCollect);

            RestClient
                .Put(userUrl, updatedUser)
                .Then(response =>
                {
                    Debug.Log($"User data updated successfully! New Level: {level}");
                })
                .Catch(error =>
                {
                    Debug.LogError($"Failed to update user data: {error.Message}");
                });
        }
        else
        {
            Debug.Log("No update needed: Too many failed customers.");
        }
    }

    #region Difficulty

    [Header("Difficulty : Base")]
    public bool isDifficultyCalculated = false;
    public int level;
    public int baseCustomers = 5;
    public int baseMistakes = 3;
    public float speedGrowthRate = 0.2f;

    [Header("Difficulty : current")]
    public int customers;
    int allowedMistakes;

    public void CalculateDifficulty(int level)
    {
        customers = baseCustomers + Mathf.FloorToInt(level * 1.2f);
        allowedMistakes = Mathf.Max(baseMistakes + Mathf.FloorToInt(customers * 0.2f), 3);

        Debug.Log($"Level {level}: Customers = {customers}, Allowed Mistakes = {allowedMistakes}");

        isDifficultyCalculated = true;
    }

    #endregion
}

public class User
{
    public string IDname;
    public int levelDay;
    public int money;

    public User(string _name, int _levelDay, int _money)
    {
        this.IDname = _name;
        this.levelDay = _levelDay;
        this.money = _money;
    }
}
