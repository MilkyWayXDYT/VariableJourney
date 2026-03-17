using UnityEngine;

public class Platform : MonoBehaviour
{
    public float harpoonForce = 10;
    public float destroyDistance = 2f;

    private bool inUp = false;
    private Vector3 posTarget;

    public void PlatformMove()
    {
        if (inUp)
            posTarget = transform.position + Vector3.down * 3;
        else
            posTarget = transform.position + Vector3.up * 3;
        inUp = !inUp;
    }

    private void Update()
    {
        if (posTarget != Vector3.zero && transform.position != posTarget)
            transform.position = Vector3.MoveTowards(transform.position, posTarget, 5f * Time.deltaTime);
    }
}
