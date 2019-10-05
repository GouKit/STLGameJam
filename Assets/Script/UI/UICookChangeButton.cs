using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICookChangeButton : MonoBehaviour
{
    static int currentViewIndex;

    Button btnCookChange;

    CookingManager cookingManager;

    UIInGameManager ui;


    [SerializeField]
    int changeDir = 0;

    private void Awake()
    {
        ui = FindObjectOfType<UIInGameManager>();
        cookingManager = FindObjectOfType<CookingManager>();

        btnCookChange = GetComponent<Button>();
        btnCookChange.onClick.AddListener(OnButtonDown);
    }

    public void OnButtonDown()
    {
        if (currentViewIndex + changeDir < -1
            || currentViewIndex + changeDir > 1)
            return;


        currentViewIndex += changeDir;

        switch (currentViewIndex)
        {
            case -1:
                ui.textCookContext.UpdateText("찌기");
                break;
            case 0:
                ui.textCookContext.UpdateText("그대로");
                break;
            case 1:
                ui.textCookContext.UpdateText("굽기");
                break;
        }

        cookingManager.ChangeCookType(changeDir, currentViewIndex);
    }

}
