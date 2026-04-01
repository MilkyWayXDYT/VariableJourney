using UnityEngine;

public class InteractDoor : MonoBehaviour
{
    [SerializeField]
    RoomSpawn roomSpawn;

    private bool isOpen = false;

    private void Start()
    {
        roomSpawn = GameObject.Find("RoomsSpawn").GetComponent<RoomSpawn>();
    }

    public void DoorOpen()
    {
        if (!isOpen)
        {
            transform.Rotate(0, 90, 0);
            isOpen = true;

            roomSpawn.Spawn(this.transform);
        }
    }
}
