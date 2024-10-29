using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDish : Food
{
    [Header("Cooking Timer")]
    public float cookingTime; 
    public float maxCookingTime = 15f; 
    void Start()
    {
        _foodCategory = foodCategory.SideDish;
        cookingState = CookingState.Ingred;  
        //GetComponent<SpriteRenderer>().color = Color.red;
    }
    
}
