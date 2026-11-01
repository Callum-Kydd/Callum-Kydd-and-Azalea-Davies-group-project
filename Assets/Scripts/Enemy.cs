using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement player_script;
    public int value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            Destroy(gameObject);
            player_script.score += value;
        }
    }
}
