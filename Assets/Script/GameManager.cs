using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    float maxLifeTime;
    public float lifeTime;

    CookingManager cookingManager;

    protected override void Awake()
    {
        base.Awake();

        cookingManager = FindObjectOfType<CookingManager>();
        cookingManager.SetGameManger(this);
    }

    private void Start()
    {
        //TODO :: npc 등장 >> 대화 >> 
        cookingManager.StartGame();
    }

}
