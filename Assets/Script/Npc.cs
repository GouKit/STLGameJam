using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    [Header("NPC 이미지")]
    public List<Sprite> image = new List<Sprite>();
    private Image npc;

    void Start()
    {
        npc = GetComponent<Image>();
        npc.sprite = image[Random.Range(0, image.Count-1)];
    }

}
