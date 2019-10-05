using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public int id;
    public int count;
    public string foodName;
    public Recipe(int id, int count, string name)
    {
        this.id = id;
        this.count = count;
        foodName = name;
    }
}

public class Quest : MonoBehaviour
{
    [SerializeField]
    private List<Recipe> QuestRecipe = new List<Recipe>();

    public List<string> Question;

    private FoodDB db;

    [Header("추가조건 확률 (%)")]
    public float[] menuCountPercent;

    const int maxUseCount = 6;
    int currentUseCount = 0;

    List<int> randFoodIDList = new List<int>();

    void Awake()
    {
        db = FindObjectOfType<FoodDB>();
        SetQuestRecipe();
    }

    void InitRandFoodIDList()
    {
        int count = db.GetFoodDBCount();
        for (int i = 0; i < count; ++i)
        {
            randFoodIDList.Add(i + 1);
        }
    }

    public void SetQuestRecipe()
    {
        InitRandFoodIDList();

        currentUseCount = maxUseCount;
        float randPercent = Random.Range(0f, 1f) * 100f;
        int menuCount = 1;

        if (randPercent > menuCountPercent[0] + (100f - (menuCountPercent[1] + menuCountPercent[0])))
        {
            //3개
            menuCount = 3;
        }
        else if (randPercent > 100f - (menuCountPercent[1] + menuCountPercent[0]))
        {
            //2개
            menuCount = 2;
        }

        for (int i = 0; i < menuCount; ++i)
        {
            AddQuestRecipe();
        }

    }

    public void AddQuestRecipe() //퀘스트 설정
    {
        int randFoodIndex = Random.Range(0, randFoodIDList.Count);
        int randFoodID = randFoodIDList[randFoodIndex];

        randFoodIDList.RemoveAt(randFoodIndex);

        int randUseCount = Random.Range(0, currentUseCount + 1);

        currentUseCount -= randUseCount;

        QuestRecipe.Add(new Recipe(randFoodID, randUseCount, db.FindFoodWithID(randFoodID).name));

    }


    string ConvertKoreanFormat(string foodName)
    {
        switch (foodName)
        {
            case "레고캐빈":
            case "버섯":
            case "귤":
            case "날치알":
            case "얼음":
            case "태양":
                return foodName + "을 " + Question[Random.Range(0, Question.Count)];
            default:
                return foodName + "를 " + Question[Random.Range(0, Question.Count)];
        }
    }

    public string ReturnQuest()
    {
        string text = "";
        int count = QuestRecipe.Count;
        for (int i = 0; i < count; ++i)
        {
            text += ConvertKoreanFormat(QuestRecipe[i].foodName);

            if (i < count - 1)
                text += "\n";
        }
        return text;
    }

    public string Tasting(bool isGood)
    {
        if(isGood)
            return "이집 맛있군, 훌륭해! 내가 원하던 맛이야!";
        else
            return "이게뭐야! 실망이군, 이런걸 먹으라고 주다니";
    }

}
