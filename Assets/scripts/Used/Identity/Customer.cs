using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    Idle,
    Order,
    Happy,
    Angry
}
public class Customer : Identity
{
    public Animator animator;
    public CustomerState currentState;
    public int waitingTime = 30;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetCustomerState(CustomerState.Idle);
    }
    public void SetCustomerState(CustomerState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case CustomerState.Idle:
                animator.SetTrigger("Idle");
                break;
            case CustomerState.Order:
                animator.SetTrigger("Order");
                break;
            case CustomerState.Happy:
                animator.SetTrigger("Happy");
                break;
            case CustomerState.Angry:
                animator.SetTrigger("Angry");
                break;
        }
    }


}
