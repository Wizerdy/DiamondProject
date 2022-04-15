using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBeamEntity : MonoBehaviour
{
    [SerializeField] private float raySpeed = 10f;
    [SerializeField] private int rayDamage = 5;
    [SerializeField] private float damageFrequency = 1f;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float raySpeedIfFar = 75f;
    [SerializeField] private float distance = 5f;

    [SerializeField] private Vector3 hitPos = new Vector3(0, 0, 0);

    [SerializeField] delegate void OnBeamPlayerHitEvent();
    OnBeamPlayerHitEvent onBeamPlayerHitEvent;
    [SerializeField] delegate void OnBeamSpawnEvent();
    OnBeamSpawnEvent onBeamSpawnEvent;
    [SerializeField] delegate void OnBeamHitEvent();
    OnBeamSpawnEvent onBeamHitEvent;

    private Transform target;
    private float damageFrequencyTimer = 0f;
    private float durationTimer = 0f;
    private LineRenderer lineRenderer;
    private float currentSpeed;

    public void Init(Transform _target, float _raySpeed, int _rayDamage, float _damageFrequency, float _duration, float _speedDistance, float _distance) {
        target = _target;
        raySpeed = _raySpeed;
        rayDamage = _rayDamage;
        damageFrequency = _damageFrequency;
        duration = _duration;
        raySpeedIfFar = _speedDistance;
        distance = _distance;
    }

    private void Start() {
        damageFrequencyTimer = 0;
        durationTimer = duration;
        lineRenderer = GetComponent<LineRenderer>();

        currentSpeed = raySpeed;
        hitPos = transform.position;
        onBeamSpawnEvent += OnSpawn;
        onBeamPlayerHitEvent += OnRayHitPlayer;
        onBeamHitEvent += OnRayHit;

        onBeamSpawnEvent?.Invoke();
    }

    // Update is called once per frame
    void Update() {
        hitPos = Vector3.MoveTowards(hitPos, target.transform.position, currentSpeed * Time.deltaTime);

        lineRenderer.SetPosition(1, hitPos);

        if (distance < GetDistance(target.transform.position, hitPos)) {
            currentSpeed = raySpeedIfFar;
        } else {
            currentSpeed = raySpeed;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, hitPos);
        for (int i = 0; i < hits.Length; i++) {
            RaycastHit2D hit = hits[i];
            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "Player") {
                damageFrequencyTimer -= Time.deltaTime;
                if (damageFrequencyTimer <= 0) {
                    onBeamPlayerHitEvent?.Invoke();
                    hit.transform.gameObject.GetComponent<IHealth>()?.TakeDamage(rayDamage);

                    damageFrequencyTimer = damageFrequency;
                }
            }
        }


        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0) 
            Destroy(this.gameObject);
        
        onBeamHitEvent?.Invoke();
    }

    private float GetDistance(Vector3 A, Vector3 B) {
        return Mathf.Sqrt(Mathf.Pow(A.x - B.x, 2) + Mathf.Pow(A.y - B.y, 2));
    }

    private void OnSpawn() {

    }

    private void OnRayHitPlayer() {

    }

    private void OnRayHit() {

    }



    private void OnDestroy() {
        onBeamSpawnEvent -= OnSpawn;
        onBeamPlayerHitEvent -= OnRayHitPlayer;
        onBeamHitEvent -= OnRayHit;

    }
}
