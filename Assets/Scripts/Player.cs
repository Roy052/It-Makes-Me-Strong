using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public
    public int health, maxhealth, damage;
    public GameObject K;
    public Foot leftFoot, rightFoot;

    //Inside
    bool movable, inEnemy = false;
    Vector3 movement;
    int moveSpeed = 3;
    float dealtime = 1;
    Enemy enemy;
    MainSM mainSM;
    float attackdelay = 0;
    void Start()
    {
        health = maxhealth;
        mainSM = GameObject.Find("MainSM").GetComponent<MainSM>();
        movable = true;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        dealtime += Time.deltaTime;
        attackdelay += Time.deltaTime;

        if (movable)
            this.transform.position += movement * moveSpeed * Time.deltaTime;

        if (health <= 0)
        {
            mainSM.PlayerDead(0); //HP = 0
            movable = false;
        }
        if(inEnemy == true)
        {
            //Attack
            if (attackdelay >= 0.2 && Input.GetKeyDown(KeyCode.K))
            {
                enemy.health -= damage;
                attackdelay = 0;
            }

            //Damaged
            if (dealtime >= 1)
            {
                health -= enemy.damage;
                dealtime = 0;
            }
        }
        if(leftFoot.isFalling == true && rightFoot.isFalling == true)
        {
            mainSM.PlayerDead(1); //Falling
        }
    }
    public void Revive()
    {
        health = maxhealth;
        movable = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        K.SetActive(true);
        if (collision.gameObject.tag == "Enemy")
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            inEnemy = true;
            movable = false;
        }
        else if (collision.gameObject.tag == "FireTrap")
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        K.SetActive(false);
        if (collision.gameObject.tag == "Enemy")
        {
            inEnemy = false;
            enemy = null;
            movable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
