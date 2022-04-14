using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBeamEntity : MonoBehaviour
{
    [SerializeField] private float raySpeed = 10f;
    [SerializeField] private float rayDamage = 5f;
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

    private Player player;
    private float damageFrequencyTimer = 0f;
    private float durationTimer = 0f;
    private LineRenderer lineRenderer;
    private float currentSpeed;

    public void Init(Player _player, float _raySpeed, float _rayDamage, float _damageFrequency, float _duration, float _speedDistance, float _distance) {
        player = _player;
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

        onBeamSpawnEvent += OnSpawn;
        onBeamPlayerHitEvent += OnRayHitPlayer;
        onBeamHitEvent += OnRayHit;

        onBeamSpawnEvent?.Invoke();
    }

    // Update is called once per frame
    void Update() {
        hitPos = Vector3.MoveTowards(hitPos, player.transform.position, currentSpeed * Time.deltaTime);

        lineRenderer.SetPosition(1, hitPos);

        if (distance < GetDistance(player.transform.position, hitPos)) {
            currentSpeed = raySpeedIfFar;
        } else {
            currentSpeed = raySpeed;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, hitPos);

        if (hit.transform.gameObject.tag == "Player") {
            damageFrequencyTimer -= Time.deltaTime;
            if (damageFrequencyTimer <= 0) {
                onBeamPlayerHitEvent?.Invoke();
                Debug.Log("took " + rayDamage + " beam damage");

                //player.TakeDamage(damage);

                damageFrequencyTimer = damageFrequency;
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
