using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDish : Food
{
    public float cookingTime; 
    public float maxCookingTime = 15f; 
    void Start()
    {
        _foodCategory = foodCategory.SideDish;
        cookingState = CookingState.Raw;  
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void CookingStatus(CookingState CookingState)
    {
        switch(cookingState)
        {
        case CookingState.Raw:
        GetComponent<SpriteRenderer>().color = Color.red;
        break;

        case CookingState.Cooked:
        GetComponent<SpriteRenderer>().color = Color.green;
        break;

        case CookingState.Overcooked:
        GetComponent<SpriteRenderer>().color = Color.blue;
        break;

        default:
        Debug.LogWarning("Unknown cooking state.");
        break;
        }
    }

    
}
