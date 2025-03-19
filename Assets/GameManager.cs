using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private PlayerControl player;
    private int score = 0;
    private int lives = 3;
    private int totalInvaders; // Contador de invasores restantes

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (livesText == null)
        {
            Debug.LogError("LivesText is not assigned in the Inspector!");
        }
    }

    [System.Obsolete]
    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();

        if (player == null)
        {
            Debug.LogError("PlayerControl not found!");
        }

        // Conta quantos invasores existem no início do jogo
        totalInvaders = FindObjectsOfType<Invaderr>().Length;

        // Altera a cor de todos os invasores para branco
        foreach (Invaderr invader in FindObjectsOfType<Invaderr>())
        {
            SpriteRenderer spriteRenderer = invader.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }
        }

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

        // Verifica se o score atingiu 1800 pontos
        if (score >= 800)
        {
            Victory();
        }
    }

    private void NewGame()
    {
        gameOverUI.SetActive(false);
        score = 0;
        lives = 3;

        if (livesText != null)
        {
            livesText.text = "Lives: " + lives.ToString();
        }

        UpdateScoreUI();
        Respawn();
    }

    private void Respawn()
    {
        if (player != null)
        {
            player.gameObject.SetActive(true);
            player.transform.position = new Vector3(0f, player.transform.position.y, player.transform.position.z);
        }
    }

    private IEnumerator RespawnWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Respawn();
    }

    // Método GameOver agora é público
    public void GameOver()
    {
        // Salva o score final
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.Save();

        // Carrega a cena de game over
        SceneManager.LoadScene("GameOverScene");
    }

    private void Victory()
    {
        // Salva o score final
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.Save();

        // Carrega a cena de vitória
        SceneManager.LoadScene("VictoryScene");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString().PadLeft(4, '0');
        }
        else
        {
            Debug.LogWarning("scoreText is not assigned in the Inspector!");
        }
    }

    public void OnInvaderKilled(Invaderr invader)
    {
        score += invader.score;
        UpdateScoreUI();
        Destroy(invader.gameObject);

        // Verifica se todos os invasores foram destruídos
        totalInvaders--;
        if (totalInvaders <= 0 || score >= 1800)
        {
            Victory(); // Chama a tela de vitória
        }
    }

    public void OnPlayerKilled(PlayerControl player)
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver(); // Chama a tela de derrota
        }
        else
        {
            StartCoroutine(RespawnWithDelay(2f)); // Respawn após 2 segundos
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        score += mysteryShip.score;
        UpdateScoreUI();
    }
}