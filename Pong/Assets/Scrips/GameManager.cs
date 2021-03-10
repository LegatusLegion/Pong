using UnityEngine;
using UnityEngine.UI;
using Pong.Scoreboards;
public class GameManager : MonoBehaviour
{
    public static bool gameRunning;
    public Ball ball;
    public AI ai;
    public Scoreboard scoreboard;
    public int score1;
    public int score2;
    public int difficultyModifier = 2;
    public float increaseBallSpeed = 0.5f;
    

    public Text scoreText1;
    public Text scoreText2;
    public Text difficultyText;
    public Text startScreen;
    

    private void Awake()
    {
        UpdateScoreText();
    }

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
            StartRound();

        //Aufrufen des Menu's
        if (Input.GetKeyDown(KeyCode.Escape))
            Menu();
    }

    private void StartRound()
    {
        if (gameRunning)
            return;
        gameRunning = true;
        ball.InitialStart();
        Destroy(startScreen);
    }

    private void Menu()
    {
        //scoreboard.StartUp();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }


    public void IncreaseScore(bool player)
    {
        if (player)
            score1++;
        else
            score2++;
        UpdateScoreText();
        CheckScore();
    }

    public void ChangeDifficulty()
    {

        if (score1 == 3)
        {
            //Increase Diffuculty
            ai.difficulty += difficultyModifier;
            Debug.Log("Difficulty Increased" + ai.difficulty);
        }
        else
        {
            //Decrease Difficulty, cannot go unter 1
            ai.difficulty -= difficultyModifier;
            if (ai.difficulty < 1)
                ai.difficulty = 1;
        }
            
    }

    public void CheckScore()
    {
        if(score1 == 3 || score2 == 3)
        {
            if (score1 == 3)
                Debug.Log("Player Wins");
            else
                Debug.Log("AI Wins");

            ChangeDifficulty();
            score1 = 0;
            score2 = 0;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText1.text = score1.ToString();
        scoreText2.text = score2.ToString();
        difficultyText.text = "Difficulty: " + ai.difficulty.ToString();
        startScreen.text = "Press Space to start the game!";
    }

}
