using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryObject
{
    public int id;
    public enum Name { Paper }
    public Name name;
    public int count;
}

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryObject> inventory = new List<InventoryObject>();

    public InventoryObject GetObject(InventoryObject.Name name)
    {
        InventoryObject obj = inventory.Find(item => item.name == name);

        if (obj != null) return obj;
        else return null;
    }

    //public int GetCountOfObjectByName(InventoryObject.Name name)
    //{
    //    InventoryObject obj = GetObject(name);

    //    return obj != null ? obj.count : 0;
    //}

    public void AddObject(InventoryObject.Name name, int number)
    {
        InventoryObject obj = GetObject(name);

        if (obj != null) obj.count += number;
        else inventory.Add(new InventoryObject { id = inventory.Count, name = name, count = number });
    }

    //public void RemoveObject(InventoryObject.Name name, int number)
    //{
    //    InventoryObject obj = GetObject(name);

    //    if (obj != null)
    //    {
    //        if (obj.count > 0 && obj.count - number > 0) obj.count -= number;
    //        else inventory.Remove(obj);
    //    }
    //}
}
