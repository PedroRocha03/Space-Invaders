using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderProjectile : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public Vector3 direction = Vector3.down;
    public float speed = 5f;
    public int points = 10;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            PlayerControl player = coll.GetComponent<PlayerControl>();
            if (player != null)
            {
                player.Die(); // Chama o método Die do jogador
                GameManager.Instance.OnPlayerKilled(player); // Notifica o GameManager
            }

            Destroy(gameObject); // Destroi o projétil
        }
    }
}