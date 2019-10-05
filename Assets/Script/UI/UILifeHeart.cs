using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILifeHeart : MonoBehaviour
{
    [SerializeField]
    GameObject[] lifes;
    
    public void UpdateLife(int current, int max)
    {
        for (int i = 0; i < lifes.Length; ++i)
        {
            if (i < current)
                lifes[i].SetActive(true);
            else
                lifes[i].SetActive(false);
        }

    }


}
