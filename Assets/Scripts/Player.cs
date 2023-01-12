using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public GameObject attackOriginal;
    public GameObject attackPosition;

    public GameObject pause;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioHurt;

    Rigidbody2D rb;
    bool isGrounded;
    bool isAttack;
    bool isDefend;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioHurt = GetComponent<AudioSource>();
        pause.SetActive(false);
        isGrounded = false;
        isAttack = false;
        isDefend = false;
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        
    }

    void Update()
    {
        MoveLeft();
        MoveRight();
        Jump();
        Attack();
        Defend();
        Pause();
    }

    //-------------------------------------------------------------------------------------//

    private void MoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (Time.timeScale == 1)
            {
                spriteRenderer.flipX = false;
                if (isDefend == false)
                {
                    transform.Translate(new Vector3(-0.3f, 0.0f));
                    gameObject.GetComponent<Animator>().SetBool("moving", true);
                }
            }
        }
#if MOBILE_INPUT
        else if (CrossPlatformInputManager.GetAxis("Horizontal") <= -1) 
        {
            if (Time.timeScale == 1)
            {
                spriteRenderer.flipX = false;
                if (isDefend == false)
                {
                    transform.Translate(new Vector3(-0.3f, 0.0f));
                    gameObject.GetComponent<Animator>().SetBool("moving", true);
                }
            }
        }
#endif
        else
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }
    }

    private void MoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (Time.timeScale == 1)
            {
                spriteRenderer.flipX = true;
                if (isDefend == false)
                {
                    transform.Translate(new Vector3(0.3f, 0.0f));
                    gameObject.GetComponent<Animator>().SetBool("moving", true);
                }
            }
        }
#if MOBILE_INPUT
        else if (CrossPlatformInputManager.GetAxis("Horizontal") >= 1)
        {
            if (Time.timeScale == 1)
            {
                spriteRenderer.flipX = true;
                if (isDefend == false)
                {
                    transform.Translate(new Vector3(0.3f, 0.0f));
                    gameObject.GetComponent<Animator>().SetBool("moving", true);
                }
            }
        }
#endif
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if (Time.timeScale == 1)
            {
                isGrounded = false;
                isAttack = false;
                gameObject.GetComponent<Animator>().SetBool("jumping", true);
                rb.AddForce(new Vector3(0.0f, 600.0f));
            }
        }
#if MOBILE_INPUT
        else if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded)
        {
            if (Time.timeScale == 1)
            {
                isGrounded = false;
                isAttack = false;
                gameObject.GetComponent<Animator>().SetBool("jumping", true);
                rb.AddForce(new Vector3(0.0f, 600.0f));
            }
        }
#endif
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Time.timeScale == 1)
            {
                if (isAttack == false)
                {
                    Invoke("Attacking", 0.75f);
                    gameObject.GetComponent<Animator>().SetBool("attacking", true);
                    isAttack = true;
                    Invoke("FinishAttacking", 0.5f);
                }
            }
        }
#if MOBILE_INPUT
        else if (CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            if (Time.timeScale == 1)
            {
                if (isAttack == false)
                {
                    Invoke("Attacking", 0.75f);
                    gameObject.GetComponent<Animator>().SetBool("attacking", true);
                    isAttack = true;
                    Invoke("FinishAttacking", 0.5f);
                }
            }
        }
#endif
    }
    private void Attacking()
    {
        Instantiate(attackOriginal, attackPosition.transform.position, attackPosition.transform.rotation);
    }
    private void FinishAttacking()
    {
        gameObject.GetComponent<Animator>().SetBool("attacking", false);
        isAttack = false;
    }

    private void Defend()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isAttack = true;
            isDefend = true;
            gameObject.GetComponent<Animator>().SetBool("defending", true);
        }
#if MOBILE_INPUT
        else if (CrossPlatformInputManager.GetButton("Defend"))
        {
            isAttack = true;
            isDefend = true;
            gameObject.GetComponent<Animator>().SetBool("defending", true);
        }
#endif
        else
        {
            isAttack = false;
            isDefend = false;
            gameObject.GetComponent<Animator>().SetBool("defending", false);
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pause.SetActive(true);

            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pause.SetActive(false);
            }
        }
#if MOBILE_INPUT
        else if (CrossPlatformInputManager.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pause.SetActive(true);

            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pause.SetActive(false);
            }
        }
#endif
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        {
            isGrounded = true;
            isAttack = true;
            gameObject.GetComponent<Animator>().SetBool("jumping", false);
        }

        if(collision.gameObject.CompareTag("EnemyPhase1") && isDefend == false)
        {
            gameObject.GetComponent<Animator>().SetTrigger("hurt");
            audioHurt.Play();
            LifeManager.lifeManager.TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("EnemyPhase2") && isDefend == false)
        {
            gameObject.GetComponent<Animator>().SetTrigger("hurt");
            audioHurt.Play();
            LifeManager.lifeManager.TakeDamage(2);
        }
        else if (collision.gameObject.CompareTag("EnemyPhase3") && isDefend == false)
        {
            gameObject.GetComponent<Animator>().SetTrigger("hurt");
            audioHurt.Play();
            LifeManager.lifeManager.TakeDamage(2);
        }
        else if (collision.gameObject.CompareTag("Boss") && isDefend == false)
        {
            gameObject.GetComponent<Animator>().SetTrigger("hurt");
            audioHurt.Play();
            LifeManager.lifeManager.TakeDamage(3);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Coin"))
        {
            trigger.gameObject.GetComponent<AudioSource>().Play();
            ScoreManager.scoreManager.RaiseScore(100);
            trigger.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(trigger.gameObject, 0.5f);
        }

        if (trigger.gameObject.CompareTag("Life"))
        {
            //If the player is below his maximum life, increase by 1
            if (LifeManager.lifeManager.life < 10)
            {
                trigger.gameObject.GetComponent<AudioSource>().Play();
                LifeManager.lifeManager.RaiseLife(1);
                trigger.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(trigger.gameObject, 1.3f);
            } //If the player catches him having his maximum life, life coins will be destroyed
            else
            {
                trigger.gameObject.GetComponent<AudioSource>().Play();
                trigger.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(trigger.gameObject, 1.3f);
            }
        }
    }
}
