using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDish : Food
{
    void Start()
    {
        _objectType = objectType.Food;
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

    // public override void OnTriggerEnter2D(Collider2D other)
    // {
    //     // เรียกฟังก์ชัน OnTriggerEnter2D ของคลาสพ่อ
    //     base.OnTriggerEnter2D(other);

    //     // เพิ่มพฤติกรรมเพิ่มเติมสำหรับคลาสลูก
    //     if (other.CompareTag("CookingTools"))
    //     {
    //         Debug.Log("Derived: SideDish collided with Cooking Tool");
    //     }
    // }
    
}
