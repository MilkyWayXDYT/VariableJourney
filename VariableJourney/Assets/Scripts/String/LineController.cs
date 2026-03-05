using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// линия строится один раз, когда пользователь приходит на уровень, на данный момент скрипт выключен
[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    [SerializeField] private List<Transform> anchorsTransform;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform endPoint;

    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private PhysicsMaterial ropeMaterial;

    public List<Vector3> points;
    List<GameObject> ropeColliders;
    private Player player;

    private void Start()
    {
        player = playerPos.GetComponent<Player>();

        ropeColliders = new List<GameObject>();
        points = new List<Vector3>();

        points.Add(playerPos.position);

        foreach (Transform anchor in anchorsTransform)
        {
            var ancPoints = anchor.GetComponentsInChildren<Transform>().Where(ch => ch != anchor.transform).ToList();
            foreach (Transform point in ancPoints)
                points.Add(point.position);
        }
        points.Add(endPoint.position);

        BuildRope();
    }

    public void BuildRope()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());

        for (int i = 1; i < points.Count - 1; i++) 
            SetRopeColliders(points[i], points[i + 1]);
    }

    private void Update()
    {
        points[0] = playerPos.position;
        lineRenderer.SetPosition(0, points[0]);
        if (points.Count < 4)
        {
            EndStringGame();
        }
        CheckAvailabilityPoint();
        SetNewRopePoint();
    }

    private void CheckAvailabilityPoint()
    {
        Vector3 dir = (points[2] - points[0]).normalized;

        RaycastHit hit;
        Debug.DrawLine(points[0] + dir / 2, points[2] - dir / 3, Color.red);
        if (!Physics.Linecast(points[0] + dir / 2, points[2] - dir / 3, out hit))
        {
            points.RemoveAt(1);
            player.maxDistance = Vector3.Distance(transform.position, points[1]);

            var colliders = GameObject.FindGameObjectsWithTag("Collider");
            if (colliders.Length > 0)
                foreach (var collider in colliders)
                    Destroy(collider);

            BuildRope();
        }
    }

    private void SetRopeColliders(Vector3 startPoint, Vector3 endPoint)
    {

        var ropeSegment = new GameObject("RopeCollider");
        ropeSegment.tag = "Collider";
        Vector3 midPoint = (startPoint + endPoint) / 2;

        ropeSegment.transform.position = midPoint;

        Vector3 dir = startPoint - endPoint;
        float length = dir.magnitude;

        ropeSegment.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        var ropeCol = ropeSegment.AddComponent<CapsuleCollider>();
        ropeCol.radius = 0.05f;
        ropeCol.height = length + ropeCol.radius * 2;
        ropeCol.material = ropeMaterial;

        ropeColliders.Add(ropeSegment);
    }

    private void SetNewRopePoint()
    {
        Vector3 dir = points[1] - points[0];
        RaycastHit hit;

        Debug.DrawLine(playerPos.position, points[1] - dir / 100 * 5, Color.green);
        if (Physics.Linecast(playerPos.position, points[1] - dir / 100 * 10, out hit, 1 << 30))
        {
            points.Insert(1, hit.point);
            BuildRope();
        }
    }

    private void EndStringGame()
    {
        Destroy(this.gameObject);
    }
}
