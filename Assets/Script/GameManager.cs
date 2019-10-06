using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public UIInGameManager ui;

    [SerializeField]
    float maxLifeTime;
    public float lifeTime;

    CookingManager cookingManager;
    LoadQuest loadQuest;
    NPCBehaviour npcBehaviour;

    public int score;
    bool isClear = false;

    [SerializeField]
    private int maxLife = 2;
    [SerializeField]
    private int life = 0;

    Coroutine timerCoroutine;

    protected override void Awake()
    {
        base.Awake();

        cookingManager = FindObjectOfType<CookingManager>();
        loadQuest = FindObjectOfType<LoadQuest>();
        ui = FindObjectOfType<UIInGameManager>();
        npcBehaviour = FindObjectOfType<NPCBehaviour>();

        cookingManager.SetGameManger(this);
        loadQuest.talkEnd += StartCook;
        loadQuest.nextNpc += NextNPC;

        life = maxLife;
    }

    private void Start()
    {
        loadQuest.CreateQuest();
        SubScore(0);
    }

    private void StartCook()
    {
        cookingManager.StartGame();
        StartTimer();
    }

    IEnumerator UpdateTime()
    {
        lifeTime = maxLifeTime;
        while (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            ui.timer.UpdateTimer(lifeTime, maxLifeTime);
            yield return null;
        }

        if (!isClear)
            Clear(false); loadQuest.ShowTasting(Quest.TASTING.NONE);
    }

    public void Clear(bool isClear)
    {
        if (!isClear)
        {
            --life;
            ui.lifeHeart.UpdateLife(life, maxLife);

            if (life < 1)
            {
                //TODO :: 게임 오버
            }
        }
    }

    public void NextNPC()
    {
        ui.timer.UpdateTimer(1,1);

        npcBehaviour.ChangeNpc();
        loadQuest.CreateQuest();
        cookingManager.foodSlotManager.SetSlot();
    }

    public void StartTimer()
    {
        if (timerCoroutine != null)
            StopTimer();

        timerCoroutine = StartCoroutine(UpdateTime());
    }


    public void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }

    public void AddScore(int score)
    {
        switch (score)
        {
            case 0:
                Clear(false);
                loadQuest.ShowTasting(Quest.TASTING.BAD);
                break;
            case 40:
                loadQuest.ShowTasting(Quest.TASTING.SOSO);
                break;
            case 70:
                loadQuest.ShowTasting(Quest.TASTING.GOOD);
                break;
            case 100:
                loadQuest.ShowTasting(Quest.TASTING.GREAT);
                break;
        }

        Clear(true);

        this.score += score;
        ui.textScore.UpdateText(this.score);
    }

    public void SubScore(int score)
    {
        this.score -= score;
        ui.textScore.UpdateText(this.score);

    }

}
