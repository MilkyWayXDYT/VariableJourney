using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Interaction : MonoBehaviour
{
    public InteractiveObj interactiveObj;

    private PlayerInput playerInput;
    private InputAction actionInteract;

    private void Start()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        actionInteract = playerInput.actions.FindAction("Interact");
        actionInteract.performed += Interacting;
    }

    private void Interacting(InputAction.CallbackContext context)
    {
        if (context.performed && interactiveObj)
            interactiveObj.Switch();
    }
}
