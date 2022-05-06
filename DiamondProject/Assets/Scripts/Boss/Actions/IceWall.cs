using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour {
    [SerializeField] private int segments = 50;
    [SerializeField] private float radius = 50f;
    [SerializeField] private float offSet = 40f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Reference<Transform> player;

    [SerializeField] private PolygonCollider2D polygonCollider;

    private LineRenderer line;
    private float circleAngle = 0;

    private List<Vector2> colliderPoints = new List<Vector2>();

    void Start() {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        circleAngle = radius;

        float angle = Mathf.Atan2(player.Instance.position.x, player.Instance.position.y) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -angle);
        CreateWall();
    }

    private void Update() {
        float x;
        float y;
        circleAngle = 20f;
        radius = radius + speed * Time.deltaTime;
        for (int i = 0; i < (segments + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * circleAngle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * circleAngle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0));

            circleAngle += (360f - offSet) / segments;
        }

        Vector3[] positions = GetPositions();

        if (positions.Length >= 2) {

            int numberOfLines = positions.Length - 1;

            polygonCollider.pathCount = numberOfLines;

            for (int i = 0; i < numberOfLines; i++) {

                List<Vector2> currentPositions = new List<Vector2> {
                    positions[i],
                    positions[i+1]
                };

                List<Vector2> currentColliderPoints = CalculateColliderPoints(currentPositions);
                polygonCollider.SetPath(i, currentColliderPoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
            }
        } else {

            polygonCollider.pathCount = 0;
        }
    }


    private void CreateWall() {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f - offSet) / segments;
        }
    }

    private List<Vector2> CalculateColliderPoints(List<Vector2> positions) {
        float width = line.startWidth;

        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        Vector2[] offsets = new Vector2[2];
        offsets[0] = new Vector2(-deltaX, deltaY);
        offsets[1] = new Vector2(deltaX, -deltaY);

        List<Vector2> colliderPoints = new List<Vector2> {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };

        return colliderPoints;
    }

    public Vector3[] GetPositions() {
        Vector3[] pos = new Vector3[line.positionCount];
        line.GetPositions(pos);
        return pos;
    }
}
