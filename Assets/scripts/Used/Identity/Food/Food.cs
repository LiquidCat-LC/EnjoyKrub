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
    public bool isNewlyCreated = true;
    void Awake()
    {
        _objectType = objectType.Food;
    }
    public override void ShowIdentity()
    {
        base.ShowIdentity();
        Debug.Log($"Food Category: {_foodCategory.ToString()}, CookingState: {cookingState}");
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
