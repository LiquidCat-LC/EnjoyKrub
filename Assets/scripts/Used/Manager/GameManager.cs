using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [Header("Set Up")]
    public CustomerManager _customerManager;
    public PlayerManager _player;
    public GameObject plate;
    public GameObject OrderNote;

    [Header("Setting text")]
    public TMP_Text mainDishOrderText;
    public TMP_Text sideDishOrderText;
    public TMP_Text curryOrderText;

    [Header("All menu")]
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
        _player = FindObjectOfType<PlayerManager>();
        _player.TotalCostumer_Success = 0;
        _player.TotalCostumer_Fail = 0;
        //RandomOrder();
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
                curry = curries[random.Next(curries.Length)],
            };
        }
        CallOrder(ordersMenu[0]);
    }

    public void CallOrder(Orders order)
    {
        mainDishOrderText.text = $"Main Dish: {order.mainDish.GetComponent<Food>()._itemname}";
        sideDishOrderText.text = $"Side Dish: {order.sideDish.GetComponent<Food>()._itemname}";
        curryOrderText.text = $"Curry: {order.curry.GetComponent<Food>()._itemname}";
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

        bool isMainDishMatch =
            plateScript.mainDishOnPlate.GetComponent<Food>()._itemname
            == order.mainDish.GetComponent<Food>()._itemname;
        bool isSideDishMatch =
            plateScript.sideDishOnPlate.GetComponent<Food>()._itemname
            == order.sideDish.GetComponent<Food>()._itemname;
        bool isCurryMatch =
            plateScript.curryOnPlate.GetComponent<Food>()._itemname
            == order.curry.GetComponent<Food>()._itemname;

        return isMainDishMatch && isSideDishMatch && isCurryMatch;
    }

    public int GetMoney(int orDNum)
    {
        if (CheckOrder(ordersMenu[orDNum]) == true)
        {
            return _player.moneyCollect += random.Next(30, 80);
            ;
        }
        return _player.moneyCollect += 0;
    }

    public void Serving()
    {
        _customerManager.isWaiting = false;
        _customerManager.IsSomeoneOrder = false;
        _customerManager.SatisfySlider.gameObject.SetActive(false);
        OrderNote.SetActive(_customerManager.IsSomeoneOrder);
        int orderIndex = 0;
        CheckOrder(ordersMenu[orderIndex]);
        if (CheckOrder(ordersMenu[orderIndex]) == true)
        {
            _customerManager
                .customerQueue[0]
                .GetComponent<Customer>()
                .SetCustomerState(CustomerState.Happy);
            _player.TotalCostumer_Success++;
            print(_player.TotalCostumer_Success++);
        }
        else
        {
            _customerManager
                .customerQueue[0]
                .GetComponent<Customer>()
                .SetCustomerState(CustomerState.Angry);
            _player.TotalCostumer_Fail++;
            print(_player.TotalCostumer_Fail);
        }
        GetMoney(orderIndex);
        ResetPlate();
        RemoveOrder(ordersMenu, orderIndex);
        _customerManager.RemoveCustomer();
    }

    public void ResetPlate()
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
