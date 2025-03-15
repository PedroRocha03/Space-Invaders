using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float speed = 5.0f;
    public float boundX = 5.25f;
    private Rigidbody2D rb2d;
    public Projectile laserPrefab;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var pos = transform.position;
        var vel = rb2d.velocity;
        if (Input.GetKey(moveRight))
        {
            vel.x = speed;
        }
        else if (Input.GetKey(moveLeft))
        {
            vel.x = -speed;
        }
        else
        {
            vel.x = 0;
        }

        rb2d.velocity = vel;

        pos.x = Mathf.Clamp(pos.x, -boundX, boundX);
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }
}