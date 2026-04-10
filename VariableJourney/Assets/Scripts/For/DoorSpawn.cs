using System.Collections.Generic;
using TMPro;
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
        RoomSpawn roomSpawn = GameObject.Find("RoomsSpawn").GetComponent<RoomSpawn>();
        if (pointsForDoorSpawn != null)
        {
            List<int> forRand = new List<int> { 0, 1, 2, 3, 4 };

            int rightRandIndex = Random.Range(0, forRand.Count);
            int rightRand = forRand[rightRandIndex];
            forRand.RemoveAt(rightRandIndex);

            int wrongRandIndex = Random.Range(0, forRand.Count);
            int wrongRand = forRand[wrongRandIndex];
            forRand.RemoveAt(wrongRandIndex);


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
                    TMP_Text doorNum = newDoor.GetComponentInChildren<TMP_Text>();
                    doorNum.text = (roomSpawn.lastRoom + 1).ToString();
                    if (isLastRoom)
                    {
                        newDoor.GetComponent<InteractiveObj>().isGlitch = true;
                    }
                }
                else if (i == wrongRand)
                {
                    GameObject newDoor = Instantiate(wrongDoorPrefab, pointsForDoorSpawn[i]);
                    TMP_Text doorNum = newDoor.GetComponentInChildren<TMP_Text>();
                    doorNum.text = roomSpawn.lastRoom.ToString();
                }
                else if (isLastRoom && lastDoor && i == lastRand)
                {
                    GameObject newDoor = Instantiate(lastDoor, pointsForDoorSpawn[i]);
                    TMP_Text doorNum = newDoor.GetComponentInChildren<TMP_Text>();
                    doorNum.text = "End";
                    //doorNum.text = roomSpawn.lastRoom.ToString();
                }
                else
                    Instantiate(simpleDoorPrefab, pointsForDoorSpawn[i]);
            }
        }

        if (endDoorSpawn != null)
        {
            GameObject newDoor = Instantiate(rightDoorPrefab, endDoorSpawn);
            TMP_Text doorNum = newDoor.GetComponentInChildren<TMP_Text>();
            doorNum.text = (roomSpawn.lastRoom + 1).ToString();
        }
    }
}
