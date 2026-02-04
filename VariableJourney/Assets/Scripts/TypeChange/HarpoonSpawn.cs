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
            harpoon.GetComponent<Rigidbody>().AddForce(spawnPoint.transform.rotation * Vector3.forward * shotPower, ForceMode.Impulse);
            sent = true;
        }
    }

}
