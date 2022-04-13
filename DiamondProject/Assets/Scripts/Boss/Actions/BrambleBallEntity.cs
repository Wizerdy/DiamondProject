using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrambleBallEntity : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 20f;

    [SerializeField] delegate void OnPlayerHitEvent();
    OnPlayerHitEvent onPlayerHitEvent;
    [SerializeField] delegate void OnSpawnEvent();
    OnSpawnEvent onSpawnEvent;

    //private Player player;
    private Vector3 aimPosition;
    private Rigidbody2D rb;

    public void Init(float _speed, float _damage, Vector3 _aimPosition) {
        speed = _speed;
        damage = _damage;
        aimPosition = _aimPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        onPlayerHitEvent += OnPlayerHit;
        onSpawnEvent += OnSpawn;

        rb = GetComponent<Rigidbody2D>();


        onPlayerHitEvent?.Invoke();
    }

    private void FixedUpdate() {
        rb.velocity = aimPosition.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            //player.TakeDamage(damage);
            Debug.Log("Player took " + damage + " damage");
            onPlayerHitEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnSpawn() {

    }

    private void OnPlayerHit() {

    }

    private void OnDestroy() {
        onPlayerHitEvent -= OnPlayerHit;
        onSpawnEvent -= OnSpawn;
    }
}
