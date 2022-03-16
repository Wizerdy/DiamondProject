using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

[System.Serializable]
public class DamageHealth : MonoBehaviour {
    [SerializeField] int _damage = 5;
    [SerializeField] MultipleTagSelector _damageables;
    [SerializeField] bool _destroyOnHit;
    Tools.BasicDelegate<GameObject> _onCollide;

    #region Properties

    public int Damage { get { return _damage; } set { _damage = value; } }
    public MultipleTagSelector Damageables { get { return _damageables; } set { _damageables = value; } }
    public event Tools.BasicDelegate<GameObject> OnCollide { add { _onCollide += value; } remove { _onCollide -= value; } }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision) {
        Collide(collision.collider, true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Collide(collision);
    }

    private void Collide(Collider2D collision, bool hardHit = false) {
        if (_damageables.Contains(collision.gameObject.tag)) {
            collision.gameObject.GetComponent<IHealth>()?.TakeDamage(_damage);
            _onCollide?.Invoke(collision.gameObject);
            if (_destroyOnHit) {
                Die();
            }
            return;
        }

        if (hardHit && _destroyOnHit) {
            _onCollide?.Invoke(collision.gameObject);
            Die();
        }
    }

    public void Die() {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
