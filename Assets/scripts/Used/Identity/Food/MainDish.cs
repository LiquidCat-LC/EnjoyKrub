using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDish : Food
{
    void Start()
    {
        _objectType = objectType.Food;
        _foodCategory = foodCategory.MainDish;
        isCooked = true; 
    }


    // public override void OnTriggerEnter2D(Collider2D other)
    // {
    //     // เรียกฟังก์ชัน OnTriggerEnter2D ของคลาสพ่อ
    //     base.OnTriggerEnter2D(other);

    //     // เพิ่มพฤติกรรมเพิ่มเติมสำหรับคลาสลูก
    //     if (other.CompareTag("CookingTools"))
    //     {
    //         Debug.Log("Derived: MainDish collided with Cooking Tool");
    //     }
    // }

    
}
