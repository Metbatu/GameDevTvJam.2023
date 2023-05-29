using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed of the moving objects

    private void Update()
    {
        // Move the object from right to left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // If the object goes off the left side of the screen, destroy it
        if (transform.position.x < -125f)
        {
            Destroy(gameObject);
        }
    }
}