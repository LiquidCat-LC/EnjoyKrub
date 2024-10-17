using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curry : Food
{
    
    void Start()
    {
        _objectType = objectType.Food;
        _foodCategory = foodCategory.Curry;
        isCooked = true; 
    }


    void Update()
    {
        
    }
}
