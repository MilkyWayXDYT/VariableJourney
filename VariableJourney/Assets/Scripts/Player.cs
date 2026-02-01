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

        direction = new Vector3(action.x, 0, action.y);
        direction = Camera.main.transform.TransformDirection(direction);
        direction = new Vector3(direction.x, 0, direction.z);

        if (moveAction.inProgress)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);

        playerRb.AddForce(direction.normalized * playerRb.mass * speed * acceleration);

        if (Mathf.Abs(playerRb.linearVelocity.x) > speed)
            playerRb.linearVelocity = new Vector3(Mathf.Sign(playerRb.linearVelocity.x) * speed, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        if (Mathf.Abs(playerRb.linearVelocity.z) > speed)
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, playerRb.linearVelocity.y, Mathf.Sign(playerRb.linearVelocity.z) * speed);
        //transform.position += new Vector3(action.x, 0, action.y) * Time.deltaTime * speed;

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
