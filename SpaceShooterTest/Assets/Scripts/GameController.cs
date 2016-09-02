using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] enemyPrefabArray = new GameObject[3];
    public GameObject playerGO;
    public Vector3 spawnValues;
    public int enemyCount;
    public float spawnWait;
    public float waveWait;
    public float startWait;
    private int score;
    private int lives;

    private bool gameOver;
    private bool restart;
    private bool paused;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text endGameText;
    public Text livesText;
    public Text pauseText;

    void Start()
    {
        score = 0;
        lives = 3;
        gameOver = false;
        restart = false;
        restartText.text = "";
        pauseText.text = "";
        endGameText.text = "";
        UpdateScore();
        UpdateLives();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemyPrefabArray[Random.Range(0, enemyPrefabArray.Length)], spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                if (getLives() > 0)
                {
                    restartText.text = "Press 'R' for Restart!";
                    restart = true;
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();                
                //SceneManager.LoadScene("MainScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        if (paused)
        {
            Time.timeScale = 0;
            pauseText.text = "Game Paused\nPress ESC to resume!";
            paused = false;
        }else
        {
            paused = true;
            Time.timeScale = 1;
            pauseText.text = "";
        }
    }

    void Respawn()
    {
        score = 0;
        UpdateScore();
        Instantiate(playerGO, new Vector3(0, 0, 0), Quaternion.identity);
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        StartCoroutine(SpawnWaves());
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void TakeLife(int value)
    {
        setLives(value);
        UpdateLives();
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        if (getLives() > 0)
        {
            gameOverText.text = "You have " + getLives() + " live(s) remaining!";
            gameOver = true;
        }
        if (getLives() <= 0)
        {
            EndGame();
        }

    }

    public void EndGame()
    {
        gameOverText.text = "Game exiting in 3 seconds";
        Application.Quit();
        Debug.Log("QUIT");
    }

    public int getLives()
    {
        return lives;
    }

    public void setLives(int value)
    {
        lives += value;
    }

}
