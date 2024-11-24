using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plate : Identity
{
    public GameObject dragSystem;

    [Header("Setting Position")]
    public GameObject mainDishPos;
    public GameObject sideDishPos;
    public GameObject curryPos;

    [Header("Food on Plate")]
    public GameObject mainDishOnPlate;
    public GameObject sideDishOnPlate;
    public GameObject curryOnPlate;

    public void OnTriggerStay2D(Collider2D other)
    {
        bool drag = dragSystem.GetComponent<DragTest1>()._isDragging;

        if (drag == false && other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            switch (food._foodCategory)
            {
                case foodCategory.MainDish:
                    Debug.Log("Main");
                    if (mainDishOnPlate == null)
                    {
                        other.transform.SetParent(transform);
                        other.transform.position = mainDishPos.transform.position;
                        mainDishOnPlate = other.gameObject;
                        other.GetComponent<Collider2D>().enabled = false;
                    }
                    else{
                        Debug.Log("There is already a main dish.");
                    }
                    break;
                case foodCategory.SideDish:
                    Debug.Log("Side");
                    if (sideDishOnPlate == null)
                    {
                        other.transform.SetParent(transform);
                        other.transform.position = sideDishPos.transform.position;
                        sideDishOnPlate = other.gameObject;
                        other.GetComponent<Collider2D>().enabled = false;
                    }
                    else{
                        Debug.Log("There is already a side dish.");
                    }
                    break;
                case foodCategory.Curry:
                    Debug.Log("Curry");
                    if (curryOnPlate == null)
                    {
                        other.transform.SetParent(transform);
                        other.transform.position = curryPos.transform.position;
                        curryOnPlate = other.gameObject;
                        other.GetComponent<Collider2D>().enabled = false;
                    }
                    else{
                        Debug.Log("There is already a Curry.");
                    }
                    break;
                default:
                    Debug.Log("Food only");
                    break;
            }
        }
    }

    public List<GameObject> GetAllDishes()
    {
        List<GameObject> dishes = new List<GameObject>();

        if (mainDishOnPlate != null) dishes.Add(mainDishOnPlate);
        if (sideDishOnPlate != null) dishes.Add(sideDishOnPlate);
        if (curryOnPlate != null) dishes.Add(curryOnPlate);

        return dishes;
    }

    public bool CheckCookedStatus()
    {
        List<GameObject> dishes = GetAllDishes();
        
        if (dishes.Count < 3)
        {
            Debug.Log("No dishes on the plate.");
            return false;
        }

        foreach (GameObject dish in dishes)
        {
            Food foodScript = dish.GetComponent<Food>();
            if (foodScript != null)
            {
                if (foodScript.cookingState != CookingState.Cooked)
                {
                    Debug.Log($"{foodScript.name} is not cooked.");
                    return false; 
                }
            }
        }
        
        Debug.Log("All dishes are cooked.");
        return true;
    }

}
