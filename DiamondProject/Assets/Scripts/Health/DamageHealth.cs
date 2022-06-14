using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ToolsBoxEngine;

[System.Serializable]
public class DamageHealth : MonoBehaviour {
    [SerializeField] int _damage = 5;
    [SerializeField] MultipleTagSelector _damageables;
    [SerializeField] bool _destroyOnHit;
    [SerializeField] bool _onlyDamageOnceEach = false;
    [SerializeField] string _damageType;
    [SerializeField] MultipleTagSelector _ignoreTag;

    [SerializeField, HideInInspector] UnityEvent<GameObject> _onCollide;
    [SerializeField, HideInInspector] UnityEvent<GameObject> _onTrigger;
    [SerializeField, HideInInspector] UnityEvent<GameObject, int> _onDamage;
    
    List<GameObject> _hitted = new List<GameObject>();

    #region Properties

    public int Damage { get { return _damage; } set { _damage = value; } }
    public string DamageType { get { return _damageType; } set { _damageType = value; } }
    public MultipleTagSelector Damageables { get { return _damageables; } set { _damageables = value; } }

    public event UnityAction<GameObject> OnCollide { add { _onCollide.AddListener(value); } remove { _onCollide.RemoveListener(value); } }
    public event UnityAction<GameObject> OnTrigger { add { _onTrigger.AddListener(value); } remove { _onTrigger.RemoveListener(value); } }
    public event UnityAction<GameObject, int> OnDamage { add { _onDamage.AddListener(value); } remove { _onDamage.RemoveListener(value); } }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision) {
        Collide(collision.gameObject, !collision.collider.isTrigger);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Collide(collision.gameObject, !collision.isTrigger);
    }

    private void Collide(GameObject obj, bool hardHit = true) {
        if (_ignoreTag.Contains(obj.tag)) { return; }

        if (_damageables.Contains(obj.tag)) {
            GameObject elderly = obj.transform.FindElderlyByTag().gameObject;
            if (_onlyDamageOnceEach && _hitted.Contains(elderly)) { return; }
            _hitted.Add(elderly);
            _onCollide?.Invoke(obj);
            IHealth health = obj.GetComponent<IHealth>();
            if (health != null && health.CanTakeDamage) {
                health.TakeDamage(_damage, _damageType);
                _onDamage?.Invoke(obj, _damage);
            }
            if (_destroyOnHit) {
                Die();
            }
            return;
        }

        if (!hardHit) { _onTrigger?.Invoke(obj); }
        else if (hardHit) { _onCollide?.Invoke(obj); }

        if (hardHit && _destroyOnHit) {
            Die();
        }
    }

    public void ResetHitted() {
        _hitted.Clear();
    }

    public void Die() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void SetValues(MultipleTagSelector damageables, int damage) {
        if (damageables != null) {
            _damageables = damageables;
        }
        _damage = damage;
        ResetHitted();
    }

    public void SetDamage(int damage) {
        _damage = damage;
    }

    public void NotEditable() {
        this.hideFlags = HideFlags.NotEditable;
    }

    public void Hit(Collider2D collision) {
        Collide(collision.gameObject, !collision.isTrigger);
    }
}

