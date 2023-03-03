using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadChunk : MonoBehaviour
{
    public List<GameObject> chunks;
    public float Y = -1.5f;
    public int currentChunk;

    private List<Vector2> chunkFloors = new List<Vector2>();

    private int chunkLength = 30;
    private int stepCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(transform.position.x, Y);

        for(int i = 0; i < chunks.Count; i++)
        {
            chunks[i].SetActive(i == currentChunk);
        }

    }

    public void MoveToRight()
    {
        transform.position = new Vector2(transform.position.x + 1, Y);
        stepCount++;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Touching floor");

            chunkFloors.Add(new Vector2(transform.position.x, transform.position.y));


            if (stepCount == chunkLength)
            {

                Debug.Log("Chunk = " + string.Join(", ",
                            chunkFloors
                            .ConvertAll(i => "new Vector2(" + System.Math.Round(i.x,2) + "f, " + System.Math.Round(i.y,2) + "f)" )
                            .ToArray()));
                stepCount = -1;
            }
        }
        else
        {
            Debug.Log("Its a hole");
            chunkFloors.Add(new Vector2(0, 0));
        }

        if (stepCount != -1)
            MoveToRight();
    }
}
