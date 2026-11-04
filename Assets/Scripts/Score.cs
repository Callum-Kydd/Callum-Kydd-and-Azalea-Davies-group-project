using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;
    
    void Start()
    {
        score = 0;
        scoreText.text = score.ToString() + " Score";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " Score";
        Debug.Log(score);
    }
}
