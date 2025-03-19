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
    private bool isDestroyed = false;      // Flag para verificar se a nave foi destruída

    private void Start()
    {
        // Transforma a coordenada da tela em coordenadas do mundo
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero); // Ponto da borda esquerda
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right); // Ponto da borda direita

        // Desloca cada destino em 1 unidade para garantir que a nave saia totalmente da tela
        leftDestination = new Vector2(leftEdge.x - 1f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 1f, transform.position.y);

        Spawn();  // Inicializa a nave
    }

    private void Update()
    {
        if (!spawned || isDestroyed) return;

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

    // Faz a nave aparecer na tela
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

        // Se a nave não foi destruída, reaparece após o ciclo
        if (!isDestroyed)
        {
            Invoke(nameof(Spawn), cycleTime);
        }
    }

    // Detecta colisões com lasers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))  // Verifica se foi atingida por um laser
        {
            isDestroyed = true; // Marca a nave como destruída
            GameManager.Instance.OnMysteryShipKilled(this);  // Notifica o GameManager
            Despawn();  // Desaparece da tela
            Destroy(gameObject); // Destroi a nave
        }
    }
}