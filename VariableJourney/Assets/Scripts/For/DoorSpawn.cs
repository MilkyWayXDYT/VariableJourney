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
    [SerializeField]
    private GameObject lastDoor;

    private bool isLastRoom = false;

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
            forRand.RemoveAt(wrongRandIndex);


            RoomSpawn roomSpawn = GameObject.Find("RoomsSpawn").GetComponent<RoomSpawn>();
            int lastRand = -1;
            if (roomSpawn.lastRoom == roomSpawn.roomsCount)
            {
                isLastRoom = true;
                int lastRandIndex = Random.Range(0, forRand.Count);
                lastRand = forRand[lastRandIndex];
            }

            for (int i = 0; i < pointsForDoorSpawn.Count; i++)
            {
                if (i == rightRand)
                {
                    GameObject newDoor = Instantiate(rightDoorPrefab, pointsForDoorSpawn[i]);
                    if (isLastRoom)
                    {
                        newDoor.GetComponent<InteractiveObj>().isGlitch = true;
                    }
                }
                else if (i == wrongRand)
                    Instantiate(wrongDoorPrefab, pointsForDoorSpawn[i]);
                else if (isLastRoom && lastDoor && i == lastRand)
                    Instantiate(lastDoor, pointsForDoorSpawn[i]);
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
