using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    GameManager gm;

    CookPointer cookPointer;

    [HideInInspector]
    public FoodSlotManager foodSlotManager;

    [SerializeField]
    Stick currentStick;

    Quest loadQuest;

    [SerializeField]
    float waitProgressTime = 3f;

    public Transform cookTypeObject;
    public CookRecipe cookType;

    void Awake()
    {
        cookPointer = FindObjectOfType<CookPointer>();
        loadQuest = FindObjectOfType<Quest>();
        foodSlotManager = FindObjectOfType<FoodSlotManager>();
    }

    public void StartGame()
    {
        currentStick.finishPushEvent += FinishStickPush;
        StartStickPush();
    }

    void StartStickPush()
    {
        cookPointer.SetInput(true);
    }

    void FinishStickPush()
    {
        cookPointer.SetGive(true);
    }

    public void FinalCook()
    {
        currentStick.SetCook(true, cookType);
        currentStick.SetName();

        StartCoroutine(WaitProcess());
    }

    public void GiveNpc()
    {
        List<Recipe> QuestRecipe = new List<Recipe>();
        Recipe r = null;
        for (int i = 0; i < currentStick.foods.Count; ++i)
        {
            if (i == currentStick.foods.Count - 1)
            {
                QuestRecipe.Add(new Recipe(0, 0, "", currentStick.cookType));
            }
            else
            {
                r = QuestRecipe.Find(item => item.id == currentStick.foods[i].id);

                if (r == null)
                {
                    QuestRecipe.Add(new Recipe(currentStick.foods[i].id, 1, currentStick.foods[i].name, CookRecipe.None));
                }
                else
                {
                    ++r.count;
                }
            }
        }

        int checkFoodPoint = 0;
        bool isCook = loadQuest.QuestRecipe[loadQuest.QuestRecipe.Count-1].CheckCorrect(QuestRecipe[QuestRecipe.Count-1].cookType);
        bool isNot = false;
        
        for (int n = 0; n < loadQuest.QuestRecipe.Count; ++n)
        {
            for (int k = 0; k < QuestRecipe.Count; ++k)
            {
                if(loadQuest.QuestRecipe[n].count == 0)//퀘스트 재료 개수가 0
                {
                    if(loadQuest.QuestRecipe[n].id == QuestRecipe[k].id)//같은 id인 재료가 있음(없어야함)
                        isNot = true;
                }
                else if(loadQuest.QuestRecipe[n].id == QuestRecipe[k].id && loadQuest.QuestRecipe[n].count <= QuestRecipe[k].count)
                {
                    //같은 id인 재료 그리고 개수도 같음
                    ++checkFoodPoint;
                }
            }
            if (loadQuest.QuestRecipe[n].count == 0)
            {
                if (!isNot) //퀘스트 재료가 없음
                {
                    ++checkFoodPoint;
                    print("hi");
                }
                isNot = false;
            }
        }

        Debug.Log("cnt: " + (loadQuest.QuestRecipe.Count - 1) + "/ food: " + checkFoodPoint);
        Debug.Log(isCook);

        if (checkFoodPoint == loadQuest.QuestRecipe.Count - 1)
        {
            //원하는 음식
            if (isCook)
            {
                //원하는 조리
                gm.AddScore(100);
            }
            else
            {
                gm.AddScore(70);
            }
        }
        else
        {
            //원하지 않는 음식
            if (isCook)
            {
                //원하는 조리
                gm.AddScore(40);
            }
            else
            {
                gm.AddScore(0);
            }
        }

        currentStick.ClearStick();
        foodSlotManager.SetSlot();
    }

    IEnumerator WaitProcess()
    {
        float timer = waitProgressTime;
        gm.StopTimer();
        gm.ui.cookWorker.SetActive(true);
        SoundManager.Instance.PlaySFX(SoundManager.SFX_SOUND.GRILL);
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }


        gm.ui.cookWorker.SetActive(false);
        gm.ui.cookResult.UpdateStickName(currentStick.GetName());
        gm.ui.cookResult.gameObject.SetActive(true);
    }

    public void ChangeCookType(int dir, int view)
    {
        cookTypeObject.position += dir * Vector3.left * 6f;
        switch (view)
        {
            case -1:
                cookType = CookRecipe.Steam;
                break;
            case 0:
                cookType = CookRecipe.None;
                break;
            case 1:
                cookType = CookRecipe.Bake;
                break;
        }
    }

    public void SetGameManger(GameManager gm)
    {
        this.gm = gm;
    }

}
