using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IngredientTools : Tools
{
    public GameObject ingredient;
    void Start()
    {
        _toolCategory = toolCategory.IngredientTool;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {  
        if (transform.childCount == 1 && other.name == ingredient.name)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else
        {
            print(other.name);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
    
    }

}
