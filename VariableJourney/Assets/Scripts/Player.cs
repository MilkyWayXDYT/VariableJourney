using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpVelocity;
    [SerializeField]
    private float jumpDistance = 1.5f;

    private PlayerInput playerInput;
    private InputAction moveAction, jumpAction;
    private Rigidbody playerRb;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        jumpAction = playerInput.actions.FindAction("Jump");
        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime * speed;
        
        Debug.DrawRay(transform.position, Vector2.down * jumpDistance, Color.red);

        if (jumpAction.triggered && InGround())
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
