using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;
    
    void Start()
    {
        // initialise the score text
        score = 0;
        scoreText.text = score.ToString() + " Score";
    }

    // Update is called once per frame
    void Update()
    {
        // update the score text to reflect current score
        scoreText.text = score.ToString() + " Score";
    }
}
