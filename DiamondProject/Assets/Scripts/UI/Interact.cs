using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string objTag;
    public GameObject obj;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.tag == objTag)
        {
            collision.gameObject.GetComponent<Interact>();
        }
    }
}
