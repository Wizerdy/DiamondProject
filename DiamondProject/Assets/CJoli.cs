using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class CJoli : MonoBehaviour {
    [SerializeField] GameObject _particlesHit;
    [SerializeField] float _particlesHitDelay;
    [SerializeField] List<GameObject> _particlesHitsUsed = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Boss") {
            IveHit(collision.ClosestPoint(transform.position), ((Vector3)collision.ClosestPoint(transform.position) - transform.position).normalized * 360f);
        }
    }


    public void IveHit(Vector3 impact, Vector3 directionImpact) {
        ParticleSystem newParticles;
        GameObject parent;
        if (_particlesHitsUsed.Count == 0) {
            parent = Instantiate(_particlesHit, impact, Quaternion.Euler(directionImpact));
        } else {
            parent = _particlesHitsUsed[0];
            parent.transform.position = impact;
            parent.transform.rotation = Quaternion.Euler(directionImpact);
            parent.gameObject.SetActive(true);
            _particlesHitsUsed.RemoveAt(0);
        }
        newParticles = parent.transform.GetChild(0).GetComponent<ParticleSystem>();
        newParticles.Clear();
        newParticles.Play();
        StartCoroutine(Tools.Delay(WillDiesooooon, parent, _particlesHitDelay));
    }

    public void WillDiesooooon(GameObject particule) {
        particule.SetActive(false);
        _particlesHitsUsed.Add(particule);
    }
}
