using UnityEngine;

public class IntBall : MonoBehaviour
{
    [SerializeField]
    private Transform targetPos;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Point")
            transform.position = targetPos.position;

        if (other.gameObject.tag == "Player")
            Destroy(this);
    }
}
