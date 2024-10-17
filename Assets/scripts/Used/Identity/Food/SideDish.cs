using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDish : Food
{
    void Start()
    {
        _foodCategory = foodCategory.SideDish;
        isCooked = false; 
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public void Cooked(bool isCooked)
    {
        if(isCooked == true)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        
    }

    
}
