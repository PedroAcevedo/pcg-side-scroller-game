using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private GameObject item;
    private Vector2 size;
    private int score;
    private int type;
    public Item(GameObject item, Vector2 size, int score, int type)
    {
        this.item = item;
        this.size = size;
        this.score = score;
        this.type = type;
    }

    public GameObject getItem()
    {
        return item;
    }

    public Vector2 getSize()
    {
        return size;
    }

    public int getScore()
    {
        return score;
    }

    public int GetItemType()
    {
        return type;
    }

}
