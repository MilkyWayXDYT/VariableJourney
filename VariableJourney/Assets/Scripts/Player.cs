using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float acceleration = 50;
    [SerializeField]
    private Transform cameraTransform;

    public float maxDistance;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction camResetAction;
    private Rigidbody playerRb;
    private Quaternion targetRotation;

    private LineController lineController;

    private bool forceCameraRotation = false;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        camResetAction = playerInput.actions.FindAction("CameraReset");
        playerRb = GetComponent<Rigidbody>();

        camResetAction.performed += PlayerRotReset;

        if (SceneManager.GetActiveScene().name == "TypingLevel")
            lineController = GameObject.Find("Start").GetComponent<LineController>();
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
            targetRotation = Quaternion.LookRotation(moveDirection);

        playerRb.AddForce(moveDirection.normalized * playerRb.mass * speed * acceleration, ForceMode.Force);

        Vector3 horizontalVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);
        if (horizontalVelocity.magnitude > speed)
        {
            horizontalVelocity = horizontalVelocity.normalized * speed;
            playerRb.linearVelocity = new Vector3(horizontalVelocity.x, playerRb.linearVelocity.y, horizontalVelocity.z);
        }

        if (lineController && lineController.points.Count > 0)
            PlayerMoveBlock();

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            10 * Time.deltaTime
        );
    }

    private void PlayerMoveBlock()
    {
        float defaultDistance = 7f;

        if (defaultDistance > maxDistance)
            maxDistance = defaultDistance;

        Vector3 offset = transform.position - lineController.points[1];

        if (offset.magnitude > maxDistance)
        {
            playerRb.MovePosition(lineController.points[1] + offset.normalized * maxDistance);
        }
    }

    private void PlayerRotReset(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 forward = cameraTransform.forward;
            forward.y = 0;

            targetRotation = Quaternion.LookRotation(forward);
        }
    }
}
