using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDish : Food
{
    void Start()
    {
        _foodCategory = foodCategory.MainDish;
        isCooked = true; 
    }

    
}
