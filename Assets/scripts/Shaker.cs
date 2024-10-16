using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaker : MonoBehaviour
{
    public Image barImage; // ลิงก์กับ Image ที่เป็นหลอด bar
    private float fillAmount = 0f;
    private bool isShaking = false;

    void Start()
    {
fillAmount = 0f;
    }

    void Update()
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
    }

    private System.Collections.IEnumerator Shake()
    {
        while (fillAmount < 1f && isShaking)
        {
            fillAmount += Time.deltaTime * 1f; // ปรับความเร็วในการเติม
            barImage.fillAmount = fillAmount; // เติม bar
            yield return null;
        }
    }
}
