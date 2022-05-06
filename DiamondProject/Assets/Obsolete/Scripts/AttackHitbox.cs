//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AttackHitbox : MonoBehaviour {
//    [SerializeField] bool isBullet = false;
//    public int damage = 10;
//    private Vector3 startPos;

//    public ToolsBoxEngine.Tools.BasicDelegate<Collider2D> OnHit;

//    private void Start() {
//        startPos = transform.localPosition;
//    }

//    private void Update() {
//        if (!isBullet) {
//            transform.localPosition = startPos;
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision) {
//        OnHit?.Invoke(collision);

//        BossBody bossBody = collision.gameObject.GetComponent<BossBody>();
//        if (bossBody != null) {
//            bossBody.Health.TakeDamage(damage);
//            if (isBullet) {
//                Destroy(gameObject);
//            }
//            return;
//        }

//        Rock rock = collision.gameObject.GetComponent<Rock>();
//        if (rock != null) {
//            rock.LoseLife(damage);
//            if (isBullet) {
//                Destroy(gameObject);
//            }
//            return;
//        }

//        BossTree tree = collision.gameObject.GetComponent<BossTree>();
//        if (tree != null) {
//            tree.LoseLife(damage);
//            if (isBullet) {
//                Destroy(gameObject);
//            }
//            return;
//        }

//        Missile missile = collision.gameObject.GetComponent<Missile>();
//        if (missile != null) {
//            missile.Die();
//            if (isBullet) {
//                Destroy(gameObject);
//            }
//            return;
//        }
//    }
//}
