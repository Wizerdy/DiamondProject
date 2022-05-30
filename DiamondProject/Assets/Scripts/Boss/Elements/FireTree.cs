using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum TreeState {
    seed,
    growing,
    fullGrown
}
public class FireTree : MonoBehaviour {
    [SerializeField] private float treeHp = 10f;
    [SerializeField] private int treeDamage = 10;
    [SerializeField] private int fireDamage = 10;
    [SerializeField] private float fireDamageFrequency = 1f;
    [SerializeField] private float fireRange = 5f;
    [SerializeField] private float treeDuration = 3f;
    [SerializeField] Animator _treeAnimator;

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

    private float growthSpeed = 1f;
    private float growthTimer = 0f;

    private float delayBeforeGrowth = 3f;
    private float delayBeforeGrowthTimer = 3f;

    //private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;
    private TreeState currentState;

    public void Init(Transform _target, int _hp, int _damage, int _fireDamage, float _frequency, float _fireRadius, float _growthSpeed, float _delayBeforeGrowth) {
        target = _target;
        treeHp = _hp;
        treeDamage = _damage;
        fireDamage = _fireDamage;
        fireDamageFrequency = _frequency;
        fireRange = _fireRadius;
        growthSpeed = _growthSpeed;
        delayBeforeGrowth = _delayBeforeGrowth;
    }

    private void Start() {
        transform.position = target.transform.position;

        timer = 0;
        growthTimer = growthSpeed;
        delayBeforeGrowthTimer = delayBeforeGrowth;

        currentState = TreeState.seed;

        // lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        onTreeSpawnEvent += OnSpawn;
        onTreePlayerHitEvent += OnTreeHit;
        onFireHitEvent += OnFireHit;
        onTreeTakeDamage += OnDamage;

        onTreeSpawnEvent?.Invoke();
        boxCollider.enabled = false;

        //  lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update() {

        if (currentState == TreeState.fullGrown) {
            timer += Time.deltaTime;
            if (timer >= treeDuration) {
                Destroy(gameObject);
                //float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - target.transform.position.x, 2) + Mathf.Pow(transform.position.y - target.transform.position.y, 2));
                //if (distance <= fireRange) {
                //    onFireHitEvent?.Invoke();
                //    target.gameObject.GetComponent<IHealth>()?.TakeDamage(fireDamage);
                //}

                //timer = fireDamageFrequency;
            }
        }

        if (currentState == TreeState.growing) {
            growthTimer -= Time.deltaTime;
            if (growthTimer <= 0) {
                // lineRenderer.enabled = true;
                //DrawAoe(50, fireRange);
                currentState = TreeState.fullGrown;
            }
        }

        if (currentState == TreeState.seed) {
            delayBeforeGrowthTimer -= Time.deltaTime;
            if (delayBeforeGrowthTimer <= 0) {
                Grow();
            }
        }
    }

    private void Grow() {
        boxCollider.enabled = true;
        currentState = TreeState.growing;

        _treeAnimator?.SetTrigger("Emerge");
    }

    private void DrawAoe(int steps, float radius) {
        //lineRenderer.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; ++currentStep) {
            float circumference = (float)currentStep / steps;

            float currentRadian = circumference * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x + transform.position.x, y + transform.position.y, 0);

            //lineRenderer.SetPosition(currentStep, currentPosition);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" && currentState != TreeState.fullGrown) {
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
