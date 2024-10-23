using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CookingTools : Tools
{
    public bool isCooking = false;
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

        if (transform.childCount == 1 && other.CompareTag("Food"))
        {
            SideDish SideDishScript = GetComponentInChildren<SideDish>();

            if (SideDishScript != null && !isCooking)
            {
                isReady = true;
                cookingCoroutine = StartCoroutine(Cooking(SideDishScript));
            }
        }

    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            Debug.Log("Cooking stopped.");
            isCooking = false;
        }

    }

    #endregion


    private IEnumerator Cooking(SideDish sideDish)
    {
        //Debug.Log(sideDish);
        if (sideDish != null)
        {
            isCooking = true;
            while (sideDish.cookingTime > -sideDish.maxCookingTime)
            {

                sideDish.cookingTime -= Time.deltaTime;

                if (sideDish.cookingTime <= 0 && sideDish.cookingState != CookingState.Cooked)
                {
                    Debug.Log("Food is now cooked!");
                    sideDish.cookingState = CookingState.Cooked;
                    sideDish.CookingStatus(CookingState.Cooked);
                }

                if (sideDish.cookingTime <= -sideDish.maxCookingTime && sideDish.cookingState != CookingState.Overcooked)
                {
                    Debug.Log("Food is overcooked!");
                    sideDish.cookingState = CookingState.Overcooked;
                    sideDish.CookingStatus(CookingState.Cooked);
                    break;
                }

                yield return null;
            }

            isCooking = false;
        }
        else
        {
            Debug.LogError("The provided food is not a SideDish!");
        }
    }


}
