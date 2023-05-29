using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin1 : MonoBehaviour
{
    
    [SerializeField]
    private AudioClip clickSound;
    private int count = 15;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Collectible"))
        {
            Debug.Log(count);
            count++;
            AudioSource.PlayClipAtPoint(clickSound, other.transform.position);
            Destroy(other.gameObject);
        }
    }
}