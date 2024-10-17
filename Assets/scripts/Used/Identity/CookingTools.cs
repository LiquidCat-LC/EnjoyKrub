using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingTools : Identity
{
    [SerializeField] public GameObject dragSystem;
    void Awake()
    {
        _objectType = objectType.CookingTool;
    }


    public virtual void OnTriggerStay2D(Collider2D other)
    {   
        bool drag = dragSystem.GetComponent<DragTest1>()._isDragging;
        if (transform.childCount == 0 && drag == false)
        {
            if (other.CompareTag("Food"))
            {
                other.transform.SetParent(transform);
                other.transform.position = transform.position + new Vector3(0,0,transform.position.z);
                Debug.Log("Obj1 is now a child of Obj2.");
            }
        }
        else
        {
            Debug.Log("Obj2 already has a child.");
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
            {
                other.transform.SetParent(null);

                Debug.Log("Exit");
            }
    }


}
