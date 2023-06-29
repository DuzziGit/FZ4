using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{

    public float timeLeft = 300.0f; // 5 minutes
    public TextMeshProUGUI timerText;
    public int maxEnemies = 10;
    public int playerLevel = 1;

    private int currentEnemies = 0;
    void Start()
    {
        StartCoroutine(UpdateTimer());
    }

    void Update()
    {
        if (currentEnemies < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Your enemy spawning logic here
        // ...
        currentEnemies++;
    }

    IEnumerator UpdateTimer()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            //timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        EndGame();
    }

    void EndGame()
    {
        // Your end game logic here
        // ...
    }
}