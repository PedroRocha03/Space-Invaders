using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public Vector3 direction = Vector3.up;
    public float speed = 20f;
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
        if (coll.CompareTag("Invader")) // Verifica apenas "Invader"
        {
            Invaderr invader = coll.GetComponent<Invaderr>();
            if (invader != null)
            {
                GameManager.Instance.OnInvaderKilled(invader);
            }

            Destroy(coll.gameObject); // Destroi o inimigo
            Destroy(gameObject); // Destroi o laser
        }
    }
}
