using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum CustomerState
{
    Idle,
    Order,
    Happy,
    Angry,
}

public class Customer : Identity
{
    public CustomerState currentState;
    public int patienceDuration = 30;

    [Header("State GameObjects")]
    public GameObject Idle;
    public GameObject Order;
    public GameObject Angry;
    public GameObject Happy;

    private void Start()
    {
        SetCustomerState(CustomerState.Idle);
    }

    public void SetCustomerState(CustomerState newState)
    {
        currentState = newState;
        DisableAllObjects();

        switch (newState)
        {
            case CustomerState.Idle:
                Idle.SetActive(true);
                break;
            case CustomerState.Order:
                Order.SetActive(true);
                break;
            case CustomerState.Happy:
                Happy.SetActive(true);
                break;
            case CustomerState.Angry:
                Angry.SetActive(true);
                break;
        }
    }

    public void _Idle()
    {
        SetCustomerState(CustomerState.Idle);
    }
    public void _Angry()
    {
        SetCustomerState(CustomerState.Angry);
    }public void _Order()
    {
        SetCustomerState(CustomerState.Order);
    }public void _Happy()
    {
        SetCustomerState(CustomerState.Happy);
    }

    private void DisableAllObjects()
    {
        Idle.SetActive(false);
        Order.SetActive(false);
        Angry.SetActive(false);
        Happy.SetActive(false);
    }
}
