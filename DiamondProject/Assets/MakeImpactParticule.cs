using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class MakeImpactParticule : MonoBehaviour {
    [SerializeField] GameObject _particlesHit;
    [SerializeField] float _particlesHitDelay;
    [SerializeField] static List<WillDieSoon> _particlesHitsUsed = new List<WillDieSoon>();

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Boss") {
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
                .SetList(_particlesHitsUsed)
                .SetPosition(collision.gameObject.transform.root.transform.position)
                .SetRotation((collision.gameObject.transform.root.transform.position - transform.root.transform.position).normalized);
            Debug.Log((transform.root.transform.position - transform.position).normalized);
            wds.IveHit();
        }
    }


}
