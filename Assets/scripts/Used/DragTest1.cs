using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragTest1 : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector2 _startTouchPosition;
    private Vector2 _startObjectPosition;
    [SerializeField] public bool _isDragging;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction TouchPerformedAction, TouchStartAction;
    [SerializeField] private GameObject _draggedObject;

    private void Awake()
    {
        //Debug.Log("Is awake");
        _camera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        TouchStartAction = playerInput.actions["TouchStart"];
        TouchPerformedAction = playerInput.actions["TouchPerformed"];
    }

    private void OnEnable()
    {
        //Debug.Log("Is OnEnable");
        TouchStartAction.performed += OnTouchStart; 
        TouchPerformedAction.performed += OnTouchPerformed; 
        TouchStartAction.canceled += OnTouchEnd;    
    }

    private void OnDisable()
    {
        //Debug.Log("Is OnDisable");
        TouchStartAction.performed -= OnTouchStart;
        TouchPerformedAction.performed -= OnTouchPerformed;
        TouchStartAction.canceled -= OnTouchEnd;

    }

    public void OnTouchStart(InputAction.CallbackContext context)
    {
            // Debug.Log("Is OnTouchStart");
            _draggedObject = null;
            Vector2 touch = TouchPerformedAction.ReadValue<Vector2>();
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(touch));

            // detect here
            if (rayHit.collider != null && rayHit.collider.CompareTag("Food"))
            {
                //Debug.Log("Is OnTouchStart rayhit ");
                _draggedObject = rayHit.collider.gameObject; // วัตถุที่จะถูกลาก
                _startTouchPosition = _camera.ScreenToWorldPoint(touch);
                _startObjectPosition = _draggedObject.transform.position; // เก็บตำแหน่งเริ่มต้นของวัตถุ
                _isDragging = true;
            }
    }

    public void OnTouchPerformed(InputAction.CallbackContext context)
    {
        //Debug.Log("Is OnTouchPerformed");
        if (_isDragging)
        {
            //Debug.Log("Is OnTouchPerformed rayhit ");
            Vector2 touch = TouchPerformedAction.ReadValue<Vector2>();
            Vector2 worldPosition = _camera.ScreenToWorldPoint(touch);
            Vector2 offset = worldPosition - _startTouchPosition;

            if (_draggedObject != null)
            {
                _draggedObject.transform.position = _startObjectPosition + offset;
            } 
             
        }
       
    }

    public void OnTouchEnd(InputAction.CallbackContext context)
    {
        //Debug.Log("Is OnTouchEnd");
         if (_isDragging)
        {
            _isDragging = false;
            _draggedObject = null;
        }
        
        
    }

    
}
