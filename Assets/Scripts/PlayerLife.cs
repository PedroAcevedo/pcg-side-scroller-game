using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die() 
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");

        if (PlayerPrefs.GetInt("death") != 0)
        {
            PlayerPrefs.SetInt("death", PlayerPrefs.GetInt("death") + 1);
        }
        else
        {
            PlayerPrefs.SetInt("death", 1);
        }
    }

    private void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
