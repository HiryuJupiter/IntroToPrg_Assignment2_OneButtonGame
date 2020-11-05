using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner instance;

    const float IntervalMax = 1.5f;
    const float IntervalMin = 0.1f;
    const float IntervalDecrement = 0.02f;

    [SerializeField] GameObject prefab;
    [SerializeField] float spawnBoundX = 10f;
    [SerializeField] float spawnBoundY = 10f;

    //Status
    float spawnInterval = IntervalMax;
    bool spawning;

    #region MonoBehavior
    void Awake()
    {
        instance = this;
    }
    #endregion

    #region Public
    public void StartSpawning()
    {
        StartCoroutine(DoSpawnSequence());
    }

    public void StopSpawning()
    {
        spawning = false;
    }
    #endregion

    #region Private
    IEnumerator DoSpawnSequence()
    {
        //This is the coroutine exit condition, set it to true.
        spawning = true;

        //Wait a moment before start spawning targets.
        yield return new WaitForSeconds(spawnInterval);

        //Endless loop to spawn targets at a certain interval
        while (spawning)
        {
            //Decrement the interval to make spawn speed faster overtime.
            spawnInterval = Mathf.Clamp(
                spawnInterval - IntervalDecrement,
                IntervalMin,
                IntervalMax);

            SpawnTarget();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTarget()
    {
        //Instantiate prefab at a random location.
        Instantiate(prefab, GetRandomSpawnPosition(), Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        //Spawn the target within the bounds specified by the player.
        return new Vector3(
            Random.Range(-spawnBoundX, spawnBoundX),
            Random.Range(-spawnBoundY, spawnBoundY),
            0f);
    }
    #endregion

}
