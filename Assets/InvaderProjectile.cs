using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderProjectile : MonoBehaviour
{
    private BoxCollider2D boxCollider;  // BoxCollider2D para colisões
    public Vector3 direction = Vector3.down;  // Direção do míssil
    public float speed = 10f;  // Velocidade do míssil
    public int points = 10;  // Pontuação ao atingir o jogador

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();  // Certifique-se de que o BoxCollider2D está presente
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;  // Movimento do míssil
    }

    // Verifica a colisão com o jogador
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))  // Verifica se a tag do objeto atingido é "Player"
        {
            PlayerControl player = coll.GetComponent<PlayerControl>();  // Obtém o componente PlayerControl
            if (player != null)
            {
                GameManager.Instance.OnPlayerKilled(player);  // Chama o método de morte do jogador
            }

            Destroy(coll.gameObject);  // Destroi o jogador
            Destroy(gameObject);  // Destroi o projétil
        }
    }
}
