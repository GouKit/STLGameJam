using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string context;

    public string spriteName;

    public Sprite GetSprite() {
        return Resources.Load<Sprite>("Sprites/"+spriteName);
    }

}
