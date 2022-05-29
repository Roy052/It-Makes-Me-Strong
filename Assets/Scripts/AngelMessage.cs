using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelMessage : MonoBehaviour
{
    public Camera mainCamera;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        this.gameObject.transform.position = mainCamera.transform.position + new Vector3(0,0,5);
    }

    public void ON()
    {
        animator.SetBool("ON", true);
        Debug.Log("ON");
    }

    public void OFF()
    {
        animator.SetBool("ON", false);
        Debug.Log("OFF");
    }
}
