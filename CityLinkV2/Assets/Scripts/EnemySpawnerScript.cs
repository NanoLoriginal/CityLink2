using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] Transform enemyContainer;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();

    private float spawnRate = 4f;

    GameManager gameManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        

        StartCoroutine(spawnEnemy());
    }

    public void Update()
    {
        if (gameManager.gameOver == true)
        {
            StopAllCoroutines();
            Destroy(enemyContainer);
        }
    }
    IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(spawnRate);
        if (spawnRate > 0.8f)
        {
            spawnRate -= 0.2f;
        }
        GameObject enemyCopy = Instantiate(enemyPrefab, enemyContainer);
        enemyCopy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        StartCoroutine(spawnEnemy());
    }
}
