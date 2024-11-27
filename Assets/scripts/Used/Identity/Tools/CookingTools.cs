using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CookingTools : Tools
{
    public bool isCooking = false;
    public List<GameObject> allowedFoodPrefabs;
    private Coroutine cookingCoroutine;

    void Awake()
    {
        isCooking = false;
        _toolCategory = toolCategory.CookingTool;
    }

    #region Detect Food
    public override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        
        if (transform.childCount == 1 && other.CompareTag("Food")&&other.GetComponent<SideDish>().cookingState != CookingState.Cooked&&other.GetComponent<SideDish>().cookingState != CookingState.Overcooked)
        {
            SideDish SideDishScript = GetComponentInChildren<SideDish>();

            foreach (GameObject food in allowedFoodPrefabs)
            {
                SideDish foodd = food.GetComponent<SideDish>();
                if (
                    SideDishScript != null
                    && !isCooking
                    && foodd._itemname == SideDishScript._itemname
                )
                {
                    isReady = true;
                    cookingCoroutine = StartCoroutine(Cooking(SideDishScript));

                }
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            cookingCircle.gameObject.SetActive(false);
            Debug.Log("Cooking stopped.");
            isCooking = false;
        }
    }

    #endregion

    [Header("Cooking UI")]
    public Image cookingCircle; 
    public Color cookingColor; 
    public Color cookedColor;
    public Color burntColor;

    private IEnumerator Cooking(SideDish sideDish)
    {
        cookingCircle.gameObject.SetActive(true);
        if (sideDish.cookingState == CookingState.Ingred)
        {
            sideDish.cookingState = CookingState.Raw;
            sideDish.SetCookingStatus(CookingState.Raw);
        }

        if (sideDish != null)
        {
            isCooking = true;

            float totalCookingTime = sideDish.maxCookingTime * 2;

            while (sideDish.cookingTime > -sideDish.maxCookingTime)
            {
                sideDish.cookingTime -= Time.deltaTime;

                float progress = 1 - (sideDish.cookingTime + sideDish.maxCookingTime) / totalCookingTime;

                cookingCircle.fillAmount = progress;

                if (sideDish.cookingTime <= 0 && sideDish.cookingState != CookingState.Cooked)
                {
                    Debug.Log("Food is now cooked!");
                    sideDish.cookingState = CookingState.Cooked;
                    sideDish.SetCookingStatus(CookingState.Cooked);
                    cookingCircle.color = cookedColor;
                }
                else if (
                    sideDish.cookingTime <= -sideDish.maxCookingTime
                    && sideDish.cookingState != CookingState.Overcooked
                )
                {
                    Debug.Log("Food is overcooked!");
                    sideDish.cookingState = CookingState.Overcooked;
                    sideDish.SetCookingStatus(CookingState.Overcooked);
                    cookingCircle.color = burntColor;
                    break;
                }
                else if (sideDish.cookingState == CookingState.Raw)
                {
                    cookingCircle.color = cookingColor;
                }

                yield return null;
            }

            cookingCircle.gameObject.SetActive(false);
            isCooking = false;
        }
        else
        {
            Debug.LogError("The provided food is not a SideDish!");
        }
    }
}
