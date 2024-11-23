using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IngredientTools : Tools
{
    public GameObject ingredPrefab;
    public Vector3 spawnIngredPos = new Vector3(0,0,-2);
    public Vector3 ingredSize;

    void Start()
    {
        _toolCategory = toolCategory.IngredientTool;
    }

#region Detect Food
    public virtual void OnTriggerEnter2D(Collider2D other)
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
        CheckAndSpawn();
    }
#endregion

#region Spawn and Destroy
    void Destroy(Collider2D other)
    {
        Food otherFood = other.GetComponent<Food>();
        Food ingredientFood = ingredPrefab.GetComponent<Food>();

        if (transform.childCount == 1 && otherFood._itemname == ingredientFood._itemname  && !otherFood.isNewlyCreated && otherFood.cookingState == CookingState.Ingred)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
    private  void CheckAndSpawn()
    {
        if (transform.childCount == 0)
        {
            GameObject newObject = Instantiate(ingredPrefab, transform.position + spawnIngredPos , Quaternion.identity);
            //newObject.transform.localScale = ingredSize;
            newObject.transform.SetParent(transform);
            newObject.GetComponent<Food>().isNewlyCreated=true;
            Debug.Log("New prefab created.");
        }
    }
#endregion
}
