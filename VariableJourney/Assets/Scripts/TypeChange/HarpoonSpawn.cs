using UnityEngine;
using UnityEngine.InputSystem;

public class HarpoonSpawn : MonoBehaviour
{
    public bool sent = false;

    [SerializeField]
    private GameObject harpoonPref;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float shotPower;
    [SerializeField]
    private Transform cameraRot;

    private PlayerInput playerInput;
    private InputAction shotAction;

    private GameObject harpoon;

    void Start()
    {
        playerInput = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
        shotAction = playerInput.actions.FindAction("HarpoonShot");
        shotAction.performed += HarpoonShot;
    }

    public void HarpoonShot(InputAction.CallbackContext context)
    {

        if (context.performed && !sent)
        {
            harpoon = Instantiate(harpoonPref, spawnPoint.transform.position, Quaternion.identity);
            float cameraX = cameraRot.eulerAngles.x;

            Quaternion cameraPitch = Quaternion.Euler(cameraX, 0, 0);
            Quaternion finalRot = spawnPoint.rotation * cameraPitch;

            harpoon.GetComponent<Rigidbody>().AddForce(finalRot * Vector3.forward * shotPower, ForceMode.Impulse);
            sent = true;
        }
        else if (context.performed && sent)
        {
            Destroy(harpoon);
            sent = false;
        }
    }

}
