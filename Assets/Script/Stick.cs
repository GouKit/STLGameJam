using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stick : MonoBehaviour
{
    [SerializeField]
    private int maxFoodCount = 6;

    [SerializeField]
    float stickSize = 1f;

    new public string name;
    public string context;

    [SerializeField]
    Transform stickPoint;

    public List<Food> foods;
    public List<FoodEffect> effects;
    public CookRecipe recipe;

    public UnityAction finishEvent;

    void SetName()
    {
        StringBuilder resultName = new StringBuilder();
        List<int> filterFoods = new List<int>();
        for (int i = 0; i < foods.Count; ++i)
        {
            if (!filterFoods.Contains(foods[i].id))
            {
                filterFoods.Add(foods[i].id);

                resultName.Append(foods[i].name);
                resultName.Append(" ");
            }
        }
        resultName.Append(GetRecipeContext());
        resultName.Append(" 꼬치");

        name = resultName.ToString();
    }

    void AddFood(Food food)
    {
        foods.Add(new Food(food));

        if (foods.Count >= maxFoodCount)
        {
            //TODO :: 꼬치 완성 >> 조리 선택
            if (finishEvent != null)
            {
                finishEvent.Invoke();
            }

            SetName();

        }

    }

    string GetRecipeContext()
    {
        switch (recipe)
        {
            case CookRecipe.None:
                return "생";
            case CookRecipe.Steam:
                return "찐";
            case CookRecipe.Bake:
                return "구운";
        }
        return "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO :: 꼬치에 순차적으로 들어감
        if (collision.CompareTag("Food"))
        {
            FoodBehaviour food = collision.GetComponent<FoodBehaviour>();

            food.transform.SetParent(transform);

            Vector3 foodPosition = transform.position;

            foodPosition.x = ((float)(foods.Count + 1) / (float)maxFoodCount) * stickSize + stickPoint.position.x;

            food.MoveStick(foodPosition);

            AddFood(food.GetFood());
        }
    }

}

[System.Serializable]
public enum CookRecipe
{
    None
        , Steam
        , Bake
}