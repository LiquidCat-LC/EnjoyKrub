using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockCookTool : Tools
{
    [SerializeField] private bool isCooking = false;
    [SerializeField] private bool isOutOfStock;
    [SerializeField] private int stock;
    [SerializeField] private GameObject stockPrefab;
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

            if (MainDishScript != null && !isCooking && isOutOfStock == true && MainDishScript.cookingState != CookingState.Cooked &&MainDishScript.cookingState != CookingState.Overcooked)
            {
                isReady = true;
                cookingCoroutine = StartCoroutine(Cooking(MainDishScript));
            }
        }

    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        print($"Stock: {stock}, OFS: {isOutOfStock}, IsNewly: {other.GetComponent<Food>().isNewlyCreated}");

        if (stock > 0 && isOutOfStock == false && other.GetComponent<Food>().isNewlyCreated == true && !readyToSwitch)
        {
            takeItem();
        }
        base.OnTriggerExit2D(other);

        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            Debug.Log("Cooking stopped.");
            isCooking = false;
        }
    }
    #endregion

    #region Cook and Take item from stock
    private IEnumerator Cooking(MainDish mainDish)
    {
        MainDish MainDishScript = GetComponentInChildren<MainDish>();
        stockPrefab = mainDish.gameObject;

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

                while (mainDish.cookingTime > 0)
                {
                    mainDish.cookingTime -= Time.deltaTime;

                    yield return null;
                }
                mainDish.cookingState = CookingState.Cooked;
                mainDish.SetCookingStatus(CookingState.Cooked);
                //MainDishScript.GetComponent<SpriteRenderer>().color = Color.green;
                //mainDish.SetCookingStatus(CookingState.Cooked);
                mainDish.isNewlyCreated = true;
                stock = mainDish.stock;

                Debug.Log("Stock :" + stock);
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
            Debug.Log("Remaining stock: " + stock);
            if (stock > 0)
            {
                GameObject newObject = Instantiate(stockPrefab, transform.position + spawnStockPos, Quaternion.identity);
                newObject.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
                newObject.transform.SetParent(transform);
                newObject.GetComponent<Food>().isNewlyCreated = true;
                newObject.GetComponent<Food>().cookingState = CookingState.Cooked;
                newObject.GetComponent<Food>().SetCookingStatus(CookingState.Cooked);
            }
            isOutOfStock = (stock == 0);
            return true;
        }
        else
        {
            Debug.Log("No more items left in stock.");
            isOutOfStock = true;
            stockPrefab = null;
            return false;
        }
    }
    #endregion
}
