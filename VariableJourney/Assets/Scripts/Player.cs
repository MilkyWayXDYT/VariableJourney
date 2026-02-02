using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float acceleration = 50;
    [SerializeField]
    private float jumpVelocity;
    [SerializeField]
    private float jumpDistance = 1.5f;
    [SerializeField]
    private Transform cameraTransform;

    private PlayerInput playerInput;
    private InputAction moveAction, jumpAction;
    private Rigidbody playerRb;

    private Vector3 direction;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");
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

        Debug.DrawRay(transform.position, Vector2.down * jumpDistance, Color.red);

        if (jumpAction.inProgress && InGround())
            playerRb.AddForce(0, jumpVelocity, 0);
    }

    private bool InGround()
    {
        bool result = false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, jumpDistance))
            result = true;

        return result;
    }
}
