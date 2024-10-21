using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum toolCategory
{
    IngredientTool,
    CookingTool
}
public class Tools : Identity
{
    //เก็บไว้
    protected toolCategory _toolCategory;
    
    //โดนยืมแน่
    public GameObject dragSystem;
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
                other.transform.position = transform.position + new Vector3(0,0,-2);  
            }
        }
        
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Food") &&  other.transform.parent == transform)
        {
            other.transform.SetParent(null);
            isReady = false;
            //Debug.Log("Exit");
        }
       
    }
#endregion

 
}
