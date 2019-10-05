
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Stick : MonoBehaviour
{
    FoodDB db;

    [SerializeField]
    private int maxFoodCount = 6;

    [SerializeField]
    float stickSize = 1f;

    bool isCooked = false;

    public new string name;
    public string context;

    [SerializeField]
    Transform stickPoint;

    public List<Food> foods;
    public List<FoodEffect> effects;
    public CookRecipe cookType;

    public UnityAction finishPushEvent;

    private void Awake()
    {
        db = FindObjectOfType<FoodDB>();
        
    }

    public void SetName()
    {
        StringBuilder resultName = new StringBuilder();

        List<int> foodIDs = new List<int>();
        Dictionary<int, int> foodCounts = new Dictionary<int, int>();

        for (int i = 0; i < foods.Count; ++i)
        {
            if (!foodCounts.ContainsKey(foods[i].id))
            {
                foodIDs.Add(foods[i].id);
                foodCounts.Add(foods[i].id, 1);
            }
            else
            {
                foodCounts[foods[i].id] += 1;
            }
        }

        for (int i = 0; i < foodCounts.Count; ++i)
        {
            resultName.Append(db.FindCountName(foodCounts[foodIDs[i]]));
            resultName.Append(foods.Find(item => item.id == foodIDs[i]).name);
        }


        if (isCooked)
        {
            resultName.Append(GetRecipeContext());
        }

        resultName.Append("꼬치");

        name = resultName.ToString();
    }

    public string GetName()
    {
        return name;
    }

    void AddFood(Food food)
    {
        foods.Add(new Food(food));

        if (foods.Count >= maxFoodCount)
        {
            SetName();

            //TODO :: 꼬치 완성 >> 조리 선택
            if (finishPushEvent != null)
            {
                finishPushEvent.Invoke();
            }

        }
    }

    public void SetCook(bool isCooked, CookRecipe recipe)
    {
        this.isCooked = isCooked;
        this.cookType = recipe;
    }

    string GetRecipeContext()
    {
        switch (cookType)
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

            food.GetComponent<Rigidbody2D>().simulated = false;

            food.transform.SetParent(transform);
            food.gameObject.layer = LayerMask.NameToLayer("Stick");

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