using UnityEngine;

public class InteractDoor : MonoBehaviour
{
    [SerializeField]
    RoomSpawn roomSpawn;

    private bool isOpen = false;

    private bool timerStart = false;
    private float timer = 5;

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

            if (this.gameObject.name == "EndDoor(Clone)")
            {
                timerStart = true;
            }
        }
    }

    private void Update()
    {
        if (timerStart)
            timer -= Time.deltaTime;

        if (timer < 0)
            Debug.Log("End game");
    }
}
