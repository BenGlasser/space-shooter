using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject hazard1;
    public GameObject hazard2;
    public GameObject hazard3;
    public Vector3 spawnValues;
    public int hazardcount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    private List<GameObject> hazardList;

    // Use this for initialization
    void Start ()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        hazardList = new List<GameObject> { hazard1, hazard2, hazard3 };

        GameObject scoreObject = GameObject.FindWithTag("score");
        GameObject gameoverObject = GameObject.FindWithTag("gameover");
        GameObject restartObject = GameObject.FindWithTag("restart");
        if (scoreObject != null)
        {
            scoreText = scoreObject.GetComponent<GUIText>();
            gameOverText = gameoverObject.GetComponent<GUIText>();
            restartText = restartObject.GetComponent<GUIText>();
        }
        else
        {
            Debug.Log("Cannot find 'Score Text object' script");
        }
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
	}
	
	// Update is called once per frame
	void Update () {
		if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}

    IEnumerator SpawnWaves()
    {
       yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardcount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazardList[i%3], spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
