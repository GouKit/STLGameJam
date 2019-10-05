using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    GameManager gm;

    CookPointer cookPointer;
    
    [SerializeField]
    Stick currentStick;

    void Awake()
    {
        cookPointer = FindObjectOfType<CookPointer>();
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
        cookPointer.SetInput(false);
    }

    public void SetGameManger(GameManager gm) {
        this.gm = gm;
    }

}
