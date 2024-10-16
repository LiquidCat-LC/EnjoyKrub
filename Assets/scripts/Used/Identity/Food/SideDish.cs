using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDish : Food
{
    private void Start()
    {
        objectType = "Food";
        foodCategory = "Side Dish";
        isCooked = false; 
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }


    void Update()
    {
        
    }

    public void Cooked(bool isCooked)
    {
        if(isCooked == true)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        
    }
}
