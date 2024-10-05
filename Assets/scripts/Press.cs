using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Press : MonoBehaviour
{
     [SerializeField] PlayerInput playerInput;
    private InputAction touchPressAction;
    private InputAction touchPositionAction;

    [SerializeField] private float touchDragPhysicsSpeed = 10;
    [SerializeField] private float touchDragSpeed = .1f;

    private InputActionMap touchMap;
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
        touchPressAction = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchMap = playerInput.actions.FindActionMap("Countdown");
    }

    private void OnEnable()
    {
        touchMap.Enable();
        touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        touchPressAction.performed -= TouchPressed;
        touchMap.Disable();
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector3 position = touchPositionAction.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Draggable"))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        while (touchPressAction.ReadValue<float>() != 0)
        {
            Vector3 position = touchPositionAction.ReadValue<Vector2>();
            Ray ray = mainCamera.ScreenPointToRay(position);
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * touchDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position,
                    ray.GetPoint(initialDistance), ref velocity, touchDragSpeed);
                yield return null;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
