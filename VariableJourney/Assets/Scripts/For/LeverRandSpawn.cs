using UnityEngine;

public class LeverRandSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject leverPrefab;
    [SerializeField]
    private Transform[] spawnPoints;

    private void Start()
    {
        int randNum = Random.Range(0, spawnPoints.Length);
        Instantiate(leverPrefab, spawnPoints[randNum]);
    }
}
