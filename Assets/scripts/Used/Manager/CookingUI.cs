using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingUI : MonoBehaviour
{
    [Header("Cooking Settings")]
    public GameObject food;

    [Header("UI Elements")]
    public Image cookingCircle;
    public Color cookingColor = Color.green;
    public Color cookedColor = Color.yellow;
    public Color burntColor = Color.red;

    private float currentTime;
    private bool isCooking = false;

    IEnumerator CookingCountdown()
    {
        float cookTime = food.GetComponent<SideDish>().cookingTime;
        float maxBurnTime = food.GetComponent<SideDish>().maxCookingTime;
        while (currentTime <= cookTime + maxBurnTime)
        {
            currentTime += Time.deltaTime;

            if (currentTime <= cookTime)
            {
                cookingCircle.fillAmount = currentTime / cookTime;
                cookingCircle.color = cookingColor;
            }
            else
            {
                cookingCircle.fillAmount = (currentTime - cookTime) / maxBurnTime;
                cookingCircle.color = burntColor;
            }

            yield return null;
        }

        isCooking = false;
        Debug.Log("Cooking completed or burnt.");
    }

    public void StopCooking()
    {
        isCooking = false;
        StopCoroutine(CookingCountdown());
        Debug.Log("Cooking stopped.");
    }

}
