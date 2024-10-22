using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IngredientTools : Tools
{
    public GameObject ingredient;
    private Vector3 spawnIngredPos = new Vector3(0,0,-2);

    void Start()
    {
        _toolCategory = toolCategory.IngredientTool;
    }
    
#region Detect Food
    public void OnTriggerEnter2D(Collider2D other)
    {   
        Destroy(other);
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        Destroy(other);
    }
    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        other.GetComponent<Food>().isNewlyCreated=false;
        CheckAndSpawn();
    }
#endregion
    void Destroy(Collider2D other)
    {
        Food otherFood = other.GetComponent<Food>();
        Food ingredientFood = ingredient.GetComponent<Food>();

        if (transform.childCount == 1 && otherFood._itemname == ingredientFood._itemname  && !otherFood.isNewlyCreated )
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
    void CheckAndSpawn()
    {
        if (transform.childCount == 0)
        {
            GameObject newObject = Instantiate(ingredient, transform.position + spawnIngredPos , Quaternion.identity);
            newObject.transform.SetParent(transform);
            newObject.GetComponent<Food>().isNewlyCreated=true;
            Debug.Log("New prefab created.");
        }
    }

}