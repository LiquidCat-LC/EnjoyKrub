using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Result : MonoBehaviour
{
    public Button checkButton;
    public PlayerManager _player;
    [Header("Text")]
    [SerializeField] private TMP_Text failText;
    [SerializeField] private TMP_Text successText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text overallText;
    [Header("sprite")]
    public GameObject playButton;
    [SerializeField] private Sprite rePlay;
    [SerializeField] private Sprite playNext;

    public void Start()
    {
        _player = FindObjectOfType<PlayerManager>();

        if (checkButton != null)
        {
            checkButton.onClick.AddListener(checkRest);
        }
        seeResultPanel.SetActive(true);
        billPanel.gameObject.SetActive(false);
        hiddenPosition = new Vector2(-Screen.width, billPanel.anchoredPosition.y);
        visiblePosition = new Vector2(targetXPosition, billPanel.anchoredPosition.y);

        billPanel.anchoredPosition = hiddenPosition;
    }

    public void checkRest()
    {
        failText.text = $"{_player.TotalCostumer_Fail.ToString()} \n / \n {_player.allowedMistakes.ToString()}";
        successText.text = _player.TotalCostumer_Success.ToString();
        moneyText.text = _player.TotalMoney.ToString();
        _player.checkResult("user123");
        if(_player.checkResult("user123") == true)
        {
            overallText.text = "Pass" ;
            playButton.GetComponent<Image>().sprite = playNext;
        }
        else
        {
            overallText.text = "Fail" ;
            playButton.GetComponent<Image>().sprite = rePlay;
        }
    }

    #region Animation
    [Header("Animation setup")]
    public GameObject seeResultPanel;
    [SerializeField] private RectTransform billPanel;
    [SerializeField] private float slideDuration = 2.0f;
    private float targetXPosition = 0f;

    private Vector2 hiddenPosition;
    private Vector2 visiblePosition;
    private bool isVisible = false;

    public void ToggleResultPanel()
    {
        if (isVisible)
        {
            StartCoroutine(SlideOut());
        }
        else
        {
            StartCoroutine(SlideIn());
        }
        isVisible = !isVisible;
    }

    private IEnumerator SlideIn()
    {
        float elapsedTime = 0f;
        Vector2 startPos = billPanel.anchoredPosition; 
        Vector2 targetPos = visiblePosition;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slideDuration;
            billPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            yield return null;
        }

        billPanel.anchoredPosition = targetPos;
    }

    private IEnumerator SlideOut()
    {
        float elapsedTime = 0f;
        Vector2 startPos = billPanel.anchoredPosition;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slideDuration;
            billPanel.anchoredPosition = Vector2.Lerp(startPos, hiddenPosition, t);
            yield return null;
        }

        billPanel.anchoredPosition = hiddenPosition;
    }

    #endregion

}
