using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int type;
    Animator animator;
    public bool on;
    bool otp = false, soundON = false;
    AudioSource audioSource;
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if(otp == false && on == true)
        {
            animator.SetBool("ON", true);
            otp = true;
        }
        
        if(on == true && soundON == false)
        {
            audioSource.Play();
            soundON = true;
        }

        if (on == false && soundON == true)
        {
            audioSource.Stop();
            soundON = false;
        }
    }


}
