using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{

    UIInGameManager ui;

    public List<NPC> npcList;

    Transform currentNPC;

    private void Awake()
    {
        ui = FindObjectOfType<UIInGameManager>();
    }

    void Start()
    {
        ChangeNpc();
    }

    public void ChangeNpc()
    {
        if (currentNPC != null)
            Destroy(currentNPC.gameObject);

        int randIndex = Random.Range(0, npcList.Count);

        currentNPC = Instantiate(npcList[randIndex].npcObject, transform).transform;
        currentNPC.localPosition = Vector3.zero;

//        ui.textNpcName.UpdateText(npcList[randIndex].npcName);

    }
}
