using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StockCookTool : Tools
{
    [SerializeField]
    private bool isCooking = false;

    [SerializeField]
    private bool isOutOfStock;

    [SerializeField]
    private int stock;

    [SerializeField]
    private GameObject stockPrefab;
    private Vector3 spawnStockPos = new Vector3(0, 0, -2);

    private Coroutine cookingCoroutine;

    void Awake()
    {
        _toolCategory = toolCategory.RepeatCookTool;
        isOutOfStock = true;
    }

    #region Detect Food
    public override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);

        if (transform.childCount == 1 && other.CompareTag("Food"))
        {
            MainDish MainDishScript = GetComponentInChildren<MainDish>();

            if (
                MainDishScript != null
                && !isCooking
                && isOutOfStock == true
                && MainDishScript.cookingState != CookingState.Cooked
                && MainDishScript.cookingState != CookingState.Overcooked
            )
            {
                isReady = true;
                cookingCircle.gameObject.SetActive(true);
                cookingCoroutine = StartCoroutine(Cooking(MainDishScript));
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        print(
            $"Stock: {stock}, OFS: {isOutOfStock}, IsNewly: {other.GetComponent<Food>().isNewlyCreated}"
        );

        if (stock > 0 && isOutOfStock == false && !readyToSwitch)
        {
            takeItem();
        }

        Food food = other.GetComponent<Food>();

        if (food != null && table != null && !readyToSwitch)
        {
            other.transform.SetParent(table.transform);
            isReady = false;
            other.GetComponent<Food>().isNewlyCreated = false;
        }

        if (cookingCoroutine != null && isOutOfStock == true)
        {
            StopCoroutine(cookingCoroutine);
            cookingCircle.gameObject.SetActive(false);
            Debug.Log("Cooking stopped.");
            isCooking = false;
        }
    }
    #endregion

    #region Cook and Take item from stock
    [Header("Cooking UI")]
    public Image cookingCircle;
    public TMP_Text stockText;
    public Color cookingColor;
    public Color cookedColor;

    private IEnumerator Cooking(MainDish mainDish)
    {
        MainDish MainDishScript = GetComponentInChildren<MainDish>();

        if (mainDish != null)
        {
            if (mainDish.cookingState == CookingState.Ingred)
            {
                MainDishScript.cookingState = CookingState.Raw;
                //MainDishScript.SetCookingStatus(CookingState.Raw);
            }

            Debug.Log("cooking Main");

            if (mainDish != null)
            {
                isCooking = true;

                float totalCookingTime = mainDish.cookingTime;
                
                cookingCircle.color = cookingColor;

                while (mainDish.cookingTime > 0)
                {
                    mainDish.cookingTime -= Time.deltaTime;

                    float progress = 1 - (mainDish.cookingTime / totalCookingTime);

                    cookingCircle.fillAmount = progress;

                    yield return null;
                }
                mainDish.cookingState = CookingState.Cooked;
                mainDish.SetCookingStatus(CookingState.Cooked);
                cookingCircle.color = cookedColor;
                mainDish.isNewlyCreated = true;
                stock = mainDish.stock;
                stockText.text = stock.ToString();
                stockPrefab = mainDish.gameObject;
                Debug.Log("Stock :" + stock);
                stockText.gameObject.SetActive(true);
                isCooking = false;
                isOutOfStock = (stock == 0);
            }
        }
    }

    public bool takeItem()
    {
        if (stock > 0)
        {
            stock -= 1;
            stockText.text = stock.ToString();

            Debug.Log("Remaining stock: " + stock);
            if (stock > 0)
            {
                GameObject newObject = Instantiate(
                    stockPrefab,
                    transform.position + spawnStockPos,
                    Quaternion.identity,
                    transform
                );
                newObject.transform.localScale = new Vector3(2f, 2f, 2f);
                newObject.GetComponent<MainDish>().isNewlyCreated = true;
                newObject.GetComponent<MainDish>().cookingState = CookingState.Cooked;
                newObject.GetComponent<MainDish>().SetCookingStatus(CookingState.Cooked);
            }
            isOutOfStock = (stock == 0);
            return true;
        }
        else
        {
            stockText.gameObject.SetActive(false);
            Debug.Log("No more items left in stock.");
            isOutOfStock = true;
            stockPrefab = null;
            return false;
        }
    }
    #endregion
}
