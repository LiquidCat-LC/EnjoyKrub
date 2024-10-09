using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    //f1
    [SerializeField] Canvas timer;

    //f2
    [SerializeField] private GameObject[] tablePanels;
    private int page = 0;
    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
        
        
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed; 
    }

     private void TouchPressed(InputAction.CallbackContext context)
    {
        //check 
        float value = context.ReadValue<float>();
        Debug.Log(value);

        //check tag
        var rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>()));
        if(rayHit.collider != null)
        {
            Debug.Log(rayHit.collider.tag);
        }

        //function 1
        if(rayHit.collider != null && rayHit.collider.CompareTag("Cooking"))
        {
            GameObject RaycastReturn = rayHit.collider.gameObject;
            Debug.Log(rayHit.collider.tag);
            Timer(RaycastReturn);
        }

        //Function 2
        if(rayHit.collider != null && rayHit.collider.CompareTag("panel"))
        {
            Debug.Log(tablePanels.Length);
            Debug.Log("Change panel");
            changePanel();
        }

    }

    private void Timer(GameObject objToChange)
    {
        objToChange.GetComponent<SpriteRenderer>().color = Color.green;
        timer.gameObject.SetActive(true);
    }
    
    private void changePanel()
    {
        Debug.Log(page);

        if(page >= 2)
        {
            tablePanels[page].SetActive(false);
            page = 0;
            tablePanels[page].SetActive(true);
        } 
        else 
        {
            tablePanels[page].SetActive(false);
            page +=1;
            tablePanels[page].SetActive(true);
        }
        
        
    }
}
