using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoorLever : MonoBehaviour
{
    public void OpenDoor()
    {
        Transform room = transform.GetComponentsInParent<Transform>()[4];
        InteractDoor door = room.GetComponentInChildren<InteractDoor>();
        door.DoorOpen();
    }
}
