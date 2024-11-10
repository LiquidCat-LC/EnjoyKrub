using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Set Up")]
    public GameManager _orderManager;
    [Header("Setting customer")]
    public GameObject customerPrefab;
    public float customerMoveSpeed;
    public float customerSpawnSpeed;
    public bool IsSomeoneOrder;
    public bool IsSomeoneLeaving;
    public List<Transform> queuePositions;
    public List<GameObject> customerQueue;

    public void Start()
    {
        StartCoroutine(SpawnCustomerRoutine());
    }
    IEnumerator SpawnCustomerRoutine()
    {
        while (true)
        {
            if (customerQueue.Count < queuePositions.Count - 2)
            {
                SpawnCustomer();
            }
            yield return new WaitForSeconds(customerSpawnSpeed);
        }
    }

    private void SpawnCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab, queuePositions[0].position, Quaternion.identity);
        customerQueue.Add(newCustomer);
        MoveCustomersInQueue();
    }

    IEnumerator MoveToPosition(GameObject customer, Vector3 targetPosition)
    {
        while (Vector3.Distance(customer.transform.position, targetPosition) > 0.1f)
        {
            customer.transform.position = Vector3.MoveTowards(customer.transform.position, targetPosition, customerMoveSpeed * Time.deltaTime);
            yield return null;
        }

        if (customer == customerQueue[0] && !IsSomeoneOrder)
        {
            print("Q1 at pos already");
            _orderManager.RandomOrder();
            IsSomeoneOrder = true;
            _orderManager.OrderNote.SetActive(IsSomeoneOrder);
        }
    }

    private void MoveCustomersInQueue()
    {
        for (int i = 0; i < customerQueue.Count; i++)
        {
            Transform targetPosition = queuePositions[Mathf.Min(i + 1, queuePositions.Count - 1)];
            StartCoroutine(MoveToPosition(customerQueue[i], targetPosition.position));
        }
    }
    public void RemoveCustomer()
    {
        IsSomeoneLeaving = true;
        StartCoroutine(CustomerLeave(customerQueue[0], queuePositions[4].position));
    }

    IEnumerator CustomerLeave(GameObject customer, Vector3 targetPosition)
    {
        customerQueue.RemoveAt(0);
        while (Vector3.Distance(customer.transform.position, targetPosition) > 0.1f)
        {
            customer.transform.position = Vector3.MoveTowards(customer.transform.position, targetPosition, customerMoveSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(customer);
        IsSomeoneLeaving = false;
        MoveCustomersInQueue();
    }

}
