using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum objectType
{
    Customer,
    Food,
    CookingTool
}
public class Identity : MonoBehaviour
{
    [SerializeField] protected string objectName;
    [SerializeField] protected string objectType;

    public virtual void ShowIdentity()
    {
        Debug.Log($"Object Name: {objectName}, Object Type: {objectType}");
    }

    public string GetObjectName() => objectName;
    public string GetObjectType() => objectType;
}
