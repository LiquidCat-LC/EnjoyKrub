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
    public User user;
    public static string _userId;
    public string userCurrent;

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

    void Start()
    {
        // // ตัวอย่างการใช้งาน: เช็คผลและอัปเดตข้อมูล
        // _userId = "user123"; // User ID ของผู้เล่น
        // user = new User("Player123", 1, 0); // สร้างข้อมูลเริ่มต้นสำหรับผู้เล่น
        // checkResult(_userId, user);
    }

    public void checkResult(string userId, User userData)
    {
        if (TotalCostumer_Success > TotalCostumer_Fail)
        {
            user.levelDay++;
            user.money = moneyCollect;

            string userUrl = $"{url}/users/{userId}.json?auth={secret}";

            // อัปเดตข้อมูลผู้ใช้ใน Firebase
            RestClient
                .Put<User>(userUrl, user)
                .Then(response =>
                {
                    Debug.Log("User data updated successfully!");
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
    float customerSpeed;

    public void CalculateDifficulty(int level)
    {
        customers = baseCustomers + Mathf.FloorToInt(level * 1.2f);
        allowedMistakes = Mathf.Max(baseMistakes - Mathf.FloorToInt(level * 0.5f), 1);
        customerSpeed = 2.0f + (level * speedGrowthRate);

        Debug.Log(
            $"Level {level}: Customers = {customers}, Allowed Mistakes = {allowedMistakes}, Speed = {customerSpeed}"
        );

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
