using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaderr : MonoBehaviour
{
    public GameObject missilePrefab;  // Referência ao prefab do projétil
    public float fireRate = 2f;       // Taxa de disparo (tempo entre os tiros)
    public int score = 10;
    private float lastFireTime;
    private bool canShoot = true;

    private void Start()
    {
        // Inicia o disparo com uma pequena espera
        StartCoroutine(FireMissileCoroutine());
    }

    private void Update()
    {
        // Você pode colocar aqui outras lógicas do Invader, como movimentação, por exemplo
    }

    private IEnumerator FireMissileCoroutine()
    {
        while (true)
        {
            if (canShoot)
            {
                FireMissile();
                canShoot = false;
                // Espera o tempo definido para o próximo disparo
                yield return new WaitForSeconds(fireRate);
                canShoot = true;
            }
            yield return null;  // Espera um quadro
        }
    }

    private void FireMissile()
    {
        // Instancia o projétil do invader
        Instantiate(missilePrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Aqui você pode verificar se o projétil do invader atingiu o jogador
        if (other.CompareTag("Player"))
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (player != null)
            {
                GameManager.Instance.OnPlayerKilled(player);  // Chama o método de morte do jogador
            }

            Destroy(other.gameObject);  // Destroi o jogador
            Destroy(gameObject);        // Destroi o invader
        }
    }
}
