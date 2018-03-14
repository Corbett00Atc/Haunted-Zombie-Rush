using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    private int score = 0;
    private float lastUpdate;
    private int finalScore = 0;

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
        ScoreKeeper();
    }

    // increases score every second
    public void ScoreKeeper()
    {
        if (Time.time - lastUpdate >= 1f)
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
}
