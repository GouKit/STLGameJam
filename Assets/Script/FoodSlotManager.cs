using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSlotManager : MonoBehaviour
{
    [SerializeField]
    List<FoodSlot> slots;
    private FoodDB db;

    List<int> randFoodIDList = new List<int>();

    private void Awake()
    {
        slots.AddRange(transform.GetComponentsInChildren<FoodSlot>());
        db = FindObjectOfType<FoodDB>();
    }

    void InitRandFoodIDList()
    {
        randFoodIDList.Clear();
        int count = db.GetFoodDBCount();
        for (int i = 0; i < count; ++i)
        {
            randFoodIDList.Add(i + 1);
        }
    }

    private void Start()
    {
        SetSlot();
    }

    public void SetSlot()
    {
        InitRandFoodIDList();

        for (int i = 0; i < slots.Count; ++i)
        {
            int randFoodIndex = Random.Range(0, randFoodIDList.Count);
            int randFoodID = randFoodIDList[randFoodIndex];

            randFoodIDList.RemoveAt(randFoodIndex);

            slots[i].SetFood(randFoodID);

        }

    }
}
