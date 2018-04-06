using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    private int score = 0;
    private float lastUpdate;
    private int finalScore = 0;
    private bool scoreCountUp = false;

    public static Score instance = null;

    [SerializeField] private int scorePerTick;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private int coinScore;


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
    }

    // Use this for initialization
    void Start ()
    {
        lastUpdate = Time.time;
    }

	// Update is called once per frame
	void Update ()
    {
        ScoreKeeper();
        scoreText.text = "Score: " + score.ToString("N0");
        finalScoreText.text = "Final Score:\n" + finalScore.ToString("N0");
    }

    // Called by GameManager to reset score
    public void ResetScore()
    {
        score = 0;
    }

    // Called by GameManager to start tracking score on new game
    public void StartScore()
    {
        scoreCountUp = true;
    }

    // increases score every second
    public void ScoreKeeper()
    {
        if (Time.time - lastUpdate >= 1f && scoreCountUp)
        {
            score += scorePerTick;
            lastUpdate = Time.time;
            finalScore = score;
        }
    }

    public void AddCoinScore()
    {
        score = score + coinScore;
    }

    public void DisableScoreCountUp()
    {
        scoreCountUp = false;
    }
}
