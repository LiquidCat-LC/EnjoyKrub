using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum toolCategory
{
    IngredientTool,
    CookingTool,
    RepeatCookTool
}
public class Tools : Identity
{
    //เก็บไว้
    protected toolCategory _toolCategory;
    public GameObject dragSystem;
    public GameObject table;
    public bool readyToSwitch;
    [Header("State")]
    public bool isReady = false;
    void Awake()
    {
        isReady = false;
        _objectType = objectType.CookingTool;
    }


    #region Detect Food
    public virtual void OnTriggerStay2D(Collider2D other)
    {
        bool drag = dragSystem.GetComponent<DragTest1>()._isDragging;
        if (transform.childCount == 0 && drag == false)
        {
            if (other.CompareTag("Food"))
            {
                isReady = true;
                other.transform.SetParent(transform);
                other.transform.position = transform.position + new Vector3(0, 0.5f, -2);
            }
        }

    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        Food food = other.GetComponent<Food>();

        if (food != null && table != null && !readyToSwitch)
        {
            other.transform.SetParent(table.transform);
        }
        // else
        // {
        //     Debug.LogWarning("Food or table is null.");
        //     other.transform.SetParent(null);
        // }

        isReady = false;
        other.GetComponent<Food>().isNewlyCreated = false;
    }
    #endregion


}
