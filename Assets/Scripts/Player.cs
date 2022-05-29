using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public
    public int health, maxhealth, damage, fireResist, elecResist;
    public GameObject K;
    public Foot leftFoot, rightFoot;
    public bool canFly;

    //Inside
    bool movable, inEnemy = false, inFire = false, inElec = false, inBoss = false;
    Vector3 movement;
    int moveSpeed = 3;
    float dealtime = 1;

    //Triggers
    Enemy enemy;
    Trap firetrap;
    Trap electrap;

    MainSM mainSM;
    float attackdelay = 0;
    void Start()
    {
        health = maxhealth;
        fireResist = 0;
        elecResist = 0;
        canFly = false;
        
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
            if (inEnemy == true)
                mainSM.PlayerDead(0);
            else if (inFire == true)
                mainSM.PlayerDead(1);
            else if (inElec == true)
                mainSM.PlayerDead(3);
            else
                mainSM.PlayerDead(4);

            movable = false;
        }

        if(inEnemy == true)
        {
            //Attack
            if (attackdelay >= 0.5 && Input.GetKeyDown(KeyCode.K))
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

        if(inFire == true)
        {
            if (dealtime >= 0.5f)
            {
                health -= (int) (100 * ((float) (100 - fireResist) / 100));
                dealtime = 0;
            }
        }

        if (inElec == true)
        {
            if (dealtime >= 0.2f)
            {
                health -= (int)(maxhealth * 1.0 / 10 * ((float)(100 - elecResist) / 100));
                dealtime = 0;
            }
        }

        if (leftFoot.isFalling == true && rightFoot.isFalling == true)
        {
            mainSM.PlayerDead(2); //Falling
        }
    }
    public void Revive()
    {
        health = maxhealth;
        movable = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            K.SetActive(true);
            enemy = collision.gameObject.GetComponent<Enemy>();
            inEnemy = true;
            movable = false;
        }
        else if (collision.gameObject.tag == "FireTrap")
        {
            firetrap = collision.gameObject.GetComponent<Trap>();
            firetrap.on = true;
            inFire = true;
        }
        else if (collision.gameObject.tag == "ElecTrap")
        {
            electrap = collision.gameObject.GetComponent<Trap>();
            electrap.on = true;
            inElec = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            K.SetActive(false);
            inEnemy = false;
            enemy = null;
            movable = true;
        }
        else if (collision.gameObject.tag == "FireTrap")
        {
            inFire = false;
            firetrap = null;
        }
        else if (collision.gameObject.tag == "ElecTrap")
        {
            inElec = false;
            electrap = null;
        }
    }
}
