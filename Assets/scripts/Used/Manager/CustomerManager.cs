using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class CustomerManager : MonoBehaviour
{
    Random random = new Random();

    [Header("Set Up")]
    public PlayerManager _playerManager;
    public GameManager _orderManager;
    public SceneRunning _Loading;
    public GameObject quickPet;
    public GameObject pettingBar;
    public Slider SatisfySlider;
    public Image handleImage;
    public List<Sprite> stateSatisfySprites;

    [Header("Setting customer")]
    public TMP_Text totalCustomerText;
    public bool isPause = false;
    public GameObject[] customerPrefab;
    public float customerMoveSpeed;
    public float customerSpawnSpeed;
    public List<Transform> queuePositions;
    [SerializeField] private Transform customerParent;

    [Header("Customer action")]
    public bool IsAllCustomerspawn;
    public int customerOfToday;
    public bool IsSomeoneOrder;
    public bool IsSomeoneLeaving;
    public bool isWaiting = false;
    public bool hadPetting = false;
    public List<GameObject> customerQueue;
    private float remainingTime;
    private float holdingTime;

    public void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        customerOfToday = 0;
        IsAllCustomerspawn = false;
        StartCoroutine(SpawnCustomerRoutine(_playerManager.customers));
        random = new System.Random();
    }

    IEnumerator SpawnCustomerRoutine(int _customers)
    {
        while (customerOfToday < _customers) 
        {
            if (customerQueue.Count < queuePositions.Count - 2)
            {
                SpawnCustomer();
                UpdateCustomerText(_customers);
            }
            yield return new WaitForSeconds(customerSpawnSpeed);
        }
        Debug.Log("All customers spawned for this level.");
    }

    private void UpdateCustomerText(int _customers)
    {
        totalCustomerText.text = $"Customers: {customerOfToday}/{_customers}";
        if (customerOfToday == _customers)
        {
            IsAllCustomerspawn = true;
        }
    }

    private void SpawnCustomer()
    {
        GameObject thisCustomer = customerPrefab[random.Next(customerPrefab.Length)];
        GameObject newCustomer = Instantiate(
            thisCustomer,
            queuePositions[0].position,
            Quaternion.identity,
            customerParent
        );
        customerQueue.Add(newCustomer);
        customerOfToday++;
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
            customerQueue[0].GetComponent<Customer>().SetCustomerState(CustomerState.Order);
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
        isPause = false;
        pettingBar.SetActive(false);
        customerQueue.RemoveAt(0);
        StopCoroutine(StartSatisfactionCountdown(customer));
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

        if (IsAllCustomerspawn && customerQueue.Count == 0)
        {
            Debug.Log("All customers have been served. Loading next scene...");
            _Loading.LoadSceneWithLoading("Endday");
        }
        else
        {
            MoveCustomersInQueue();
        }
    }

    IEnumerator StartSatisfactionCountdown(GameObject customer)
    {
        Customer thisCustomer = customer.GetComponent<Customer>();

        remainingTime = thisCustomer.patienceDuration;
        holdingTime = thisCustomer.patienceDuration * 0.3f;
        hadPetting = false;
        bool hadScreaming = false;
        bool hadExtendedTime = false;
        SatisfySlider.gameObject.SetActive(true);

        while (remainingTime > 0)
        {
            if (!isWaiting)
            {
                Debug.Log("Customer : Coroutine cancelled!");

                yield break;
            }

            if (isPause)
            {
                yield return null;
            }
            else
            {
                remainingTime -= Time.deltaTime;

                float progress = 1 - (remainingTime / thisCustomer.patienceDuration);
                SatisfySlider.value = progress;

                if (progress < 0.3f)
                {
                    handleImage.sprite = stateSatisfySprites[0];
                }
                else if (progress < 0.6f)
                {
                    handleImage.sprite = stateSatisfySprites[1];
                }
                else
                {
                    handleImage.sprite = stateSatisfySprites[2];
                }
            }

            if (remainingTime < holdingTime && !hadScreaming)
            {
                Debug.Log("Customer : screaming");
                thisCustomer.SetCustomerState(CustomerState.Angry);
                hadScreaming = true;
                quickPet.SetActive(true);
                StartCoroutine(QuickTimePettingCountdown());
            }

            if (hadScreaming && !hadExtendedTime && hadPetting)
            {
                thisCustomer.SetCustomerState(CustomerState.Idle);
                Debug.Log("Time extended by 10 seconds!");
                remainingTime += 10f;
                hadExtendedTime = true;
            }
            yield return null;
        }
        thisCustomer.SetCustomerState(CustomerState.Angry);
        Debug.Log($"Customer is unhappy and leaving due to long wait time.");
        _orderManager.Serving();
    }

    IEnumerator QuickTimePettingCountdown()
    {
        float remainingPetTime = 5f;
        while (remainingPetTime > 0 && !hadPetting)
        {
            if (IsSomeoneLeaving)
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

    // private void SetHadPettingTrue()
    // {
    //     hadPetting = true;
    // }
    public void OpenPettingUI()
    {
        pettingBar.SetActive(true);
        quickPet.SetActive(false);
    }
}
