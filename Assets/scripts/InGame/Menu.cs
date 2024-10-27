using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Menu : MonoBehaviour
{
    public static int orD = 3;
    public Orders[] ordersMenu = new Orders[orD];
    public string[] mainDishes = new string[] {"Rice","Fried Rice","Rice Berry" };
    public string[] sideDishes = new string[] {"French", "Fried Chicken","Grilled Shrimp" };
    public string[] curries = new string[] {"Green Curry", "Phanaeng", "Southern Curry" };
    Random random = new Random();
    
    public class Orders
    {
        public string mainDish;
        public string sideDish;
        public string curry;
    }
  
    
    // Start is called before the first frame update
    void Start()
    {
        for (int a = 0; a < orD; a++)
        {
            ordersMenu[a] = new Orders(){mainDish = mainDishes[random.Next(mainDishes.Length-1)], 
                sideDish = sideDishes[random.Next(sideDishes.Length-1)], curry = curries[random.Next(curries.Length-1)]};
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallOrders(Orders[] orders)
    {
        
    }
    
}
