using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDish : Food
{
    public float cookingTime;
    public int stock;
    void Start()
    {
        _foodCategory = foodCategory.MainDish;
        cookingState = CookingState.Ingred; 
    }

    
}
