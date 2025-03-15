using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaderr : MonoBehaviour
{
    public GameObject missilePrefab;  // Refer�ncia ao prefab do proj�til
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
        // Voc� pode colocar aqui outras l�gicas do Invader, como movimenta��o, por exemplo
    }

    private IEnumerator FireMissileCoroutine()
    {
        while (true)
        {
            if (canShoot)
            {
                FireMissile();
                canShoot = false;
                // Espera o tempo definido para o pr�ximo disparo
                yield return new WaitForSeconds(fireRate);
                canShoot = true;
            }
            yield return null;  // Espera um quadro
        }
    }

    private void FireMissile()
    {
        // Instancia o proj�til do invader
        Instantiate(missilePrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Aqui voc� pode verificar se o proj�til do invader atingiu o jogador
        if (other.CompareTag("Player"))
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (player != null)
            {
                GameManager.Instance.OnPlayerKilled(player);  // Chama o m�todo de morte do jogador
            }

            Destroy(other.gameObject);  // Destroi o jogador
            Destroy(gameObject);        // Destroi o invader
        }
    }
}
