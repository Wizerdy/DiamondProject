using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class LeafBeam : BaseAttack {
    [SerializeField] private GameObject leafBeamPrefab;
    [SerializeField] private float raySpeed = 10f;
    float currentSpeed = 10f;
    [SerializeField] private int rayDamage = 5;
    [SerializeField] private float damageFrequency = 1f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private Vector3 _beamPosOnBoss;
    [SerializeField] private Vector3 target;

    private GameObject currentleafBeam;
    private float damageFrequencyTimer = 0f;
    private LineRenderer lineRenderer;
    [SerializeField] delegate void OnBeamPlayerHitEvent();
    OnBeamPlayerHitEvent onBeamPlayerHitEvent;
    [SerializeField] delegate void OnBeamSpawnEvent();
    OnBeamSpawnEvent onBeamSpawnEvent;
    [SerializeField] delegate void OnBeamHitEvent();
    OnBeamHitEvent onBeamHitEvent;

    protected override IEnumerator IExecute() {
        target = BossPos;
        currentleafBeam = Instantiate(leafBeamPrefab, _beamPosOnBoss + BossPos, Quaternion.identity).gameObject;
        lineRenderer = currentleafBeam.GetComponent<LineRenderer>();
        onBeamSpawnEvent += OnSpawn;
        onBeamPlayerHitEvent += OnRayHitPlayer;
        onBeamHitEvent += OnRayHit;
        onBeamSpawnEvent?.Invoke();
        currentSpeed = raySpeed;
        float attackTimer = duration;
        while (attackTimer > 0) {
            attackTimer -= Time.deltaTime;
            currentleafBeam.transform.position = _beamPosOnBoss + BossPos;
            target = Vector3.MoveTowards(target, PlayerPos, currentSpeed * Time.deltaTime);
            UpdateRenderer(target);
            damageFrequencyTimer -= Time.deltaTime;
            RaycastHit2D[] hits = Physics2D.RaycastAll(currentleafBeam.transform.position, target - currentleafBeam.transform.position);
            for (int i = 0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];
                onBeamHitEvent?.Invoke();
                if (hit.transform.gameObject.tag == "Player" && damageFrequencyTimer <= 0) {
                    onBeamPlayerHitEvent?.Invoke();
                    hit.transform.gameObject.GetComponent<IHealth>()?.TakeDamage(rayDamage);
                    damageFrequencyTimer = damageFrequency;
                }
            }
            yield return null;
        }
        Destroy(currentleafBeam);
    }

    private void UpdateRenderer(Vector3 target) {
        Vector2 direction = target - currentleafBeam.transform.position;
        currentleafBeam.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        lineRenderer.SetPosition(1, target);
        lineRenderer.SetPosition(0, currentleafBeam.transform.position);
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
