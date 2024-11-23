using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaker : MonoBehaviour
{
    public CustomerManager _customerManager;
    public Image barImage; // ลิงก์กับ Image ที่เป็นหลอด bar
    private float fillAmount = 0f;
    private bool isShaking = false;

    void Start()
    {
        fillAmount = 0f;
        _customerManager.isPause = true;
    }

    void Update()
    {
        PettingCustomer();
    }

    private void OnEnable()
    {
        // if (barImage != null)
        // {
        //     fillAmount = 0f;
        //     _customerManager.isPause = true;

        //     Debug.Log("Fill reset to 0.");
        // }
    }

    public void PettingCustomer()
    {

        // ตรวจสอบการเขย่า
        if (Input.acceleration.magnitude > 2.0f)
        {
            if (!isShaking)
            {
                isShaking = true; // เริ่มเขย่า
                StartCoroutine(Shake());
            }
        }
        else
        {
            isShaking = false; // หยุดเขย่า
        }

        // if(fillAmount > 1f)
        // {
        //     Debug.Log("fin1");
        //     _customerManager.hadPetting = true;
        //     _customerManager.isPause = false;
        //     this.gameObject.SetActive(false);
        // }
    }

    private IEnumerator Shake()
    {
        while (fillAmount < 1f && isShaking)
        {
            Debug.Log("Shake");
            fillAmount += Time.deltaTime * 2f; // ปรับความเร็วในการเติม
            barImage.fillAmount = fillAmount; // เติม bar
            yield return null;
        }

        if(fillAmount > 1f)
        {
            Debug.Log("fin");
            _customerManager.hadPetting = true;
            _customerManager.isPause = false;
            this.gameObject.SetActive(false);
        }
    }
}
