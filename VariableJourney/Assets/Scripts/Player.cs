using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float acceleration = 50;
    [SerializeField]
    private Transform cameraTransform;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private Rigidbody playerRb;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 action = moveAction.ReadValue<Vector2>();

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 moveDirection = camRight * action.x + camForward * action.y;

        if (moveAction.inProgress)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), 10 * Time.deltaTime);

        playerRb.AddForce(moveDirection.normalized * playerRb.mass * speed * acceleration, ForceMode.Force);

        Vector3 horizontalVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        if (horizontalVelocity.magnitude > speed)
        {
            horizontalVelocity = horizontalVelocity.normalized * speed;
            playerRb.linearVelocity = new Vector3(horizontalVelocity.x, playerRb.linearVelocity.y, horizontalVelocity.z);
        }
    }
}
