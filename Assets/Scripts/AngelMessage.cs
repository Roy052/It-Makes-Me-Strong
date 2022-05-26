using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMessage : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ON()
    {
        animator.SetBool("ON", true);
    }

    public void OFF()
    {
        animator.SetBool("ON", false);
    }
}
