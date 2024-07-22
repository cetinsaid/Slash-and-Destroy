using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private bool gameIsPaused = false;
    public int lives = 4;
    public List<GameObject> objects;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public bool gameOver;
    public Button restartButton;
    [SerializeField] private Button[] difficultyButtons;
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private Slider slider;
    public TextMeshProUGUI pauseText;

    public float coolDown;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) && !gameOver)
        {
            PauseGame();
        }


    }

    IEnumerator Test(int diff)
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(coolDown/diff);
            Spawn();
            
        }
    }

    public void Spawn()
    {
        int index = Random.Range(0, objects.Count);
        Instantiate(objects[index]);
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void PauseGame()
    {
        if (gameIsPaused)
        {
            pauseText.gameObject.SetActive(false);
            gameIsPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            pauseText.gameObject.SetActive(true);
            gameIsPaused = true;
            Time.timeScale = 0;
        }

    }

    public void changeScore(int amount)
    {
        score += amount;
        if (score < 0)
        {
            lives--;
            if (lives <= 0)
            {
                GameOver();
            }
        }
        scoreText.text = ("Score: " + score);
    }

    public void RestartGame()
    {
        Debug.Log("pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int diff)
    {
        pauseText.gameObject.SetActive(false);
        slider.gameObject.SetActive(false);
        Time.timeScale = 1;
        startText.gameObject.SetActive(false);
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            difficultyButtons[i].gameObject.SetActive(false);
        }
        gameOver = false;
        score = 0;
        changeScore(0);
        decrementLives();
        StartCoroutine(Test(diff));
        
    }

    public void decrementLives()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
        
    }

   
}
