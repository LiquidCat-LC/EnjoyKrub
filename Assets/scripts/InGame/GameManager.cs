using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public int money;
    public GameObject plate;

    [Header("What can Order")]
    public GameObject[] mainDishes;
    public GameObject[] sideDishes;
    public GameObject[] curries;
    Random random = new Random();

    public static int orD = 1;
    public Orders[] ordersMenu = new Orders[orD];

    [System.Serializable]
    public class Orders
    {
        public GameObject mainDish;
        public GameObject sideDish;
        public GameObject curry;
    }

    void Start()
    {
        RandomOrder();
        //CallOrder(ordersMenu,1);
        //RemoveOrder(ordersMenu, 2);
        //CallOrder(ordersMenu,1);
    }

    [ContextMenu("RandomOrder")]
    public void RandomOrder()
    {
        for (int a = 0; a < orD; a++)
        {
            ordersMenu[a] = new Orders()
            {
                mainDish = mainDishes[random.Next(mainDishes.Length)],
                sideDish = sideDishes[random.Next(sideDishes.Length)],
                curry = curries[random.Next(curries.Length)]
            };

            // Debug.Log("Order : " + a);
            // Debug.Log(ordersMenu[a].mainDish);
            // Debug.Log(ordersMenu[a].sideDish);
            // Debug.Log(ordersMenu[a].curry);
        }
    }

    public void CallOrder(Orders[] order, int num)
    {
        Debug.Log("Order #" + num);
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

    public bool CheckOrder(Orders order)
    {
        Plate plateScript = plate.GetComponent<Plate>();

        bool isMainDishMatch = plateScript.mainDishOnPlate.GetComponent<Food>()._itemname  == order.mainDish.GetComponent<Food>()._itemname;
        bool isSideDishMatch = plateScript.sideDishOnPlate.GetComponent<Food>()._itemname == order.sideDish.GetComponent<Food>()._itemname;
        bool isCurryMatch = plateScript.curryOnPlate.GetComponent<Food>()._itemname == order.curry.GetComponent<Food>()._itemname;

        return isMainDishMatch && isSideDishMatch && isCurryMatch;
    }
    public int GetMoney(int orDNum)
    {
        if (CheckOrder(ordersMenu[orDNum]) == true)
        {
            return money += random.Next(30, 80); ;
        }
        return money += 0;
    }
    public void Serving()
    {
        int orderIndex = 0;
        CheckOrder(ordersMenu[orderIndex]);
        if(CheckOrder(ordersMenu[orderIndex]) == true)
        {
            print("Correct");
        }
        else{
            print("Wrong");
        }
        GetMoney(orderIndex);
        RandomOrder();
    }
}

