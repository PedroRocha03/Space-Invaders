using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderProjectile : MonoBehaviour
{
    private BoxCollider2D boxCollider;  // BoxCollider2D para colis�es
    public Vector3 direction = Vector3.down;  // Dire��o do m�ssil
    public float speed = 10f;  // Velocidade do m�ssil
    public int points = 10;  // Pontua��o ao atingir o jogador

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();  // Certifique-se de que o BoxCollider2D est� presente
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * direction;  // Movimento do m�ssil
    }

    // Verifica a colis�o com o jogador
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))  // Verifica se a tag do objeto atingido � "Player"
        {
            PlayerControl player = coll.GetComponent<PlayerControl>();  // Obt�m o componente PlayerControl
            if (player != null)
            {
                GameManager.Instance.OnPlayerKilled(player);  // Chama o m�todo de morte do jogador
            }

            Destroy(coll.gameObject);  // Destroi o jogador
            Destroy(gameObject);  // Destroi o proj�til
        }
    }
}
