using System.Collections;
using System.Collections.Generic;
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

    public virtual void OnTriggerStay2D(Collider2D other)
    {
        bool drag = dragSystem.GetComponent<DragTest1>()._isDragging;

        if (drag == false && other.CompareTag("Food"))
        {
            Food food = other.GetComponent<Food>();
            switch (food._foodCategory)
            {
                case foodCategory.MainDish:
                    Debug.Log("Main");
                    other.transform.SetParent(transform);
                    other.transform.position = mainDishPos.transform.position;
                    mainDishOnPlate = other.gameObject;
                    break;
                case foodCategory.SideDish:
                    Debug.Log("Side");
                    other.transform.SetParent(transform);
                    other.transform.position = sideDishPos.transform.position;
                    sideDishOnPlate = other.gameObject;
                    break;
                case foodCategory.Curry:
                    Debug.Log("Curry");
                    other.transform.SetParent(transform);
                    other.transform.position = curryPos.transform.position;
                    curryOnPlate = other.gameObject;
                    break;
                default:
                    Debug.Log("Nope");
                    break;
            }
        }
    }

}
