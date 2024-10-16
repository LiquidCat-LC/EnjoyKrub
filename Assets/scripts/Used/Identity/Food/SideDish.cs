using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDish : Food
{
    
    private void Start()
    {
        _objectType = objectType.Food;
        _foodCategory = foodCategory.SideDish;
        isCooked = false; 
        GetComponent<SpriteRenderer>().color = Color.red;
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
