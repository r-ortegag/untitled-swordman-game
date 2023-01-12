using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    int life;

    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    AudioSource audioHurt;
    public AudioSource audioDeath;
    public AudioSource audioAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioHurt = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance to player
        float distToPlayer = Vector3.Distance(transform.position, player.position);
        if (Time.timeScale == 1)
        {
            if (distToPlayer < agroRange)
            {
                //Chase player
                ChasePlayer();
            }
            else
            {
                //Stop chasing player
                StopChasingPlayer();
            }
        }
    }

    void ChasePlayer()
    {
        if (life > 0)
        {
            if (transform.position.x < player.position.x)
            {
                //Enemy is to the left side of the player, so move right
                rb2d.transform.Translate(new Vector3(0.1f, 0.0f));
                spriteRenderer.flipX = true;
                gameObject.GetComponent<Animator>().SetBool("moving", true);
            }
            else
            {
                //Enemy is to the right side of the player, so move left
                rb2d.transform.Translate(new Vector3(-0.1f, 0.0f));
                spriteRenderer.flipX = false;
                gameObject.GetComponent<Animator>().SetBool("moving", true);
            }
        }
    }

    void StopChasingPlayer()
    {
        rb2d.transform.Translate(new Vector3(0f, -0.1f));
        gameObject.GetComponent<Animator>().SetBool("moving", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioAttack.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Attack")
        {
            life -= 1;
            gameObject.GetComponent<Animator>().SetTrigger("hurt");
            audioHurt.Play();
            if (life == 0)
            {
                gameObject.GetComponent<Animator>().SetBool("isDeath", true);
                audioDeath.Play();
                Invoke("YouWin", 5f);
            }
        }
    }

    void YouWin()
    {
        SceneManager.LoadScene("YouWin");
    }
}
