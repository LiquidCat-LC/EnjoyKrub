using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    //เหลือเปลี่ยน string[] เป็น Gameobject[]
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
            
                Debug.Log("Order : "+a);
                Debug.Log(ordersMenu[a].mainDish);
                Debug.Log(ordersMenu[a].sideDish);
                Debug.Log(ordersMenu[a].curry);
                
        }
        CallOrder(ordersMenu,1);
        RemoveOrder(ordersMenu, 2);
        CallOrder(ordersMenu,1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallOrder(Orders[] order,int num)
    {
        Debug.Log("Order #"+num);
        Debug.Log(order[num].mainDish);
        Debug.Log(order[num].sideDish);
        Debug.Log(order[num].curry);
        //gameObject.SetActive(true);
        //Debug.Log(gameObject.name);
    }

    public Orders[] RemoveOrder(Orders[] order, int num)
    {
        var orderList = order.ToList();
        orderList.RemoveAt(num);
        var ordersArray = orderList.ToArray();
        return ordersArray;
    }
}
    
