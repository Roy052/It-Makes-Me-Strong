using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int type;
    Animator animator;
    public bool on;
    bool otp = false;
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(otp == false && on == true)
        {
            animator.SetBool("ON", true);
            otp = true;
        }
    }


}
