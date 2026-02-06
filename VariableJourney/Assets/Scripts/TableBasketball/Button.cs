using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private float loweringSpeed = 0.5f;

    private Vector3 loweringDistance = new Vector3(0, 0.1f);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ButtonDown();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ButtonUp();
        }
    }

    private void ButtonDown()
    {
        Vector3 targetPosition = transform.position - loweringDistance;
        while (targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, loweringSpeed * Time.deltaTime);
        }
    }

    private void ButtonUp()
    {
        Vector3 targetPosition = transform.position + loweringDistance;
        while (targetPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, loweringSpeed * Time.deltaTime);
        }
    }
}
