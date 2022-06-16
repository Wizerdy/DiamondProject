using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ShardType {
    iceHell, bulletHell
}

public class IceShard : MonoBehaviour
{
    [SerializeField] string targetTag = "Player";
    [SerializeField] float lifeSpan = 15f;
    [SerializeField] float maxSize = 3f;
    [SerializeField] float growthTime = 1f;
    [SerializeField] DamageHealth _modDamage = null;

    [HideInInspector, SerializeField] UnityEvent<IceShard> _onShardSpawn;
    [HideInInspector, SerializeField] UnityEvent<GameObject> _onShardHit;
    [HideInInspector, SerializeField] UnityEvent _onShardDestroy;

    Vector3 aimDir = new Vector3(0, 0, 0);

    float shardSpeed = 10f;
    int shardDamage = 5;
    Transform target;
    Rigidbody2D rb;
    float _lifeTimer;
    Vector3 size;
    bool isTargetingPlayer = false;
    bool canMove = false;
    public ShardType shardType;

    public event UnityAction<IceShard> OnShardSpawn { add => _onShardSpawn.AddListener(value); remove => _onShardSpawn.RemoveListener(value); }
    public event UnityAction<GameObject> OnShardHit { add => _onShardHit.AddListener(value); remove => _onShardHit.RemoveListener(value); }
    public event UnityAction OnShardDestroy { add => _onShardDestroy.AddListener(value); remove => _onShardDestroy.RemoveListener(value); }

    //method overload
    public void Init(ShardType _shardType, Transform _target, float _ShardSpeed, int _ShardDamage, Vector3 _aimDir, float lifetime, bool _isTargetingPlayer = false) {
        target = _target;
        shardSpeed = _ShardSpeed;
        shardDamage = _ShardDamage;
        aimDir = _aimDir;
        isTargetingPlayer = _isTargetingPlayer;
        lifeSpan = lifetime;
        shardType = _shardType;
    }

    private void Start() {
        canMove = false;
        rb = GetComponent<Rigidbody2D>();

        //transform.localScale = new Vector3(0,0,1);
        //StartCoroutine(Growth());
        _onShardSpawn?.Invoke(this);

        _lifeTimer = lifeSpan;
        size = new Vector3(maxSize, maxSize, 1);

        if (_modDamage != null) {
            _modDamage.Damage = shardDamage;
            _modDamage.OnCollide += _OnHit;
        }

        StartCoroutine(Growth(growthTime));
    }

    private void FixedUpdate() {
        if (canMove) {
            rb.velocity = aimDir.normalized * shardSpeed;
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer <= 0) {
                KillShard();
            }
        }
    }

    public void Launch() {
        canMove = true;
    }

    private void Update() {
        if (isTargetingPlayer && !canMove) {
            float angle = (Mathf.Atan2(transform.position.x - target.transform.position.x, transform.position.y - target.transform.position.y) * 180 / Mathf.PI + 630) % 360;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angle);

            Vector3 dir = new Vector3(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
            aimDir = dir;
        }

        //transform.localScale = Vector3.Lerp(transform.localScale, size, growthTime * Time.deltaTime);
        //if (transform.localScale.x > maxSize - 0.25f) {

        //}
    }

    IEnumerator Growth(float time) {
        if (time <= 0f) { transform.localScale = size; yield break; }
        float timePassed = 0f;
        transform.localScale = Vector3.zero;
        while (timePassed < time) {
            yield return null;
            timePassed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, size, timePassed / time);
        }
    }

    //private IEnumerator Growth() {
    //    while(transform.localScale.x < maxSize) {
    //        Vector3 growth = new Vector3(transform.localScale.x + growthSpeed * Time.deltaTime, transform.localScale.y + growthSpeed * Time.deltaTime, 1
    //            );
    //        transform.localScale = growth;
    //        yield return null;
    //    }
    //    canMove = true;
    //}

    private void KillShard() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnDisable() {
        Destroy(gameObject);
    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if (collision.gameObject.tag == targetTag) {
    //        collision.gameObject.GetComponent<IHealth>()?.TakeDamage(shardDamage);
    //        _onShardHit?.Invoke(collision.gameObject);
    //        KillShard();
    //    }
    //}

    private void OnDestroy() {
        if (_modDamage != null) {
            _modDamage.OnCollide -= _OnHit;
        }

        _onShardDestroy?.Invoke();
    }

    private void _OnHit(GameObject target) {
        _onShardHit?.Invoke(target);
    }
}
