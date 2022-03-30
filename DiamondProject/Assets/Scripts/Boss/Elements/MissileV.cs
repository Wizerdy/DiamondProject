using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileV : Missile {
    bool inversed;
    public override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            _player.Instance.Health.TakeDamage(10);
            Die();
        }

        if (collision.gameObject.tag == "Boss" && inversed) {
            collision.gameObject.GetComponentInParent<Health>().TakeDamage(50);
            Die();
        }

        if (collision.gameObject.tag == "Sword") {
            firemissile.counter++;
            if (firemissile.counter == 1 && gameObject.name == "5") {
                inversed = true;
                rotationSpeed = 360;
            }

            direction = new Vector3(transform.position.x - collision.gameObject.transform.position.x, transform.position.y - collision.gameObject.transform.position.y, 0) * speed;
            lifetimeTimer = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Boss" && inversed) {
            collision.gameObject.GetComponentInParent<Health>().TakeDamage(50);
            Die();
        }
    }


    public override void FixedUpdate() {
        lifetimeTimer -= Time.fixedDeltaTime;
        if (lifetimeTimer <= 0) {
            Die();
        }
        if (!inversed) {
            directionTarget = _player.Instance.gameObject.transform.position - transform.position;
        } else {
            directionTarget = bossRef.Instance.gameObject.transform.position - transform.position;
        }
        float directionµSense = 180 <= Vector3.Angle(direction, directionTarget) ? -1 : 1;
        direction = Vector3.RotateTowards(direction, directionTarget, Mathf.Deg2Rad * Time.fixedDeltaTime * rotationSpeed, 1);
        rb.velocity = direction.normalized * speed;
    }
}
