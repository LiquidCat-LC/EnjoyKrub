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
    //Tools tool;


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
        if (rayHit.collider != null)
        {
            Debug.Log(rayHit.collider.tag);
        }

        //function 1
        if (rayHit.collider != null && rayHit.collider.CompareTag("Cooking"))
        {
            GameObject RaycastReturn = rayHit.collider.gameObject;
            Debug.Log(rayHit.collider.tag);
            Timer(RaycastReturn);
        }

        //Function 2
        if (rayHit.collider != null && rayHit.collider.CompareTag("panel"))
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

    #region Change Panel
    private void changePanel()
    {
        Debug.Log("Page:" + page);
        StartCoroutine(DeactivateAndSwitchPanel(tablePanels[page]));
    }
    IEnumerator DeactivateAndSwitchPanel(GameObject currentPanel)
    {
        DeactivateChildren(currentPanel);

        yield return new WaitForEndOfFrame();

        if (page >= 2)
        {
            currentPanel.SetActive(false);
            page = 0;
            tablePanels[page].SetActive(true);
        }
        else
        {
            currentPanel.SetActive(false);
            page += 1;
            tablePanels[page].SetActive(true);
        }

        ActivateChildren(currentPanel);
    }

    void DeactivateChildren(GameObject parent)
    {
        if (parent != null)
        {
            foreach (Transform child in parent.transform)
            {
                if (child.gameObject.GetComponent<Tools>() != null)
                {
                    child.gameObject.GetComponent<Tools>().readyToSwitch = true;
                }

                child.gameObject.SetActive(false);
                DeactivateChildren(child.gameObject);
            }
        }
    }

    void ActivateChildren(GameObject parent)
    {
        if (parent != null)
        {
            foreach (Transform child in parent.transform)
            {
                if (child.gameObject.GetComponent<Tools>() != null)
                {
                    child.gameObject.GetComponent<Tools>().readyToSwitch = false;
                }

                child.gameObject.SetActive(true);
                ActivateChildren(child.gameObject);
            }
        }
    }
    #endregion
}
