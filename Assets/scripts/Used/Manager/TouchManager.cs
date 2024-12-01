using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    //f2
    [SerializeField]
    private GameObject[] tablePanels;
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
        var rayHit = Physics2D.GetRayIntersection(
            Camera.main.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>())
        );
        if (rayHit.collider != null)
        {
            Debug.Log(rayHit.collider.tag);
        }
    }

    #region Change Panel
    public void changePanel()
    {
        Debug.Log("Page:" + page);
        StartCoroutine(DeactivateAndSwitchPanel(tablePanels[page]));
    }

    private IEnumerator DeactivateAndSwitchPanel(GameObject currentPanel)
    {
        SetReadyToSwitch(currentPanel, true);

        yield return new WaitForEndOfFrame();

        currentPanel.SetActive(false);

        if (page >= tablePanels.Length - 1)
        {
            page = 0;
        }
        else
        {
            page += 1;
        }

        tablePanels[page].SetActive(true);
        SetReadyToSwitch(tablePanels[page], false);
        ActivateChildren(tablePanels[page]);
    }

    private void SetReadyToSwitch(GameObject parent, bool state)
    {
        if (parent != null)
        {
            foreach (Transform child in parent.transform)
            {
                if (child.gameObject.TryGetComponent<Tools>(out Tools toolsComponent))
                {
                    toolsComponent.readyToSwitch = state;
                    Debug.Log(
                        $"{(state ? "Setting" : "Resetting")} readyToSwitch for {child.name} to {state}"
                    );
                }

                SetReadyToSwitch(child.gameObject, state);
            }
        }
    }

    private void ActivateChildren(GameObject parent)
    {
        if (parent != null)
        {
            foreach (Transform child in parent.transform)
            {
                if (!child.CompareTag("FakeUI"))
                {
                    child.gameObject.SetActive(true);

                    ActivateChildren(child.gameObject);
                }
            }
        }
    }
    #endregion
}
