using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{   [Header("Setting text")]
    public TMP_Text mainDishOrderText;
    public TMP_Text sideDishOrderText;
    public TMP_Text curryOrderText;

    [Header("All menu")]
    public GameObject[] mainDishes;
    public GameObject[] sideDishes;
    public GameObject[] curries;
    Random random = new Random();

    [Header("Overall")]
    public int heart;
    public int money;
    public GameObject plate;

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

        }
        CallOrder(ordersMenu[0]);
    }

    public void CallOrder(Orders order)
    {
        // Debug.Log("Order #" + num);
        // Debug.Log(order[num].mainDish);
        // Debug.Log(order[num].sideDish);
        // Debug.Log(order[num].curry);

        mainDishOrderText.text = $"Main Dish: {order.mainDish.GetComponent<Food>()._itemname}";
        sideDishOrderText.text = $"Side Dish: {order.sideDish.GetComponent<Food>()._itemname}";
        curryOrderText.text = $"Curry: {order.curry.GetComponent<Food>()._itemname}";

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

        if (!plateScript.CheckCookedStatus())
        {
            return false;
        }

        bool isMainDishMatch = plateScript.mainDishOnPlate.GetComponent<Food>()._itemname == order.mainDish.GetComponent<Food>()._itemname;
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
        if (CheckOrder(ordersMenu[orderIndex]) == true)
        {
            print("Correct");
        }
        else
        {
            print("Wrong");
        }
        GetMoney(orderIndex);
        RandomOrder();
    }

    public void Redo()
    {
        Plate plateScript = plate.GetComponent<Plate>();
        List<GameObject> dishes = plateScript.GetAllDishes();
        foreach (GameObject food in dishes)
        {
            Destroy(food);
        }
        plateScript.mainDishOnPlate = null;
        plateScript.sideDishOnPlate = null;
        plateScript.curryOnPlate = null;

        Debug.Log("All dishes on the plate have been reset.");
    }
}

