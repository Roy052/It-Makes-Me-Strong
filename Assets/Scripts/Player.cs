using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public
    public int health, maxhealth, damage, fireResist, elecResist;
    public GameObject K;
    public Foot leftFoot, rightFoot;
    public bool canFly, isMoving, indead;

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
        //Initialize
        health = maxhealth;
        fireResist = 0;
        elecResist = 0;
        canFly = false;
        indead = false;
        movable = true;

        mainSM = GameObject.Find("MainSM").GetComponent<MainSM>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 0.0001f) isMoving = true;
        else isMoving = false;

        dealtime += Time.deltaTime;
        attackdelay += Time.deltaTime;

        if (movable)
            this.transform.position += movement * moveSpeed * Time.deltaTime;

        if (health <= 0 && indead == false)
        {
            if (inEnemy == true)
                mainSM.PlayerDead(0);
            else if (inFire == true)
                mainSM.PlayerDead(1);
            else if (inElec == true)
                mainSM.PlayerDead(3);
            else
                mainSM.PlayerDead(4);

            indead = true;
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

        if(inBoss == true)
        {
            //Attack
            if (attackdelay >= 0.5 && Input.GetKeyDown(KeyCode.K))
            {
                enemy.health -= damage;
                attackdelay = 0;
            }
        }

        if (canFly == false && leftFoot.isFalling == true && rightFoot.isFalling == true)
        {
            mainSM.PlayerDead(2); //Falling
        }
    }
    public void Revive()
    {
        health = maxhealth;
        indead = false;
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
            moveSpeed = 1;
        }
        else if (collision.gameObject.tag == "Area")
        {
            collision.GetComponent<Area>().inArea = true;
        }
        else if (collision.gameObject.tag == "BossAttack")
        {
            health -= 1000;
            inBoss = true;
        }
        else if (collision.gameObject.tag == "Door")
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.MainToEnd();
        }
        else if (collision.gameObject.tag == "Boss")
        {
            inBoss = true;
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
            moveSpeed = 3;
        }
        else if (collision.gameObject.tag == "Area")
        {
            collision.GetComponent<Area>().inArea = false;
        }
        else if (collision.gameObject.tag == "Boss")
        {
            inBoss = false;
        }
    }
}
