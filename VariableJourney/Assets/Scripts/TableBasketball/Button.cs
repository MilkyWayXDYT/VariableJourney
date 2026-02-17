using System.Linq;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private float loweringSpeed = 0.05f;
    [SerializeField]
    private float loweringPlaneSpeed = 1f;
    [SerializeField]
    private GameObject pushedPlane;
    [SerializeField]
    private Transform targetPosition;
    [SerializeField]
    private Rigidbody ball;
    [SerializeField]
    private Vector3 force;

    private Vector3 loweringDistance = new Vector3(0, 0.1f);
    private Vector3 targetBtnPosition;
    private Vector3 originalPos;

    private bool ballMove = false;

    private void Start()
    {
        originalPos = transform.position;
        targetBtnPosition = originalPos;
    }

    private bool IsTouchingTarget()
    {
        return Physics.OverlapSphere(ball.position, 1.1f).Any(hit => hit.gameObject == pushedPlane);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            targetBtnPosition = originalPos - loweringDistance;
            Collider[] hits = Physics.OverlapSphere(ball.position, 1.1f);
            if (IsTouchingTarget())
                ballMove = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            targetBtnPosition = originalPos;
            ballMove = false;
        }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetBtnPosition, loweringSpeed * Time.deltaTime);
        if (ballMove)
        {
            ball.AddForceAtPosition(force, targetPosition.position + RandDirection());
            ballMove = false;
        }
    }

    private Vector3 RandDirection()
    {
        return new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
    }
}
