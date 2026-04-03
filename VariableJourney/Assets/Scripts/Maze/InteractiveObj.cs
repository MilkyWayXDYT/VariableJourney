using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveObj : MonoBehaviour
{
    [SerializeField] private Door[] doors;
    [SerializeField] private Platform[] platforms;

    private GameObject player;
    private PlayerInput playerInput;
    private InputAction actionInteract;

    public bool objEnable = false;
    public bool isGlitch = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        actionInteract = playerInput.actions.FindAction("Interact");
    }

    private bool IndexCheck()
    {
        return player.GetComponent<TypeSwitch>().typeIndex == 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IndexCheck())
        {
            actionInteract.Enable();
            other.GetComponent<Interaction>().interactiveObj = this;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && IndexCheck())
        {
            actionInteract.Enable();
            other.GetComponent<Interaction>().interactiveObj = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && IndexCheck())
        {
            actionInteract.Disable();
            other.GetComponent<Interaction>().interactiveObj = null;
        }
    }

    public void Switch()
    {
        objEnable = !objEnable;
        if (gameObject.tag == "Lever" && this)
            Levering();
        else if (gameObject.tag == "Door" && this)
            Opening();
    }

    private void Levering()
    {
        float rotate = objEnable ? 20 : -20;
        transform.Find("GameObject").localRotation = Quaternion.Euler(0, 0, rotate);
        OpenDoorLever openDoor = GetComponent<OpenDoorLever>();
        if (doors.Length > 0)
        {
            foreach (Door door in doors)
            {
                door.DoorAction();
            }
        }
        else if (platforms.Length > 0)
        {
            foreach (Platform platform in platforms)
            {
                platform.PlatformMove();
            }
        }
        else  if (openDoor)
        {
            openDoor.OpenDoor();
        }
        Transform puzzleRoom = GetComponentsInParent<Transform>().FirstOrDefault(r => r.name == "ThePuzzleRoom(Clone)");
        if (puzzleRoom)
        {
            var createDoor = puzzleRoom.GetComponent<CreateDoorFromLevers>();
            if (objEnable)
                createDoor.enableLevers++;
            else
                createDoor.enableLevers--;
            createDoor.LeverPress();
        }
    }

    private void Opening()
    {
        var roomsDoor = GetComponent<InteractDoor>();
        roomsDoor.DoorOpen();
    }
}
