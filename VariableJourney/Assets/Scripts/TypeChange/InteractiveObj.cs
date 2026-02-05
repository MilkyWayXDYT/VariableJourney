using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveObj : MonoBehaviour
{
    private GameObject player;
    private PlayerInput playerInput;
    private InputAction actionInteract;

    public bool objEnable = false;

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
    }

    private void Opening()
    {

    }
}
