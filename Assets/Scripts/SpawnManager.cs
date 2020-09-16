using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;
    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(SpawnRoutine());
    }

    //Type IEnumerator lets us to yield events
    IEnumerator SpawnRoutine()
    {
        while(!stopSpawning)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-8f, 8f),7,0),Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void PlayerIsDead()
    {
        stopSpawning = true;
    }
}
