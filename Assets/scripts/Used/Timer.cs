using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Image Fill;
    [SerializeField] float Max;
    [SerializeField] GameObject obj;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        time -= Time.deltaTime;
        //timerText.text ="" + (int)time;
        Fill.fillAmount = time / Max;

        if (time<0)
        {
            time = 10;
            gameObject.SetActive(false);
            //obj.GetComponent<Food>().isCooked = true;
            obj.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
