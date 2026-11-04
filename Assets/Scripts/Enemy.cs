using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int value;
    public Score scoreScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
