using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDB : MonoBehaviour
{
    [SerializeField]
    private List<Food> db;

    [SerializeField]
    private List<string> countNaming;

    public Food FindFoodWithID(int id)
    {
        return db.Find(item => item.id == id);
    }

    public Food FindFoodWithName(string name)
    {
        return db.Find(item => item.name == name);
    }

    public string FindCountName(int count)
    {
        if (count < 0 && countNaming.Count < count - 1)
            return countNaming[0];

        return countNaming[count - 1];
    }

    public int GetFoodDBCount() {
        return db.Count;
    }

   
}
