using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void ON()
    {
        this.gameObject.transform.position = new Vector3(-3, 8.3f);
        this.gameObject.SetActive(true);
        StartCoroutine(moveArm());
    }

    public void OFF()
    {
        this.gameObject.SetActive(false);
    }

    public IEnumerator moveArm()
    {
        while (true)
        {
            this.transform.position -= new Vector3(0, 3, 0) * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            if (this.transform.position.y <= 4.3f) break;
        }
    }
}
