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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();

        if (player == null)
        {
            Debug.LogError("PlayerControl not found!");
        }

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        gameOverUI.SetActive(false);
        score = 0;
        lives = 3;
        UpdateScoreUI();
        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        //gameOverUI.SetActive(true);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString().PadLeft(4, '0');
        }
    }

    public void OnInvaderKilled(Invaderr invader)
    {
        score += invader.score; // Adiciona a pontuação correta do inimigo
        UpdateScoreUI();
        Destroy(invader.gameObject);
    }

    public void OnPlayerKilled(PlayerControl player)
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        score += mysteryShip.score;  // Adiciona a pontuação da nave mãe
        UpdateScoreUI();  // Atualiza o UI da pontuação
    }

}
