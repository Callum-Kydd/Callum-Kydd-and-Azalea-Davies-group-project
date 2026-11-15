using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy : MonoBehaviour
{
    public int value;
    public ScoreSystem scoreScript;
    public HighScoreSystem highscoreScript;
    public LevelSystem levelScript;
    private bool excludedFromTower;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //prefabs cant hold references, so I find them here at runtime 
        if (levelScript == null) levelScript = FindFirstObjectByType<LevelSystem>();
        if (scoreScript == null) scoreScript = FindFirstObjectByType<ScoreSystem>();
        

        levelScript.towerPiecesLeft++;
    }

    void Update()
    {
        // condition to destroy remaining tower pieces in preparation for spawning the next tower
        if (levelScript.shotsFired == 3 && levelScript.towerPiecesLeft <= 1) // 1 instead of 0 just for a little bit more leniency
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // destroy the piece if it touches the ground, and remove it from the tower piece count, unless it is already excluded from the tower
        if (collision.gameObject.CompareTag("Ground"))
        {
            scoreScript.score += value;
            if (scoreScript.score > Highscore.highscore)
            {
                Highscore.highscore = scoreScript.score;
            }
                
            Destroy(gameObject);
            if (!excludedFromTower)
            {
                levelScript.towerPiecesLeft--;
                levelScript.failTimer = 0;
            }
        }

        // exclude the piece from the tower if it touches the platform, and remove it from the tower piece count, but don't destroy it
        if (collision.gameObject.CompareTag("Enemy Platform"))
        {
            if (!excludedFromTower)
            {
                levelScript.towerPiecesLeft--;
                excludedFromTower = true;
                levelScript.failTimer = 0;
            }
        }
    }
}
