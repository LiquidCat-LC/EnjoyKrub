using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public static bool isCooked = false;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooked == true)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
