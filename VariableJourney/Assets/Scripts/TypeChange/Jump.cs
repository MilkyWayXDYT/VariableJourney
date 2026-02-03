using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField]
    private float jumpVelocity;
    [SerializeField]
    private float jumpDistance = 1.5f;

    private PlayerInput playerInput;
    private InputAction jumpAction;
    private Rigidbody playerRb;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions.FindAction("Jump");
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
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
