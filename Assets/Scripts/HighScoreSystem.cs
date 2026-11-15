using UnityEngine;
using TMPro;

public static class Highscore
{
    public static int highscore; //static class and int allows us to use the highscore variable accross different scenes.
}

public class HighScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    

    void Start()
    {
        // initialise the score text
        
        scoreText.text = Highscore.highscore.ToString() + " Highscore";


    }

    // Update is called once per frame
    void Update()
    {
        // update the score text to reflect current score
        scoreText.text = Highscore.highscore.ToString() + " Highscore";


    }
}