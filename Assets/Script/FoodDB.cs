using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDB : MonoBehaviour
{
    [SerializeField]
    private List<Food> db;
    
    public Food FindFoodWithID(int id)
    {
        return db.Find(item => item.id == id);
    }

    public Food FindFoodWithName(string name)
    {
        return db.Find(item => item.name == name);
    }



}
