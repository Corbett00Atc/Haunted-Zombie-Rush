using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] Vector3 playerStart;
    [SerializeField] Vector3[] rockStartLocations;
    [SerializeField] GameObject inGameScoreUI;
    [SerializeField] GameObject endGameScoreUI;
    [SerializeField] GameObject instructions;
    [SerializeField] PlayerHealthBar energyBar;

    private Score scoreKeeper;
    private GameObject[] rocks;
    private bool playerActive = false;
    private bool gameOver = false;
    private bool gameStarted = false;

    public bool PlayerActive
    {
        get { return playerActive; }
    }

    public bool GameOver
    {
        get { return gameOver; }
    }

    public bool GameStarted
    {
        get { return gameStarted; }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // Doesn't destroy on new scene
        DontDestroyOnLoad(gameObject);

        Assert.IsNotNull(mainMenu);

        scoreKeeper = GetComponent<Score>();
    }

    public void GameIsOver()
    {
        gameOver = true;
        StartCoroutine(RestartDelay());
        Coin.instance.Stop();
        energyBar.Stop();
        scoreKeeper.DisableScoreCountUp();
    } 

    // triggers once player starts moving
    public void PlayerStartedGame()
    {
        playerActive = true;
        Score.instance.StartScore();
        Coin.instance.Spawn();
        energyBar.GameStart();
        scoreKeeper.StartScore();
    }

    public void EnterGame()
    {
        inGameScoreUI.SetActive(true);
        mainMenu.SetActive(false);
        gameStarted = true;
        instructions.SetActive(true);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


    // adds a delay for the zombie to die before game over appears
    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(1.5f);

        inGameScoreUI.SetActive(false);
        gameOverMenu.SetActive(true);
        endGameScoreUI.SetActive(true);
    }

    public void ResetGame()
    {
        gameOverMenu.SetActive(false);
        gameOver = false;
        playerActive = false;
        inGameScoreUI.SetActive(true);
        Score.instance.ResetScore();
        endGameScoreUI.SetActive(false);

        ResetRocks();
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        // accesses the player object
        GameObject player = GameObject.FindWithTag("player");

        // removes the force and rotational forces from player
        // this prepares the object for the next step
        player.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().detectCollisions = true;
        player.GetComponent<Rigidbody>().freezeRotation = true;

        // sets the player to the start position
        player.transform.eulerAngles = new Vector3(3.553f, -60.571f, 2.227f);
        player.transform.position = new Vector3(-0.054243f, 9.9984f, -12.89f);
    }

    private void ResetRocks()
    {
        // create array of the rock objects
        rocks = GameObject.FindGameObjectsWithTag("obstacle");

        // loops through assigning rocks[i] to rockStartLocations[i]
        for (int i = 0; i < 3; i++)
        {
            rocks[i].GetComponent<Rigidbody>().transform.position = rockStartLocations[i];
        }
    }

    public void MainMenuLoad()
    {
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(true);
        inGameScoreUI.SetActive(false);
        endGameScoreUI.SetActive(false);
        instructions.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ToggleInstructions()
    {
        instructions.SetActive(instructions.activeSelf ? false : true);
        print(instructions.activeSelf ? false : true);
    }

}
