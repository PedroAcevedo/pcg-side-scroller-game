using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private TMPro.TextMeshProUGUI cherriesLabel;
    [SerializeField] private TMPro.TextMeshProUGUI deathLabel;
    [SerializeField] private TMPro.TextMeshProUGUI maxScoreLabel;

    void Start()
    {
        deathLabel.text = "Deaths: " + PlayerPrefs.GetInt("death");
        maxScoreLabel.text = "MaxScore: " + PlayerPrefs.GetInt("maxScore");
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            cherries += 1;
            cherriesLabel.text = "Cherries: " + cherries;


            if (cherries > PlayerPrefs.GetInt("maxScore"))
            {
                PlayerPrefs.SetInt("maxScore", cherries);
            }
        }
    }
}
