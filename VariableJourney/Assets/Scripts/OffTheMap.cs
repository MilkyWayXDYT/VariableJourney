using UnityEngine;

public class OffTheMap : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y <= -10)
            Destroy(gameObject);
    }
}
