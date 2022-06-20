using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

//[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class IceWall : MonoBehaviour {
    [SerializeField] private int segments = 50;
    [SerializeField] private float radius = 50f;
    [SerializeField] private float gapWidth = 40f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 10;
    //[SerializeField] private Reference<Transform> player;

    [SerializeField] private EdgeCollider2D edgeCollider;
    [SerializeField] private LineRenderer line;
    [SerializeField] List<Transform> effects = new List<Transform>();

    private float circleAngle = 0;
    private Vector2[] _points;

    //private List<Vector2> colliderPoints = new List<Vector2>();

    [SerializeField] UnityEvent _onWallDestroy;

    public event UnityAction OnWallDestroy { add => _onWallDestroy.AddListener(value); remove => _onWallDestroy.RemoveListener(value); }

    void Start() {
        //line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        circleAngle = radius;

        //float angle = Mathf.Atan2(player.Instance.position.x, player.Instance.position.y) * 180 / Mathf.PI;
        //transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -angle);
        _points = new Vector2[segments + 1];
        for (int i = 0; i < _points.Length; i++) {
            _points[i] = new Vector2();
        }
        CreateWalls();
        SetGraphics();
        SetEdgeCollider();
        edgeCollider.enabled = true;
    }

    public void Init(int _segments, float _radius, float _gapWidth, float _speed, int _damage) {
        segments = _segments;
        radius = _radius;
        gapWidth = _gapWidth;
        speed = _speed;
        damage = _damage;
    }

    private void Update() {
        //Debug.DrawRay(Vector2.zero, Vector2.up * 100f, Color.red, 10f, false);
        radius -= speed * Time.deltaTime;
        if (radius <= 1) {
            Destroy(gameObject);
            return;
        }
        CreateWalls();

        SetGraphics();
        SetEdgeCollider();
    }

    private void SetEdgeCollider() {
        List<Vector2> edges = new List<Vector2>();

        for (int i = 0; i < _points.Length; ++i) {
            Vector3 point = _points[i];
            edges.Add(new Vector2(point.x, point.y));
        }

        edgeCollider.SetPoints(edges);
    }

    private void CreateWalls() {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            //line.SetPosition(i, new Vector3(x, y, 0));
            _points[i].Set(x, y);

            angle += (360f - gapWidth) / segments;
        }
    }

    private void SetGraphics() {
        Quaternion positiveAngle = Quaternion.AngleAxis(-gapWidth/2f, Vector3.forward);
        Quaternion negativeAngle = Quaternion.AngleAxis(gapWidth/2f, Vector3.forward);

        if (effects.Count < 8) { return; }
        float dist = Tools.Remap(gapWidth, 0f, 180f, 1.25f, 0.5f);

        effects[0].localPosition = positiveAngle * Vector2.up * radius;
        effects[1].localPosition = positiveAngle * new Vector2(dist, 1f) * radius;
        effects[2].localPosition = new Vector2(dist, -1f) * radius;
        effects[3].localPosition = Vector2.down * radius;
        effects[4].localPosition = negativeAngle * Vector2.up * radius;
        effects[5].localPosition = negativeAngle * new Vector2(-dist, 1f) * radius;
        effects[6].localPosition = new Vector2(-dist, -1f) * radius;
        effects[7].localPosition = Vector2.down * radius;

        //Debug.DrawLine(Vector2.zero, result * 2f, Color.blue);
        //Debug.DrawLine(Vector2.zero, result2 * 2f, Color.blue);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<IHealth>()?.TakeDamage(damage);
        }
    }

    private void OnDisable() {
        Destroy(gameObject);
    }

    private void OnDestroy() {
        _onWallDestroy?.Invoke();
    }
}
