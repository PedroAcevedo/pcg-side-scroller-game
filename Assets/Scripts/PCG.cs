using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCG : MonoBehaviour
{
    public enum LevelDificulty { easy, medium, hard };

    public GameObject[] terrain;
    public GameObject[] items;
    public Vector2[] size;
    public int[] itemType;
    public int[] scores;
    public LevelDificulty dificulty;

    private int chunkScore = 5;
    private int chunkLength = 30;
    private int generatedChunks = 1;
    private Chunk currentChunk;
    private List<Item> itemList;
    public Chunk previousChunk;

    private bool isFloor = false;
    private int selectedChunkType = 0;
    private int[] dificultyScore = { 26, 36, 56 };
    private int currentChunkScore = 0;
    private List<int> scoreWallet = new List<int>();
    private List<int> selectedItems = new List<int>();

    void Start()
    {
        currentChunk = new Chunk(chunk1, Instantiate(terrain[selectedChunkType], new Vector3(0f, 0f, 0f), Quaternion.identity));

        createItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > (chunkLength / 2) * generatedChunks && !isFloor)
        {
            generateChunk();
        }

        if (transform.position.x > (((chunkLength) * generatedChunks) + +(chunkLength / 6)))
        {
            previousChunk.Destroy();
            previousChunk = null;
            isFloor = false;
            generatedChunks++;
        }
    }

    public void createItems()
    {
        itemList = new List<Item>();

        for(int i = 0; i < items.Length; i++)
        {
            itemList.Add(new Item(items[i], size[i], scores[i], itemType[i]));

            if (!scoreWallet.Contains(scores[i]))
            {
                scoreWallet.Add(scores[i]);
            }
        }

        scoreWallet.Sort((p1, p2) => p2.CompareTo(p1));
        Debug.Log("Wallet -> " + string.Join(", ", scoreWallet.ToArray()));
    }

    public void generateChunk()
    {
        previousChunk = currentChunk;
        selectedItems.Clear();

        selectedChunkType = getRandomInt(0, terrain.Length);
        currentChunk = new Chunk(getChunkInfo(selectedChunkType), Instantiate(terrain[selectedChunkType], new Vector3(chunkLength * (generatedChunks), 0f, 0f), Quaternion.identity));
        isFloor = true;

        currentChunkScore = selectedChunkType * chunkScore;

        coinWallet();

        placeElements();

    }

    public void coinWallet()
    {
        int amount = dificultyScore[(int)dificulty] - currentChunkScore;

        int max = scoreWallet[0];

        for (int i = 0; i < scoreWallet.Count; i++)
        {
            int numbers = amount / scoreWallet[i];

            if(scoreWallet[i] == max && numbers > 2)
            {
                numbers = 2;
            }

            if(scoreWallet[i] < max && scoreWallet[i] > 2 && numbers > getHigherLimit())
            {
                numbers = getHigherLimit() - 1;
            }

            if (numbers > 0)
            {
                int ratio = scoreWallet[i] * numbers;
                if (ratio <= amount)
                {
                    amount -= ratio;
                    
                    for(int j = 0; j < numbers; j++)
                        selectedItems.Add(scoreWallet[i]);
                }
            }

            if (amount == 0)
            {
                break;
            }
        }

        if(amount > 0)
        {
            for (int j = 0; j < 3; j++)
                selectedItems.Add(scoreWallet[scoreWallet.Count - 1]);
        }

        Debug.Log("Current Chunk Score -> " + (dificultyScore[(int)dificulty] - currentChunkScore));
        Debug.Log("Used Chunk Elements -> " + string.Join(", ", selectedItems.ToArray()));
    }

    public void placeElements() 
    {
        for (int i = 0; i < selectedItems.Count; i++)
        {

            int selectedItem = getRandomItemByScore(selectedItems[i]);
            int cellPosition;

            bool placed = false;

            do
            {
                cellPosition = currentChunk.getRandomCell();

                if(currentChunk.couldBePlaced(cellPosition, (int)size[selectedItem].x, itemList[selectedItem].GetItemType()))
                {
                    GameObject generatedItem = Instantiate(itemList[selectedItem].getItem(), new Vector3((chunkLength * (generatedChunks)) + currentChunk.getCell(cellPosition).x, itemList[selectedItem].getSize().y, 0f), Quaternion.identity);

                    currentChunk.addItem(generatedItem, cellPosition, (int)itemList[selectedItem].getSize().x);
                    placed = true;
                }

            } while (!placed);

        }
    }

    public int getRandomInt(int init, int end) // Range is inclusives
    {
        return (int)Random.Range(init, end);
    }

    public int getRandomItemByScore(int score)
    {
        List<int> sameScoreItems = new List<int>();

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].getScore() == score)
                sameScoreItems.Add(i);
        }

        return sameScoreItems[getRandomInt(0, sameScoreItems.Count)];
    }

    public Vector2[] getChunkInfo(int selectedChunk)
    {
        switch (selectedChunk)
        {
            case 0:
                return chunk1;
            case 1:
                return chunk2;
            case 2:
                return chunk3;
            case 3:
                return chunk4;
            case 4:
                return chunk5;
        }

        return chunk1;
    }

    public int getHigherLimit()
    {
        return ((dificultyScore[(int)dificulty] % 100 - dificultyScore[(int)dificulty] % 10) / 10) - 1;
    }

    private Vector2[] chunk1 =
    {
        new Vector2(-8.5f, -1.57f), new Vector2(-7.5f, -1.57f), new Vector2(-6.5f, -1.57f), new Vector2(-5.5f, -1.57f), new Vector2(-4.5f, -1.57f), new Vector2(-3.5f, -1.57f), new Vector2(-2.5f, -1.57f), new Vector2(-1.5f, -1.57f), new Vector2(-0.5f, -1.57f), new Vector2(0.5f, -1.57f), new Vector2(1.5f, -1.57f), new Vector2(2.5f, -1.57f), new Vector2(3.5f, -1.57f), new Vector2(4.5f, -1.57f), new Vector2(5.5f, -1.57f), new Vector2(6.5f, -1.57f), new Vector2(7.5f, -1.57f), new Vector2(8.5f, -1.57f), new Vector2(9.5f, -1.57f), new Vector2(10.5f, -1.57f), new Vector2(11.5f, -1.57f), new Vector2(12.5f, -1.57f), new Vector2(13.5f, -1.57f), new Vector2(14.5f, -1.57f), new Vector2(15.5f, -1.57f), new Vector2(16.5f, -1.57f), new Vector2(17.5f, -1.57f), new Vector2(18.5f, -1.57f), new Vector2(19.5f, -1.57f), new Vector2(20.5f, -1.57f)
    };

    private Vector2[] chunk2 =
    {
        new Vector2(-8.5f, -1.57f), new Vector2(-7.5f, -1.57f), new Vector2(-6.5f, -1.57f), new Vector2(-5.5f, -1.57f), new Vector2(-4.5f, -1.57f), new Vector2(-3.5f, -1.57f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(-0.5f, -1.57f), new Vector2(0.5f, -1.57f), new Vector2(1.5f, -1.57f), new Vector2(2.5f, -1.57f), new Vector2(3.5f, -1.57f), new Vector2(4.5f, -1.57f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(7.5f, -1.57f), new Vector2(8.5f, -1.57f), new Vector2(9.5f, -1.57f), new Vector2(10.5f, -1.57f), new Vector2(11.5f, -1.57f), new Vector2(12.5f, -1.57f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(15.5f, -1.57f), new Vector2(16.5f, -1.57f), new Vector2(17.5f, -1.57f), new Vector2(18.5f, -1.57f), new Vector2(19.5f, -1.57f), new Vector2(20.5f, -1.57f)
    };

    private Vector2[] chunk3 =
    {
         new Vector2(-8.5f, -1.57f), new Vector2(-7.5f, -1.57f), new Vector2(-6.5f, -1.57f), new Vector2(-5.5f, -1.57f), new Vector2(-4.5f, -1.57f), new Vector2(-3.5f, -1.57f), new Vector2(-2.5f, -1.57f), new Vector2(-1.5f, -1.57f), new Vector2(-0.5f, -1.57f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(7.5f, -1.57f), new Vector2(8.5f, -1.57f), new Vector2(9.5f, -1.57f), new Vector2(10.5f, -1.57f), new Vector2(11.5f, -1.57f), new Vector2(12.5f, -1.57f), new Vector2(13.5f, -1.57f), new Vector2(14.5f, -1.57f), new Vector2(15.5f, -1.57f), new Vector2(16.5f, -1.57f), new Vector2(17.5f, -1.57f), new Vector2(18.5f, -1.57f), new Vector2(19.5f, -1.57f), new Vector2(20.5f, -1.57f)
    };

    private Vector2[] chunk4 =
    {
        new Vector2(-8.5f, -1.57f), new Vector2(-7.5f, -1.57f), new Vector2(-6.5f, -1.57f), new Vector2(-5.5f, -1.57f), new Vector2(-4.5f, -1.57f), new Vector2(-3.5f, -1.57f), new Vector2(-2.5f, -1.57f), new Vector2(-1.5f, -1.57f), new Vector2(-0.5f, -1.57f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(2.5f, -1.57f), new Vector2(3.5f, -1.57f), new Vector2(4.5f, -1.57f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(7.5f, -1.57f), new Vector2(8.5f, -1.57f), new Vector2(9.5f, -1.57f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(12.5f, -1.57f), new Vector2(13.5f, -1.57f), new Vector2(14.5f, -1.57f), new Vector2(15.5f, -1.57f), new Vector2(16.5f, -1.57f), new Vector2(17.5f, -1.57f), new Vector2(18.5f, -1.57f), new Vector2(19.5f, -1.57f), new Vector2(20.5f, -1.57f)
    };

    private Vector2[] chunk5 =
    {
        new Vector2(-8.5f, -1.57f), new Vector2(-7.5f, -1.57f), new Vector2(-6.5f, -1.57f), new Vector2(-5.5f, -1.57f), new Vector2(-4.5f, -1.57f), new Vector2(-3.5f, -1.57f), new Vector2(-2.5f, -1.57f), new Vector2(-1.5f, -1.57f), new Vector2(-0.5f, -1.57f), new Vector2(0f, 0f), new Vector2(1.5f, -0.6f), new Vector2(2.5f, -0.6f), new Vector2(3.5f, -0.6f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(7.5f, 1.43f), new Vector2(8.5f, 1.43f), new Vector2(9.5f, 1.43f), new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(12.5f, -1.57f), new Vector2(13.5f, -1.57f), new Vector2(14.5f, -1.57f), new Vector2(15.5f, -1.57f), new Vector2(16.5f, -1.57f), new Vector2(17.5f, -1.57f), new Vector2(18.5f, -1.57f), new Vector2(19.5f, -1.57f), new Vector2(20.5f, -1.57f)
    };
}
