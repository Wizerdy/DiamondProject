using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBeamEntity : MonoBehaviour
{
    [SerializeField] private float raySpeed = 10f;
    [SerializeField] private float rayDamage = 5f;
    [SerializeField] private float damageFrequency = 1f;
    [SerializeField] private float duration = 5f;

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

    public void Init(Player _player, float _raySpeed, float _rayDamage, float _damageFrequency, float _duration) {
        player = _player;
        raySpeed = _raySpeed;
        rayDamage = _rayDamage;
        damageFrequency = _damageFrequency;
        duration = _duration;
    }

    private void Start() {
        transform.position = player.transform.position;

        damageFrequencyTimer = 0;
        durationTimer = duration;
        lineRenderer = GetComponent<LineRenderer>();

        onBeamSpawnEvent += OnSpawn;
        onBeamPlayerHitEvent += OnRayHitPlayer;
        onBeamHitEvent += OnRayHit;

        onBeamSpawnEvent?.Invoke();
    }

    // Update is called once per frame
    void Update() {
        hitPos = Vector3.MoveTowards(hitPos, player.transform.position, raySpeed * Time.deltaTime);

        lineRenderer.SetPosition(1, hitPos);
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
        Debug.Log(durationTimer);
        if (durationTimer <= 0) 
            Destroy(this.gameObject);
        
        onBeamHitEvent?.Invoke();
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
