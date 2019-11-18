using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float objectsSpeed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * objectsSpeed; //Uses the current axis of the GameObject to go forward.
    }

    // Asteroid/Enemy Ship speed is -5, laser bolt speed is 20
}