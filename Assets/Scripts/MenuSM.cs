using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSM : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void ButtonSoundON()
    {
        audioSource.Play();
    }
}
