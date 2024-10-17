using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum foodCategory
{
    MainDish,
    SideDish,
    Curry
}

public class Food : Identity
{   
    public foodCategory _foodCategory;
    [SerializeField] public bool isCooked;
    [SerializeField] public GameObject dragSystem;
    
    void Awake()
    {
        _objectType = objectType.Food;
    }
    public override void ShowIdentity()
    {
        base.ShowIdentity();
        Debug.Log($"Food Category: {_foodCategory.ToString()}, Cooked: {isCooked}");
    }

}
