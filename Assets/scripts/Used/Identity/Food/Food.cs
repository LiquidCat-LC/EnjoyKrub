using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum foodCategory
{
    MainDish,
    SideDish,
    Curry
}

public enum CookingState
{
    Raw,        
    Cooked,     
    Overcooked  
}

public class Food : Identity
{   
    //เก็บไว้
    protected foodCategory _foodCategory;

    //โดนยืมแน่
    public GameObject dragSystem;
    public CookingState cookingState;
    void Awake()
    {
        _objectType = objectType.Food;
    }
    public override void ShowIdentity()
    {
        base.ShowIdentity();
        Debug.Log($"Food Category: {_foodCategory.ToString()}, CookingState: {cookingState}");
    }

}
