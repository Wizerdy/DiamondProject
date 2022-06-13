using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

public class LeafBeam : BaseAttack {
    [SerializeField] private GameObject _leafBeamPrefab;
    [SerializeField] private float _rayAngularSpeed = 10f;
    [SerializeField] private float _rayLinearSpeed = 10f;
    [SerializeField] private int _rayDamage = 5;
    [SerializeField] private int _radius = 5;
    //[SerializeField] private float _damageFrequency = 0f;
    [SerializeField] private Vector3 _beamPosOnBoss;
    [SerializeField] float _minDistLaser = 5;
    private float damageFrequencyTimer = 0f;
    GameObject currentBeam;
    [SerializeField, HideInInspector] UnityEvent _onStart;
    [SerializeField] UnityEvent<GameObject> _onHit;

    public event UnityAction OnSpawn { add => _onStart.AddListener(value); remove => _onStart.RemoveListener(value); }
    public event UnityAction<GameObject> OnHit { add => _onHit.AddListener(value); remove => _onHit.RemoveListener(value); }

    protected override IEnumerator IExecute() {
        _onStart?.Invoke();
        float _minDistLaserSquare = _minDistLaser * _minDistLaser;
        _rayAngularSpeed = Mathf.Acos((2 * _minDistLaserSquare - _rayLinearSpeed * _rayLinearSpeed) / (2 * _minDistLaserSquare));

        Vector3 dir;
        Vector3 currentAim = (PlayerPos - BossPos).normalized * _minDistLaser + BossPos;
        Vector3 nextPosition;

        currentBeam = Instantiate(_leafBeamPrefab, _beamPosOnBoss + BossPos, Quaternion.identity).gameObject;
        GameObject rendererBeam = currentBeam.transform.GetChild(0).gameObject;
        GameObject impact = currentBeam.transform.GetChild(1).gameObject;
        //impact.transform.GetChild(1).transform.localScale = new Vector3(_radius, _radius, _radius);

        float attackTimer = duration;
        while (attackTimer > 0) {
            currentBeam.transform.position = BossPos + _beamPosOnBoss;
            dir = (PlayerPos - currentAim).normalized;
            nextPosition = currentAim + dir * Time.deltaTime * _rayLinearSpeed;
            currentAim = nextPosition;

            //if (Vector3.Distance(nextPosition, BossPos) < _minDistLaser) {
            //    currentAim += Vector3.RotateTowards(currentAim - BossPos + (currentAim - BossPos).normalized, PlayerPos - BossPos, _rayAngularSpeed * Time.deltaTime, 0.0f) - (currentAim - BossPos);
            //} else {
            //    currentAim = nextPosition;
            //}


            Debug.DrawRay(BossPos, currentAim - BossPos, Color.blue);

            Vector2 direction = currentAim - currentBeam.transform.position;
            rendererBeam.transform.position = Vector3.Lerp(currentBeam.transform.position, currentAim, 0.5f);
            rendererBeam.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            rendererBeam.transform.localScale = new Vector3(4, Vector3.Distance(currentBeam.transform.position, currentAim), 1);
            currentBeam.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            rendererBeam.transform.GetChild(0).transform.position = currentBeam.transform.position;
            impact.transform.position = currentAim;
            impact.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.forward);
            Collider2D[] hits = Physics2D.OverlapCircleAll(impact.transform.position, _radius);
            for (int i = 0; i < hits.Length; i++) {
                Collider2D hit = hits[i];
                _onHit?.Invoke(hit.gameObject);
                if (hit.transform.gameObject.CompareTag("Player")) {
                    hit.transform.gameObject.GetComponent<IHealth>()?.TakeDamage(_rayDamage);
                }
            }
            Debug.DrawRay(currentBeam.transform.position, currentAim - currentBeam.transform.position, Color.red);
            attackTimer -= Time.deltaTime;
            yield return null;
        }
    }

    public override void End() {
        base.End();
        currentBeam.gameObject.SetActive(false);
        Destroy(currentBeam);
    }
}
