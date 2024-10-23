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
    
}
