using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float waitTime = 2.0f;
    private float speed = 1.0f;
    private float descentAmount = 0.1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        var vel = rb2d.velocity;
        vel.x = speed;
        rb2d.velocity = vel;

        // Altera a cor do sprite para branco
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            ChangeState();
            timer = 0.0f;
        }
    }

    void ChangeState()
    {
        var vel = rb2d.velocity;
        vel.x *= -1; // Inverte a direção horizontal
        rb2d.velocity = vel;

        // Faz os invasores descerem
        transform.position += Vector3.down * descentAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o invasor colidiu com o Bouncer
        if (other.CompareTag("Bouncer"))
        {
            GameManager.Instance.GameOver(); // Chama o Game Over
        }
    }
}