using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDish : Food
{
    public float cookingTime;
    public int stock;
    void Awake()
    {
        _foodCategory = foodCategory.MainDish;
        cookingState = CookingState.Raw; 
    }

    
}
