using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class BossTree : BossEntities { //obsolete
    [SerializeField] Reference<Boss> _boss;
    //[SerializeField] TreeShield treeShield;
    SpriteRenderer _sr;
    CircleCollider2D _cc;
    public int life = 0;
    public Vector3 destination = Vector3.zero;
    public float apparitionTime = 2f;
    public float zoneDamageTime = 2f;
    [SerializeField] GameObject _zoneDamage;
    [SerializeField] GameObject _apparition;


    #region Builder

    public BossTree SetDestination(Vector3 destination) {
        this.destination = destination;
        return this;
    }

    public BossTree SetApparitionTime(float apparitionTime) {
        this.apparitionTime = apparitionTime;
        return this;
    }

    public BossTree SetZoneDamageTime(float zoneDamageTime)
    {
        this.zoneDamageTime = zoneDamageTime;
        return this;
    }

    public BossTree SetLife(int life) {
        this.life = life;
        return this;
    }
    #endregion

    private void Start() {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 0);
        _cc = GetComponent<CircleCollider2D>(); 
        _cc.enabled = false;
       // treeShield.AddTree(this);
        transform.position = destination;
        _apparition.SetActive(true);
        StartCoroutine(Spawning());
    }
    IEnumerator Spawning() {
        float timer = apparitionTime;
        yield return new WaitForSeconds(timer);
        Spawn();
    }
    void Spawn() {
        _apparition.SetActive(false);
        _zoneDamage.SetActive(true);
        _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b, 255);
        _cc.enabled = true;
        StartCoroutine(Tools.Delay(() => _zoneDamage.SetActive(false), zoneDamageTime));
    }
    void Die() {
     //   treeShield.RemoveTree(this);
        Destroy(gameObject);
    }

    public void LoseLife(int life) {
        this.life -= life;
        if (this.life <= 0) {
            Die();
        }
    }

    //public void AddTreeShield(TreeShield treeShield) {
    //    this.treeShield = treeShield;
    //}
}
