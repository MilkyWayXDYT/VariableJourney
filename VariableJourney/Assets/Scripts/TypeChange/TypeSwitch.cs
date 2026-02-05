using UnityEngine;
using UnityEngine.InputSystem;

public class TypeSwitch : MonoBehaviour
{
    private GameObject player;

    private PlayerInput playerInput;
    private InputAction nextAction, previousAction, jumpAction, interactionAction, harpoonAction;

    public int typeIndex = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
        nextAction = playerInput.actions.FindAction("Next");
        previousAction = playerInput.actions.FindAction("Previous");

        jumpAction = playerInput.actions.FindAction("Jump");
        interactionAction = playerInput.actions.FindAction("Interact");
        harpoonAction = playerInput.actions.FindAction("HarpoonShot");

        nextAction.performed += NextType;
        previousAction.performed += PrevType;

        jumpAction.Enable();
        interactionAction.Disable();
        harpoonAction.Disable();
    }

    private void NextType(InputAction.CallbackContext context)
    {
        typeIndex++;
        if (typeIndex > 2) typeIndex = 0;
        CheckEnableScript();
    }

    private void PrevType(InputAction.CallbackContext context)
    {
        typeIndex--;
        if (typeIndex < 0) typeIndex = 2;
        CheckEnableScript();

    }

    private void CheckEnableScript()
    {
        switch (typeIndex)
        {
            case 0:
                jumpAction.Enable();
                interactionAction.Disable();
                harpoonAction.Disable();
                break;
            case 1:
                jumpAction.Disable();
                interactionAction.Enable();
                harpoonAction.Disable();
                break;
            case 2:
                jumpAction.Disable();
                interactionAction.Disable();
                harpoonAction.Enable();
                break;
        }
    }
}
