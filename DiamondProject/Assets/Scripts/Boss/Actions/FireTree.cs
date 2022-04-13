using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTree : MonoBehaviour
{
    [SerializeField] private float treeHp = 10f;
    [SerializeField] private float treeDamage = 10f;
    [SerializeField] private float fireDamage = 10f;
    [SerializeField] private float fireDamageFrequency = 1f;
    [SerializeField] private float fireRange = 5f;

    private Player player;
    private Rigidbody2D rb;
    private float timer = 0f;

    public void Init(Player _player, int _hp, float _damage, float _fireDamage, float _frequency, float _fireRadius) {
        player = _player;
        treeHp = _hp;
        treeDamage = _damage;
        fireDamage = _fireDamage;
        fireDamageFrequency = _frequency;
        fireRange = _fireRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = fireDamageFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            float distance = Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2));
            if (distance <= fireRange)
                Debug.Log("took "+ fireDamage + " fire damage");
                //player.TakeDamage(damage);

            timer = fireDamageFrequency;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            //player.TakeDamage(damage);
            Debug.Log("Player took " + treeDamage + " damage");
        }
    }

    private void TakeDamage(float damage) {
        treeHp = treeHp - damage;
        if (treeHp <= 0) {
            Destroy(this);
        }
    }
}
