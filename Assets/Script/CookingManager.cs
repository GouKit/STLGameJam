using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    
    CookPointer cookPointer;

    [SerializeField]
    Stick currentStick;


    void Awake()
    {
        cookPointer = FindObjectOfType<CookPointer>();
    }

    private void Start()
    {
        currentStick.finishEvent += FinishStickPush;
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


}
