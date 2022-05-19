using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class LeafBeam : BaseAttack {
    [SerializeField] private GameObject _leafBeamPrefab;
    [SerializeField] private float _rayAngularSpeed = 10f;
    [SerializeField] private float _rayLinearSpeed = 10f;
    [SerializeField] private int _rayDamage = 5;
    //[SerializeField] private float _damageFrequency = 0f;
    [SerializeField] private Vector3 _beamPosOnBoss;
    [SerializeField] float _minDistLaser = 5;
    Vector3 _beamPos;
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
        float _minDistLaserSquare = _minDistLaser * _minDistLaser;
        _rayAngularSpeed = Mathf.Acos((2 * _minDistLaserSquare - _rayLinearSpeed * _rayLinearSpeed) / (2 * _minDistLaserSquare));
        currentleafBeam = Instantiate(_leafBeamPrefab, _beamPosOnBoss + BossPos, Quaternion.identity).gameObject;
        lineRenderer = currentleafBeam.GetComponent<LineRenderer>();
        onBeamSpawnEvent += OnSpawn;
        onBeamPlayerHitEvent += OnRayHitPlayer;
        onBeamHitEvent += OnRayHit;
        onBeamSpawnEvent?.Invoke();
        float attackTimer = duration;
        Vector3 dir;
        Vector3 currentAim = (PlayerPos - BossPos).normalized * _minDistLaser + BossPos;
        Vector3 nextPosition = currentAim;
        while (attackTimer > 0) {
            currentleafBeam.transform.position = BossPos + _beamPosOnBoss;
            dir = (PlayerPos - currentAim).normalized;
            nextPosition = currentAim + dir * Time.deltaTime * _rayLinearSpeed;
            //Debug.DrawRay(BossPos, nextPosition - BossPos, Color.black);
            //Debug.Log(Vector3.RotateTowards(currentAim - BossPos, PlayerPos - BossPos, _rayAngularSpeed * Time.deltaTime, 0.0f));
            if (Vector3.Distance(nextPosition, currentleafBeam.transform.position) < _minDistLaser) {
                currentAim += Vector3.RotateTowards(currentAim - currentleafBeam.transform.position + (currentAim - currentleafBeam.transform.position).normalized, PlayerPos - currentleafBeam.transform.position, _rayAngularSpeed * Time.deltaTime, 0.0f) - (currentAim - currentleafBeam.transform.position);
            } else {
                currentAim = nextPosition;
            }
            Debug.DrawRay(BossPos, currentAim - BossPos, Color.blue);
            UpdateRenderer(currentAim);
            RaycastHit2D[] hits = Physics2D.RaycastAll(currentleafBeam.transform.position, currentAim - currentleafBeam.transform.position, Vector3.Distance(currentAim, currentleafBeam.transform.position) + 1);
            Debug.DrawRay(currentleafBeam.transform.position, currentAim - currentleafBeam.transform.position, Color.red);
            for (int i = 0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];
                onBeamHitEvent?.Invoke();
                if (hit.transform.gameObject.tag == "Player") {
                    onBeamPlayerHitEvent?.Invoke();
                    hit.transform.gameObject.GetComponent<IHealth>()?.TakeDamage(_rayDamage);
                }
            }
            attackTimer -= Time.deltaTime;
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
