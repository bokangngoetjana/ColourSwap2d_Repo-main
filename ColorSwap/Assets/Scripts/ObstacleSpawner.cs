using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public float spawnInterval = 2f;
    private float timer = 0;

    private string[] hexColors = { "#75D5FD", "#B76CFD", "#FF2281", "#011FFD" };

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }
    }
    void SpawnObstacle()
    {
        float randomX = Random.Range(-2f, 2f);
        Vector3 spawnPosition = new Vector3(randomX, 6, 0);

        GameObject selectedObstacle = obstaclePrefab[Random.Range(0, obstaclePrefab.Length)];
        GameObject newObstacle = Instantiate(selectedObstacle, spawnPosition, Quaternion.identity);

        SpriteRenderer sr = newObstacle.GetComponent<SpriteRenderer>();
        if(sr != null )
        {
            Color color;
            ColorUtility.TryParseHtmlString(hexColors[Random.Range(0, hexColors.Length)], out color);
            sr.color = color;
        }
    }
}
