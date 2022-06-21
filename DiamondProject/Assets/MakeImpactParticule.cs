using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class MakeImpactParticule : MonoBehaviour {
    public static Color _particleColor = Color.yellow;
    [SerializeField] DamageHealth _damageHealth;
    [SerializeField] GameObject _particlesHit;
    [SerializeField] float _particlesHitDelay;
    [SerializeField] static List<WillDieSoon> _particlesHitsUsed = new List<WillDieSoon>();

    private void Start() {
        _damageHealth.OnDamage += _Trigger;
    }

    private void OnDestroy() {
        _damageHealth.OnDamage -= _Trigger;
    }

    private void _Trigger(GameObject collision, int damage) {
        WillDieSoon wds;
        if (_particlesHitsUsed.Count == 0) {
            GameObject newGameObject;
            newGameObject = Instantiate(_particlesHit);
            wds = newGameObject.AddComponent<WillDieSoon>();
        } else {
            wds = _particlesHitsUsed[0];
            _particlesHitsUsed.RemoveAt(0);
        }
        wds.gameObject.SetActive(true);
        wds.SetDelay(_particlesHitDelay)
            .SetColor(_particleColor)
            .SetList(_particlesHitsUsed)
            .SetPosition(collision.transform.root.transform.position)
            .SetRotation((collision.transform.root.transform.position - transform.root.transform.position).normalized);
        wds.IveHit();
    }
}
