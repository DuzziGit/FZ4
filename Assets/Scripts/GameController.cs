using System.Collections;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public static GameController instance; // Singleton instance

    public float timeLeft = 300.0f; // 5 minutes
    public TextMeshProUGUI timerText;
    public int maxEnemies = 10;
    public int playerLevel = 1;
    public GameObject gameControlsUi;
    private int currentEnemies = 0;
    public GameObject RoguePrefab;
    public CinemachineVirtualCamera cinemachineCam;

    // Static flag to check if Rogue has been instantiated
    private static bool rogueInstantiated = false;

void Awake()
{
    // Singleton check
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else if (instance != this)
    {
        Destroy(gameObject);
        return; // Prevent the rest of the Awake method from running
    }

    gameControlsUi.SetActive(true);
    StartCoroutine(UpdateTimer());

    // Check if Rogue exists in the scene
if (GameObject.FindObjectOfType<RogueSkillController>() == null)
    {
        GameObject rogueInstance = Instantiate(RoguePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        cinemachineCam.Follow = rogueInstance.transform;
        cinemachineCam.m_Lens.FieldOfView = 60f;
    }
}

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
       
    }
}
