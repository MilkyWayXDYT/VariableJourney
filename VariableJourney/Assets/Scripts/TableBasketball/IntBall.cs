using UnityEngine;

public class IntBall : MonoBehaviour
{
    [SerializeField]
    private Transform targetPos;
    [SerializeField]
    private LineController lineController;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject cameraUI;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Point")
            transform.position = targetPos.position;

        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            lineController.enabled = true;
            cameraUI.SetActive(true);
            player.GetComponent<TypeSwitch>().enabled = true;
            player.GetComponent<Jump>().enabled = true;
        }
    }
}
