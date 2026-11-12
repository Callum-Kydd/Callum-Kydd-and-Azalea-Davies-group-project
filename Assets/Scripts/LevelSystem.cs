using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : MonoBehaviour
{
    public int shotsFired = 0;
    public int towerPiecesLeft;
    public int spawnDelay;
    private int spawnTimer;
    public int failTime;
    public int failTimer;
    public bool incrementFailTimer;
    public int level = 1;
    

    [Header("Tower Spawning")]
    public GameObject towerPrefab;
    public Transform towerSpawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTimer = 0;
        incrementFailTimer = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        // a delay is used to ensure everything has happened that needs to before a new tower is spawned
        if (towerPiecesLeft <= 1 && spawnTimer < spawnDelay)
        {
            Debug.Log("tower destroyed");
            spawnTimer++;
        }
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0;
            RespawnTower();
            Debug.Log("The next tower should be spawned now"); // [CODE TO SPAWN THE NEXT TOWER GOES HERE]
        }

        // increment the fail timer if player is about to fail
        if (towerPiecesLeft > 1 && shotsFired == 3 && failTimer < failTime && incrementFailTimer)
        {
            failTimer++;
        }

        // if the fail timer finishes
        if (failTimer >= failTime)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadSceneAsync(0);
            Debug.Log("The player should go to the end screen now"); // [CODE TO GO TO THE END SCREEN GOES HERE]
        }

        void RespawnTower()
        {
            // reset state BEFORE instantiating, because each new Enemy adds itself to towerPiecesLeft in Start()
            level++;
            shotsFired = 0;
            towerPiecesLeft = 0;
            failTimer = 0;
            incrementFailTimer = false;

            Instantiate(towerPrefab, towerSpawnPoint.position, towerSpawnPoint.rotation);
        }
    }
}
