using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    UIInGameManager ui;

    [SerializeField]
    float maxLifeTime;
    public float lifeTime;

    CookingManager cookingManager;
    LoadQuest loadQuest;

    int score;

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

        cookingManager.SetGameManger(this);
        loadQuest.talkEnd += StartCook;
    }

    private void Start()
    {
        loadQuest.CreateQuest();
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
        loadQuest.CreateQuest();
    }

    public void AddScore(int score)
    {
        this.score += score;
        ui.textScore.UpdateText(this.score);
    }

    public void SubScore(int score)
    {
        this.score -= score;
        ui.textScore.UpdateText(this.score);

    }

}
