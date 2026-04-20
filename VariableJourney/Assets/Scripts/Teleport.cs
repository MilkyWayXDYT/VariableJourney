using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private Transform teleportPoint;
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!string.IsNullOrEmpty(sceneName))
                SceneManager.LoadScene(sceneName);

            if (teleportPoint)
                player.transform.position = teleportPoint.position;
        }
    }
}
