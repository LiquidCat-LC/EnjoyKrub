using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halloweenevent : MonoBehaviour
{
    public GameObject HalloweenMenuTheme;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HalloweenTheme(bool isactive)
    {
        HalloweenMenuTheme.SetActive(isactive);
    }
}
