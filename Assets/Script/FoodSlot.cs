using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FoodSlot : MonoBehaviour
{
    FoodDB db;

    public int FoodID;

    [SerializeField]
    private Food food;

    [SerializeField]
    SpriteRenderer iconRenderer;

    private void Awake()
    {
        db = FindObjectOfType<FoodDB>();
    }

    public Food GetFood()
    {
        return this.food;
    }

    public void SetFood(int id)
    {
        FoodID = id;
        this.food = new Food(db.FindFoodWithID(id));

        SetSprite();

    }

    public void SetFood(Food food)
    {
        FoodID = food.id;
        this.food = new Food(food);

        SetSprite();
    }

    void SetSprite()
    {
        iconRenderer.sprite = food.GetSprite();
    }

}
