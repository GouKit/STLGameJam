using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSlotManager : MonoBehaviour
{
    [SerializeField]
    List<FoodSlot> slots;

    private void Awake()
    {
        slots.AddRange(transform.GetComponentsInChildren<FoodSlot>());
    }

    private void Start()
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].SetFood(1);
        }
    }

}
