using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proyecto26;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartScene : MonoBehaviour
{
    public const string url =
        "https://enjoykrub-ae32e-default-rtdb.asia-southeast1.firebasedatabase.app";
    public const string secret = "HDRqy4XcxgYRVXYJsKpV6y4efQhyqa4vocBp2mDl";
    public Button startButton;
    public PlayerManager _player;

    void Start()
    {
        part1Panel.gameObject.SetActive(true);

        if (startButton != null)
        {
            startButton.interactable = false;
        }
    }

    //Save1 user123
    public void user123Save()
    {
        StartCoroutine(LoadUserData("user123"));
    }

    private IEnumerator LoadUserData(string userId)
    {
        //string userId = "user123";
        yield return StartCoroutine(GetUserLevelCoroutine(userId));

        if (startButton != null)
        {
            Debug.Log(
                 $"User Info - ID: {_player.userNowList.FirstOrDefault()?.IDname}, Level: {_player.level}, Money: {_player.moneyCollect}"
            );
            startButton.interactable = true;
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

    public void QuitApp()
    {
        Application.Quit();
    }

    #region Animation
    [SerializeField] private RectTransform part1Panel;
    [SerializeField] private RectTransform part2Panel;
    [SerializeField] private RectTransform rotatingRect;
    [SerializeField] private GameObject saveDataPanel;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private TMP_Text NameText;
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text MoneyText;

    private bool spriteChanged = false;

    public void LoadSave()
    {
        saveDataPanel.SetActive(false);
        StartCoroutine(RotateAndShowData());
    }

    private IEnumerator RotateAndShowData()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float angle = Mathf.Lerp(0, 360, elapsed / duration);
            rotatingRect.rotation = Quaternion.Euler(0, angle, 0);

            if (!spriteChanged && angle >= 90)
            {
                ChangeSprite();
                spriteChanged = true;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        rotatingRect.rotation = Quaternion.Euler(0, 0, 0);
        ShowSaveData();
    }

    private void ShowSaveData()
    {
        NameText.text = _player.userNowList.FirstOrDefault()?.IDname.ToString();
        LevelText.text = _player.level.ToString();
        MoneyText.text = _player.moneyCollect.ToString();
        saveDataPanel.SetActive(true);
    }

    private void ChangeSprite()
    {
        Image rectImage = rotatingRect.GetComponent<Image>();
        if (rectImage != null && newSprite != null)
        {
            rectImage.sprite = newSprite;
            Debug.Log("Sprite changed at 90 degrees.");
        }
    }

    public void Slide()
    {
        StartCoroutine(SlideOut(part1Panel));
    }
    private IEnumerator SlideIn(RectTransform panel)
    {
        float elapsedTime = 0f;
        Vector2 startPos = new Vector2(panel.anchoredPosition.x, Screen.height);
        Vector2 targetPos = new Vector2(0,0);
        panel.anchoredPosition = startPos;
        panel.gameObject.SetActive(true);

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 1.0f;
            panel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        panel.anchoredPosition = targetPos;
    }

    private IEnumerator SlideOut(RectTransform panel)
    {
        float elapsedTime = 0f;
        Vector2 startPos = panel.anchoredPosition;
        Vector2 hiddenPosition = new Vector2(panel.anchoredPosition.x, -Screen.height);

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / 1.0f;
            panel.anchoredPosition = Vector2.Lerp(startPos, hiddenPosition, t);

            yield return null;
        }

        panel.gameObject.SetActive(false);
        panel.anchoredPosition = startPos;
        StartCoroutine(SlideIn(part2Panel));

    }

    #endregion
}
