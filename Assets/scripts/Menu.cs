using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Menu : MonoBehaviour
{
    public int orders = 5;
    public string[] mainDishes = new string[] {"Rice","Fried Rice","Rice Berry" };
    public string[] sideDishes = new string[] {"French", "Fried Chicken","Grilled Shrimp" };
    public string[] curries = new string[] {"Green Curry", "Phanaeng", "Southern Curry" };
    Random random = new Random();
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int a = 0; a < orders; a++)
        {
            int r1 = random.Next(mainDishes.Length-1);
            int r2 = random.Next(sideDishes.Length-1);
            int r3 = random.Next(curries.Length-1);
            Debug.Log(mainDishes[r1]);
            Debug.Log(sideDishes[r2]);
            Debug.Log(curries[r3]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
