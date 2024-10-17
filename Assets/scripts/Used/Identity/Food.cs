using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum foodCategory
{
    MainDish,
    SideDish,
    Curry
}

public class Food : Identity
{   
    public foodCategory _foodCategory;
    [SerializeField] public bool isCooked;
    [SerializeField] public GameObject dragSystem;
    
    public override void ShowIdentity()
    {
        base.ShowIdentity();
        Debug.Log($"Food Category: {_foodCategory.ToString()}, Cooked: {isCooked}");
    }
    
    public virtual void OnTriggerStay2D(Collider2D other)
    {   bool drag = dragSystem.GetComponent<DragTest1>()._isDragging;

        if (other.CompareTag("CookingTools") && drag == false)
        {
            // ถ้าชนกับ Cooking Tool ให้ย้ายวัตถุไปยังตำแหน่งตรงกลางจาน
            transform.position = other.transform.position + new Vector3(0,0,transform.position.z);
            Debug.Log("Food placed on Cooking Tool via trigger collision.");
        }
    }

}
