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
    Ingred,
    Raw,        
    Cooked,     
    Overcooked  
}

public class Food : Identity
{   
    public foodCategory _foodCategory;
    //public GameObject dragSystem;

    [Header("State status")]
    public List<Sprite> stateSprites; 
    private SpriteRenderer spriteRenderer;
    public CookingState cookingState;
    public bool isNewlyCreated = true;

    void Awake()
    {
        _objectType = objectType.Food;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void ShowIdentity()
    {
        base.ShowIdentity();
        Debug.Log($"Food Category: {_foodCategory.ToString()}, CookingState: {cookingState}");
    }

    public void SetCookingStatus(CookingState CookingState)
    {
        switch(cookingState)
        {    
        case CookingState.Ingred:
        Debug.Log("Ingred");
        break;

        case CookingState.Raw:
        spriteRenderer.sprite = stateSprites[0];
        break;

        case CookingState.Cooked:
        spriteRenderer.sprite = stateSprites[1];
        break;

        case CookingState.Overcooked:
        spriteRenderer.sprite = stateSprites[2];
        break;

        default:
        Debug.LogWarning("Unknown cooking state.");
        break;
        }
    }


}
