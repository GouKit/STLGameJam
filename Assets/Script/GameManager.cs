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

    int score;
    int playCount = 0;

    bool isClear = false;

    [SerializeField]
    private int maxLife = 2;
    [SerializeField]
    private int life = 0;

    protected override void Awake()
    {
        base.Awake();

        cookingManager = FindObjectOfType<CookingManager>();
        loadQuest = FindObjectOfType<LoadQuest>();
        ui = FindObjectOfType<UIInGameManager>();
        npcBehaviour = FindObjectOfType<NPCBehaviour>();

        cookingManager.SetGameManger(this);
        loadQuest.talkEnd += StartCook;
    }

    private void Start()
    {
        loadQuest.CreateQuest();
        SubScore(0);
    }

    private void StartCook()
    {
        cookingManager.StartGame();
        StartCoroutine(UpdateTime());
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
            Clear(false);
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

        NextNPC();
    }

    public void NextNPC()
    {
        npcBehaviour.ChangeNpc();
        loadQuest.CreateQuest();
    }

    public void AddScore(int score)
    {
        switch (score)
        {
            case 0:
                Clear(false);
                break;
            case 40:
            case 70:
            case 100:
                Clear(true);
                break;
        }

        this.score += score;
        ui.textScore.UpdateText(this.score);
    }

    public void SubScore(int score)
    {
        this.score -= score;
        ui.textScore.UpdateText(this.score);

    }

}
