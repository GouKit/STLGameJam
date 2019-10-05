using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food : Item
{
    public List<FoodTaste> tastes;

    public Food(Food f)
    {
        this.id = f.id;
        this.name = f.name;
        this.context = f.context;
        this.spriteName = f.spriteName;

        this.tastes = f.tastes;        
    }

}


[System.Serializable]
public enum FoodTaste
{

    Sweetness
        , Salty
        , Spicy
        , Acidity
        , Bitter
        , Umami
}

[System.Serializable]
public enum FoodEffect
{
    Hot,
    Ice
}