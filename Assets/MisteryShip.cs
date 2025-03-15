using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MysteryShip : MonoBehaviour
{
    public float speed = 5f;               // Velocidade de movimento da nave
    public float cycleTime = 30f;          // Tempo entre as aparições da nave
    public int score = 300;                // Pontuação ao destruir a nave

    private Vector2 leftDestination;
    private Vector2 rightDestination;
    private int direction = -1;
    private bool spawned;

    private void Start()
    {
        // Transforma a coordenada da tela em coordenadas do mundo
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); // Ponto da borda esquerda
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right); // Ponto da borda direita

        // Desloca cada destino em 1 unidade para garantir que a nave saia totalmente da tela
        leftDestination = new Vector2(leftEdge.x - 1f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 1f, transform.position.y);

        Despawn();  // Inicializa a nave como desaparecida
    }

    private void Update()
    {
        if (!spawned) return;

        // Movimenta a nave para a direita ou para a esquerda dependendo da direção
        if (direction == 1)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
    }

    // Movimenta a nave para a direita
    private void MoveRight()
    {
        transform.position += speed * Time.deltaTime * Vector3.right;

        // Se atingir o limite direito, desaparece
        if (transform.position.x >= rightDestination.x)
        {
            Despawn();
        }
    }

    // Movimenta a nave para a esquerda
    private void MoveLeft()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        // Se atingir o limite esquerdo, desaparece
        if (transform.position.x <= leftDestination.x)
        {
            Despawn();
        }
    }

    // Faz a nave mãe aparecer na tela
    private void Spawn()
    {
        direction *= -1;

        if (direction == 1)
        {
            transform.position = leftDestination;
        }
        else
        {
            transform.position = rightDestination;
        }

        spawned = true; // Marca a nave como aparecida
    }

    // Faz a nave desaparecer da tela
    private void Despawn()
    {
        spawned = false;

        if (direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }

        // Chama o método Spawn para fazer a nave reaparecer após o ciclo
        Invoke(nameof(Spawn), cycleTime);
    }

    // Detecta colisões com lasers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))  // Verifica se foi atingida por um laser
        {
            Despawn();  // Desaparece da tela
            GameManager.Instance.OnMysteryShipKilled(this);  // Chama o método para atualizar a pontuação
        }
    }
}
