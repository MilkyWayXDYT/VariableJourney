using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Interaction : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction actionInteract;

    private bool leverEnable = false;

    private void Start()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        actionInteract = playerInput.actions.FindAction("Interact");
    }

    public void OnTriggerEnter()
    {
        actionInteract.performed += Interacting;
    }

    public void OnTriggerExit()
    {
        actionInteract.performed -= Interacting;
    }

    private void Interacting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact");
            if (gameObject.tag == "Lever")
                Levering();
            else if (gameObject.tag == "Door")
                Opening();
        }

    }

    private void Levering()
    {
        leverEnable = !leverEnable;
        float rotate = leverEnable ? 20 : -20;
        transform.Find("GameObject").localRotation = Quaternion.Euler(0, 0, rotate);
    }

    private void Opening()
    {

    }
}
