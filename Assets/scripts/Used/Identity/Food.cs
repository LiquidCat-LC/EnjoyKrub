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
    [SerializeField] protected string foodCategory;
    [SerializeField] public bool isCooked;
    

    public override void ShowIdentity()
    {
        base.ShowIdentity();
        Debug.Log($"Food Category: {foodCategory}, Cooked: {isCooked}");
    }
    

}
