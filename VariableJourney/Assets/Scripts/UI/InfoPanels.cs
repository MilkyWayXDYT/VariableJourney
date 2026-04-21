using UnityEngine;

public class InfoPanels : MonoBehaviour
{
    [SerializeField]
    private float lookPointHeigh = 2;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Vector3 uiRotation = new Vector3(player.transform.position.x, lookPointHeigh, player.transform.position.z);
        this.transform.LookAt(uiRotation);
    }
}
