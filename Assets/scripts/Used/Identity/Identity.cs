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
    [SerializeField] public string _itemname;
    [SerializeField] protected objectType _objectType;
    //public string GetObjectName() objectName=> ;
    public string GetObjectType() => _objectType.ToString();
    public virtual void ShowIdentity()
    {
       Debug.Log($"Object Name: {_itemname}, Object Type: {_objectType.ToString()}");
    }

    
}
