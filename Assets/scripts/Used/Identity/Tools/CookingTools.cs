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

        if (transform.childCount == 1 && other.CompareTag("Food"))
        {
            SideDish SideDishScript = GetComponentInChildren<SideDish>();

            foreach (GameObject food in allowedFoodPrefabs)
            {
                Food foodd = food.GetComponent<Food>();
                if (
                    SideDishScript != null
                    && !isCooking
                    && foodd._itemname == SideDishScript._itemname
                )
                {
                    isReady = true;
                    cookingCoroutine = StartCoroutine(Cooking(SideDishScript));
                    cookingCircle.gameObject.SetActive(true);

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
    public Image cookingCircle; // Image วงกลมสำหรับแสดงสถานะ
    public Color cookingColor = Color.green; // สีขณะกำลังทำอาหาร (Raw)
    public Color cookedColor = Color.yellow; // สีหลังจากสุก (Cooked)
    public Color burntColor = Color.red; // สีหลังจากไหม้ (Overcooked)

    private IEnumerator Cooking(SideDish sideDish)
    {
        if (sideDish.cookingState == CookingState.Ingred)
        {
            sideDish.cookingState = CookingState.Raw;
            sideDish.SetCookingStatus(CookingState.Raw);
        }

        if (sideDish != null)
        {
            isCooking = true;

            // เชื่อมโยงกับ UI
            float totalCookingTime = sideDish.maxCookingTime * 2; // รวมเวลาทั้งหมด (เวลาสุก + ไหม้)
            float cookingStartTime = sideDish.cookingTime + sideDish.maxCookingTime; // เวลาเริ่มต้น

            while (sideDish.cookingTime > -sideDish.maxCookingTime)
            {
                sideDish.cookingTime -= Time.deltaTime;

                // คำนวณ progress
                float progress = 1 - (sideDish.cookingTime + sideDish.maxCookingTime) / totalCookingTime;

                // อัปเดต UI วงกลม
                cookingCircle.fillAmount = progress;

                // เปลี่ยนสีตามสถานะ
                if (sideDish.cookingTime <= 0 && sideDish.cookingState != CookingState.Cooked)
                {
                    Debug.Log("Food is now cooked!");
                    sideDish.cookingState = CookingState.Cooked;
                    sideDish.SetCookingStatus(CookingState.Cooked);
                    cookingCircle.color = cookedColor; // เปลี่ยนสีเป็นสีของ Cooked
                }
                else if (
                    sideDish.cookingTime <= -sideDish.maxCookingTime
                    && sideDish.cookingState != CookingState.Overcooked
                )
                {
                    Debug.Log("Food is overcooked!");
                    sideDish.cookingState = CookingState.Overcooked;
                    sideDish.SetCookingStatus(CookingState.Overcooked);
                    cookingCircle.color = burntColor; // เปลี่ยนสีเป็นสีของ Overcooked
                    break;
                }
                else if (sideDish.cookingState == CookingState.Raw)
                {
                    cookingCircle.color = cookingColor; // สีขณะกำลังทำอาหาร
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
