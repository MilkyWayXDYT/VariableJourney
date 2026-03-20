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

    private void Jumping(InputAction.CallbackContext callback)
    {
        if (callback.performed && InGround())
        {
            Debug.Log(callback.performed.ToString());
            playerRb.AddForce(0, jumpVelocity, 0);
        }
    }

    RaycastHit hitBox;

    private bool InGround()
    {
        bool result = false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, jumpDistance))
            result = true;

        CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
        float radius = col.radius;

        Vector3 center = transform.position + Vector3.down * (col.height / 2);
        Vector3 halfExtendsSides = Vector3.one * radius * 1.5f;
        Vector3 halfExtends = new Vector3(halfExtendsSides.x, 0.05f, halfExtendsSides.z);

        if (Physics.BoxCast(center, halfExtends, Vector3.down, out hitBox))
            result = true;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
        float radius = col.radius;

        Vector3 center = transform.position + Vector3.down * (col.height / 2);
        Vector3 halfExtendsSides = Vector3.one * radius * 1.5f;
        Vector3 halfExtends = new Vector3(halfExtendsSides.x, 0.05f, halfExtendsSides.z);

        Gizmos.DrawWireCube(center, halfExtends);
    }
}
