using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTree : MonoBehaviour
{
    [SerializeField] private float treeHp = 10f;
    [SerializeField] private int treeDamage = 10;
    [SerializeField] private int fireDamage = 10;
    [SerializeField] private float fireDamageFrequency = 1f;
    [SerializeField] private float fireRange = 5f;

    [SerializeField] delegate void OnTreePlayerHitEvent();
    OnTreePlayerHitEvent onTreePlayerHitEvent;
    [SerializeField] delegate void OnFireHitEvent();
    OnFireHitEvent onFireHitEvent;
    [SerializeField] delegate void OnTreeSpawnEvent();
    OnTreeSpawnEvent onTreeSpawnEvent;
    [SerializeField] delegate void OnTreeTakeDamage();
    OnTreeTakeDamage onTreeTakeDamage;

    private Transform target;
    private float timer = 0f;
    private LineRenderer lineRenderer;

    public void Init(Transform _target, int _hp, int _damage, int _fireDamage, float _frequency, float _fireRadius) {
        target = _target;
        treeHp = _hp;
        treeDamage = _damage;
        fireDamage = _fireDamage;
        fireDamageFrequency = _frequency;
        fireRange = _fireRadius;
    }

    private void Start() {
        transform.position = target.transform.position;

        timer = fireDamageFrequency;
        lineRenderer = GetComponent<LineRenderer>();
        DrawAoe(50, fireRange);

        onTreeSpawnEvent += OnSpawn;
        onTreePlayerHitEvent += OnTreeHit;
        onFireHitEvent += OnFireHit;
        onTreeTakeDamage += OnDamage;

        onTreeSpawnEvent?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.transform.position.x, 2) + Mathf.Pow(transform.position.y - target.transform.position.y, 2));
            if (distance <= fireRange) {
                onFireHitEvent?.Invoke();
                target.gameObject.GetComponent<IHealth>()?.TakeDamage(fireDamage);
            }
                //player.TakeDamage(damage);

            timer = fireDamageFrequency;
        }
    }

    private void DrawAoe(int steps, float radius) {
        lineRenderer.positionCount = steps;

        for(int currentStep = 0; currentStep < steps; ++ currentStep) {
            float circumference = (float)currentStep / steps;

            float currentRadian = circumference * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x + transform.position.x, y + transform.position.y, 0);

            lineRenderer.SetPosition(currentStep, currentPosition);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            onTreePlayerHitEvent?.Invoke();
            collision.gameObject.GetComponent<IHealth>()?.TakeDamage(treeDamage);
        }
    }

    private void TakeDamage(float damage) {
        onTreeTakeDamage?.Invoke();
        treeHp = treeHp - damage;
        if (treeHp <= 0) {
            Destroy(this);
        }
    }

    private void OnSpawn() {

    }

    private void OnTreeHit() {

    }

    private void OnFireHit() {

    }

    private void OnDamage() {

    }

    private void OnDestroy() {
        onTreeSpawnEvent -= OnSpawn;
        onTreePlayerHitEvent -= OnTreeHit;
        onFireHitEvent -= OnFireHit;
        onTreeTakeDamage -= OnDamage;
    }
}
