using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingTools : Identity
{
    public GameObject dragSystem;
    public bool isReady = false;
    public bool isCooking = false;
    
    void Awake()
    {
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
                other.transform.SetParent(transform);
                other.transform.position = transform.position + new Vector3(0,0,-2);
                SideDish SideDishScript = GetComponentInChildren<SideDish>();
                if(SideDishScript != null)
                {
                    Debug.Log("Start");
                    isReady = true;
                    StartCoroutine(Cooking(SideDishScript));
                }
                
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Food") &&  other.transform.parent == transform)
        {
            other.transform.SetParent(null);
            isReady = false;
            //Debug.Log("Exit");
        }
       
    }
#endregion


    private IEnumerator Cooking(SideDish sideDish)
    {
        Debug.Log(sideDish);
        if (sideDish != null)
        {
            while (sideDish.cookingTime > -sideDish.maxCookingTime)
            {
        
                sideDish.cookingTime -= Time.deltaTime;

                if (sideDish.cookingTime <= 0 && sideDish.cookingState != CookingState.Cooked)
                {
                    Debug.Log("Food is now cooked!");
                    sideDish.cookingState = CookingState.Cooked;
                    sideDish.AlreadyCooked(CookingState.Cooked);
                }

                if (sideDish.cookingTime <= -sideDish.maxCookingTime && sideDish.cookingState != CookingState.Overcooked)
                {
                    Debug.Log("Food is overcooked!");
                    sideDish.cookingState = CookingState.Overcooked;
                    sideDish.AlreadyCooked(CookingState.Cooked);
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
