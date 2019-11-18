using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text pointsText;
    public Text restartText;
    public Text gameOverText;
    public Text youWinText; 

    private int points;
    private bool restart;
    private bool gameOver;
    private bool youWin;
    private GameObject player;
   // private GameObject enemy;

    void Start()
    {
        StartCoroutine(SpawnWaves());
        points = 0;
        UpdatePoints();
        restart = false;
        gameOver = false;
        youWin = false;
        restartText.text = "";
        gameOverText.text = "";
        youWinText.text = "";
        player = GameObject.FindGameObjectWithTag("Player"); //Reference and modify another scripts variable by its tag
        // enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update() //Reloads scene and exits game
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true) //Makes this loop infinite, spawning infinite asteroids.
        {
            for (int i = 0; i < hazardCount; i++) //Spawn multiple asteroids at once
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            
            if (gameOver) //Restart at Game Over, stops asteroid spawns.
            {
                restartText.text = "Press 'T' for Restart";
                restart = true;
                break;
            }

            if (youWin) //Restart at Win, stops asteroid spawns.
            {
                restartText.text = "Press 'T' for Restart"; 
                restart = true;
                break;
            }
        }
    }

    public void AddPoints(int newPointsValue) //Adds score
    {
        points += newPointsValue;
        UpdatePoints();
    }

    void UpdatePoints() //Displays current score, sets win condiiton, and win text
    {
        pointsText.text = "Points: " + points;
        if (points >= 100 && gameOver != true) 
        {
            // player.GetComponent<PlayerController>().playerSpeed = 0; //Modifies player movement to zero
            youWinText.text = "You Win! Game Created by Giovanni Minus :)"; 
            youWin = true; 
        }

        if (gameOver == true) //Makes it so you cannot both lose and win.
        {
            youWinText.text = "";
            youWin = false;
        }
    }

    public void GameOver () //Displays game over text when player dies
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
