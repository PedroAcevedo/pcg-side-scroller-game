using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    private Vector2[] cells;
    private GameObject currentChunk;
    private bool[] availableCells;

    public Chunk(Vector2[] cells, GameObject currentChunk)
    {
        this.cells = cells;
        this.currentChunk = currentChunk;
        defineCellSpace();
    }

    public void defineCellSpace()
    {
        availableCells = new bool[cells.Length];

        for (int i = 0; i < cells.Length; i++)
        {
            availableCells[i] = isGrounded(i);
        }
    }

    public bool isGrounded(int i)
    {   
        return !(cells[i].x == 0.0f & cells[i].y == 0.0f); 
    }

    public bool isOccupied(int i)
    {
        return availableCells[i];
    }

    public void occupiedCell(int i)
    {
        availableCells[i] = false;
    }

    public Vector2 getCell(int i)
    {
        return cells[i];
    }

    public void addItem(GameObject item, int i, int size)
    {
        item.transform.parent = this.currentChunk.transform;

        int item_size = i + size;

        for (int j = i; j < item_size; j++)
        {
            availableCells[j] = false;
        }
    }

    public int getRandomCell()
    {
        List<int> availables = new List<int>();

        for(int i = 0; i < cells.Length; i++)
        {
            if (availableCells[i])
            {
                availables.Add(i);
            }
        }

        return availables[(int)Random.Range(0, availables.Count - 1)];
    }

    public bool couldBePlaced(int i, int size, int item)
    {
        int item_size = i + size;

        if (item_size > availableCells.Length)
            return false;

        if ((i == 0 || i == availableCells.Length) && item == 3)
            return false;

        for (int j = i; j < item_size; j++)
        {
            if (!availableCells[j])
            {
                return false;
            }
        }


        if(i > 0)
        {
            if(item == 2 || item == 3)
            {
                if (availableCells[i - 1] == false || availableCells[i + 1] == false)
                {
                    return false;
                }
            }
        }

        if(item == 3)
        {
            if (cells[i].y > -1.0)
            {
                return false;
            }
        }

        return true;
    }

    public void Destroy()
    {
        GameObject.Destroy(currentChunk);
    }
}
