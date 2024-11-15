using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("Set Up")]
    public GameManager _orderManager;
    public GameObject quickPet;

    [Header("Setting customer")]
    public GameObject customerPrefab;
    public float customerMoveSpeed;
    public float customerSpawnSpeed;
    public bool IsSomeoneOrder;
    public bool IsSomeoneLeaving;
    public bool isWaiting = false;
    public bool hadPetting = false;
    public List<Transform> queuePositions;
    public List<GameObject> customerQueue;

    [SerializeField]
    private float remainingTime;

    [SerializeField]
    private float holdingTime;

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
        GameObject newCustomer = Instantiate(
            customerPrefab,
            queuePositions[0].position,
            Quaternion.identity
        );
        customerQueue.Add(newCustomer);
        MoveCustomersInQueue();
    }

    IEnumerator MoveToPosition(GameObject customer, Vector3 targetPosition)
    {
        while (Vector3.Distance(customer.transform.position, targetPosition) > 0.1f)
        {
            customer.transform.position = Vector3.MoveTowards(
                customer.transform.position,
                targetPosition,
                customerMoveSpeed * Time.deltaTime
            );
            yield return null;
        }

        if (customer == customerQueue[0] && !IsSomeoneOrder)
        {
            print("Q1 at pos already");
            _orderManager.RandomOrder();
            IsSomeoneOrder = true;
            _orderManager.OrderNote.SetActive(IsSomeoneOrder);
            isWaiting = true;
            StartCoroutine(StartSatisfactionCountdown(customer));
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
            customer.transform.position = Vector3.MoveTowards(
                customer.transform.position,
                targetPosition,
                customerMoveSpeed * Time.deltaTime
            );
            yield return null;
        }
        Destroy(customer);
        IsSomeoneLeaving = false;
        MoveCustomersInQueue();
    }

    IEnumerator StartSatisfactionCountdown(GameObject customer)
    {
        Customer thisCustomer = customer.GetComponent<Customer>();

        remainingTime = thisCustomer.patienceDuration;
        holdingTime = thisCustomer.patienceDuration * 0.3f;
        hadPetting = false;
        bool hadScreaming = false;
        bool hadExtendedTime = false;

        while (remainingTime > 0)
        {
            if (!isWaiting)
            {
                Debug.Log("Customer : Coroutine cancelled!");
                yield break;
            }

            remainingTime -= Time.deltaTime;

            if (remainingTime < holdingTime && !hadScreaming)
            {
                Debug.Log("Customer : screaming");
                hadScreaming = true;
                quickPet.SetActive(true);
                StartCoroutine(QuickTimePettingCountdown());
            }

            if (hadScreaming && !hadExtendedTime && hadPetting)
            {
                Debug.Log("Time extended by 10 seconds!");
                remainingTime += 10f;
                hadExtendedTime = true;
            }
            yield return null;
        }

        Debug.Log($"Customer is unhappy and leaving due to long wait time.");
        _orderManager.Serving();
    }

    IEnumerator QuickTimePettingCountdown()
    {
        float remainingPetTime = 5f;
        while (remainingPetTime > 0 && !hadPetting)
        {
            if(IsSomeoneLeaving)
            {
                quickPet.SetActive(false);
                yield break;
            }
            remainingPetTime -= Time.deltaTime;
            yield return null;
        }

        if (!hadPetting)
        {
            Debug.Log("Quick petting time ended.");
            quickPet.SetActive(false);
        }
        else
        {
            Debug.Log("Quick petting succeeded!");
            quickPet.SetActive(false);
        }
    }

    public void SetHadPettingTrue()
    {
        hadPetting = true;
    }
}
