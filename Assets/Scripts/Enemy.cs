using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement player_script;
    public int value;
    public Score scoreScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreScript = FindAnyObjectByType<Score>();
        player_script = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            scoreScript.score += value;
            Destroy(gameObject);
            



        }
    }
}
