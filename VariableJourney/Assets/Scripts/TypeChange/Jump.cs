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
        jumpAction.performed += Jumping;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * jumpDistance, Color.red);
    }

    private void Jumping(InputAction.CallbackContext callback)
    {
        if (callback.performed && InGround())
        {
            Debug.Log(callback.performed.ToString());
            playerRb.AddForce(0, jumpVelocity, 0);
        }
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
