using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using ToolsBoxEngine;

public class FallingThunder : MonoBehaviour {
    [Header("References")]
    [SerializeField] DamageHealth _damageHealth = null;
    [SerializeField] VisualEffect _lightning = null;

    [Header("Parameters")]
    [SerializeField] float _height = 0f;
    [SerializeField] float _delay = 1f;

    Vector3 _target;
    int _damage = 10;

    public void Init(int damage, float delay) {
        _target = transform.position;
        _damageHealth?.SetDamage(_damage);
        StartCoroutine(Tools.Delay(DoDamage, _delay, delay));
    }

    private void Start() {
        _damageHealth.gameObject.SetActive(false);
        _lightning.gameObject.SetActive(false);
        _lightning.transform.position = transform.position.Override(_height);
        _lightning?.SetFloat("Height", transform.position.y);
    }

    private void DoDamage(float time) {
        StartCoroutine(CollisionTime(time));

        IEnumerator CollisionTime(float time) {
            _damageHealth.gameObject.SetActive(true);
            _lightning.gameObject.SetActive(true);
            yield return new WaitForSeconds(time);
            _damageHealth.gameObject.SetActive(false);
            _lightning.gameObject.SetActive(false);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
