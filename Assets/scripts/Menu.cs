using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Menu : MonoBehaviour
{
    public int h = 1;
    public List<string> mainDishes = new List<string> {"Rice","Fried Rice","Rice Berry" };
    public List<string> sideDishes = new List<string> {"French", "Fried Chicken","Grilled Shrimp" };
    public List<string> curries = new List<string> {"Green Curry", "Phanaeng", "Southern Curry" };
    Random random = new Random();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int a = 0; a < a; a++)
        {
            random.Next(mainDishes.Count-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
