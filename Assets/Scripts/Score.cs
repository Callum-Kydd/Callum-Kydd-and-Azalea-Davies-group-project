using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public int score = 0;
    
    void Start()
    {
        scoreText.text = score.ToString() + " Score";
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString() + " Score";
    }
}
