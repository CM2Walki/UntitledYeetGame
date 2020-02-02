using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGravity : MonoBehaviour
{
    public Vector3 groundlevel = new Vector3(0, -5f, 0);
    public float gravity = 3.81f;

    private Vector3 velocity;

    void Update()
    {
        velocity.y -= gravity * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;

        if (transform.position.y <= groundlevel.y)
        {
            transform.position = new Vector3(transform.position.x, groundlevel.y, transform.position.z);
            velocity = Vector3.zero; 
        }
    }
}
