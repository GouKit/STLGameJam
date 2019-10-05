using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public int id;
    public int count;
    public string foodName;
    public CookRecipe cookType;

    public Recipe(int id, int count, string name, CookRecipe cookType)
    {
        this.id = id;
        this.count = count;
        foodName = name;
        this.cookType = cookType;
    }

    public bool CheckCorrect(CookRecipe cookType)
    {
        return this.cookType == cookType? true : false;
    }
}

public class Quest : MonoBehaviour
{
    public enum TASTING
    {
        NONE,
        BAD,
        SOSO,
        GOOD,
        GREAT        
    }
    public List<Recipe> QuestRecipe = new List<Recipe>();

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
    }

    void InitRandFoodIDList()
    {
        randFoodIDList.Clear();
        int count = db.GetFoodDBCount();
        for (int i = 0; i < count; ++i)
        {
            randFoodIDList.Add(i + 1);
        }
    }

    public void SetQuestRecipe()
    {
        InitRandFoodIDList();

        QuestRecipe.Clear();
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


        QuestRecipe.Add(new Recipe(0, 0, "", (CookRecipe)Random.Range(0, 3)));
    }

    public void AddQuestRecipe() //퀘스트 설정
    {
        int randFoodIndex = Random.Range(0, randFoodIDList.Count);
        int randFoodID = randFoodIDList[randFoodIndex];

        randFoodIDList.RemoveAt(randFoodIndex);

        int randUseCount = Random.Range(0, currentUseCount + 1);

        currentUseCount -= randUseCount;

        QuestRecipe.Add(new Recipe(randFoodID, randUseCount, db.FindFoodWithID(randFoodID).name, 0));

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

    string ConvertKoreanFormat(CookRecipe cookType)
    {
        switch (cookType)
        {
            case CookRecipe.None:
                return "생 꼬치로 주세요.";
            case CookRecipe.Steam:
                return "찐 꼬치로 주세요.";
            case CookRecipe.Bake:
                return "구운 꼬치로 주세요.";
            default:
                return "생 꼬치로 주세요.";
        }
    }

    public string ReturnQuest()
    {
        string text = "";
        int count = QuestRecipe.Count;
        for (int i = 0; i < count; ++i)
        {
            if (i == count - 1)
            {
                text += ConvertKoreanFormat(QuestRecipe[i].cookType);
            }
            else
                text += ConvertKoreanFormat(QuestRecipe[i].foodName);

            if (i < count - 1)
                text += "\n";
        }
        return text;
    }

    public string Tasting(TASTING isGood)
    {
        switch (isGood)
        {
        case TASTING.BAD:
            return "이게뭐야! 실망이네요, 이런걸 먹으라고 주다니";
        case TASTING.SOSO:
            return "먹을만 하네요. 좀 더 노력해보세요.";
        case TASTING.GOOD:
            return "좋네요, 하나 더 주겠어요?";
        case TASTING.GREAT:
            return "이집 맛있네요, 좋아요! 내가 원하던 맛이에요!";
        default:
            return "엉망이네요! 이런 사람도 가게를 하다니!";
        }
    }

}
