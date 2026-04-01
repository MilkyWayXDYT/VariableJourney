using System.Collections.Generic;
using UnityEngine;

public class DoorSpawn : MonoBehaviour
{
    [SerializeField]
    private List<Transform> pointsForDoorSpawn;
    [SerializeField]
    private Transform endDoorSpawn;
    [SerializeField]
    private GameObject rightDoorPrefab;
    [SerializeField]
    private GameObject wrongDoorPrefab;
    [SerializeField]
    private GameObject simpleDoorPrefab;

    private void Start()
    {
        if (pointsForDoorSpawn != null)
        {
            List<int> forRand = new List<int> { 0, 1, 2, 3, 4 };
            int rightRandIndex = Random.Range(0, forRand.Count);
            int rightRand = forRand[rightRandIndex];
            forRand.RemoveAt(rightRandIndex);
            int wrongRandIndex = Random.Range(0, forRand.Count);
            int wrongRand = forRand[wrongRandIndex];
            

            for (int i = 0; i < pointsForDoorSpawn.Count; i++)
            {
                if (i == rightRand)
                    Instantiate(rightDoorPrefab, pointsForDoorSpawn[i]);
                else if (i == wrongRand)
                    Instantiate(wrongDoorPrefab, pointsForDoorSpawn[i]);
                else
                    Instantiate(simpleDoorPrefab, pointsForDoorSpawn[i]);
            }
        }

        if (endDoorSpawn != null)
        {
            Instantiate(rightDoorPrefab, endDoorSpawn);
        }
    }
}
