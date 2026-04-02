using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomSpawn : MonoBehaviour
{
    public int roomsCount = 15;

    [SerializeField]
    private GameObject simpleRoom;
    [SerializeField]
    private GameObject hallway;
    [SerializeField]
    private GameObject randomLeverRoom;
    [SerializeField]
    private GameObject puzzleRoom;
    [SerializeField]
    private GameObject startRoom;
    [SerializeField]
    private GameObject simpleDoor;

    private List<GameObject> rooms;
    private int simpleRoomCount, hallwayCount, randomLeverRoomCount, puzzleRoomCount;

    private int lastRoom = 1;
    private List<GameObject> currentRooms;

    System.Random rand = new System.Random();

    private void Start()
    {
        if (roomsCount == 15)
        {
            simpleRoomCount = 4;
            puzzleRoomCount = 4;
            hallwayCount = 2;
            randomLeverRoomCount = 3;
        }
        else if (roomsCount == 23)
        {
            simpleRoomCount = 6;
            puzzleRoomCount = 6;
            hallwayCount = 4;
            randomLeverRoomCount = 5;
        }
        else if (roomsCount == 27)
        {
            simpleRoomCount = 7;
            puzzleRoomCount = 7;
            hallwayCount = 5;
            randomLeverRoomCount = 6;
        }
        else if (roomsCount == 29)
        {
            simpleRoomCount = 8;
            puzzleRoomCount = 7;
            hallwayCount = 5;
            randomLeverRoomCount = 7;
        }
        else if (roomsCount == 30)
        {
            simpleRoomCount = 8;
            puzzleRoomCount = 8;
            hallwayCount = 5;
            randomLeverRoomCount = 7;
        }
        rooms = new List<GameObject>();
        currentRooms = new List<GameObject>();

        RoomsFilling();
        RoomRandomizing();
        currentRooms.Add(startRoom);
    }

    private void RoomsFilling()
    {
        for (int i = 0; i < simpleRoomCount; i++)
            rooms.Add(simpleRoom);
        for (int i = 0; i < hallwayCount; i++)
            rooms.Add(hallway);
        for (int i = 0; i < puzzleRoomCount; i++)
            rooms.Add(puzzleRoom);
        for (int i = 0; i < randomLeverRoomCount; i++)
            rooms.Add(randomLeverRoom);
    }

    private void RoomRandomizing()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            int j = rand.Next(rooms.Count);
            var temp = rooms[j];
            rooms[j] = rooms[i];
            rooms[i] = temp;
        }
    }

    public void Spawn(Transform door)
    {
        //currentRooms.Add(rooms[lastRoom - 1]);
        Transform spawnPoint = door.parent;
        lastRoom++;
        GameObject newRoom = Instantiate(rooms[lastRoom - 1], spawnPoint.position, spawnPoint.rotation);
        currentRooms.Add(newRoom);

        if (currentRooms.Count > 2)
        {
            Transform firstDoorPoint = currentRooms[1].GetComponentsInChildren<Transform>().FirstOrDefault(p => p.name == "DoorStart");
            GameObject blockDoor = Instantiate(simpleDoor, firstDoorPoint);
            RoomDelete();
        }
    }

    public void RoomDelete()
    {
        Destroy(currentRooms[0]);
        currentRooms.RemoveAt(0);
    }
}
