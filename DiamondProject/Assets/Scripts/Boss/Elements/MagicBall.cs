using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour {

    public Vector3 direction = Vector3.zero;
    public float speed = 1;
    SpriteRenderer sr = null;
    Rigidbody2D rb = null;
    public enum State {
        RED,
        YELLOW,
        WHITE
    }
    State state;

    #region Builder

    public MagicBall SetDirection(Vector3 direction) {
        this.direction = direction;
        return this;
    }

    public MagicBall SetSpeed(float speed) {
        this.speed = speed;
        return this;
    }

    public MagicBall SetState(MagicBall.State state) {
        this.state = state;
        return this;
    }

    #endregion
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        sr = GetComponent<SpriteRenderer>();
        if (state == State.RED) {
            sr.color = Color.red;
        } else if (state == State.YELLOW) {
            sr.color = Color.yellow;
        } else if (state == State.WHITE) {
            sr.color = Color.white;
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b,0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerController touchais = collision.gameObject.GetComponent<PlayerController>();
            if (state == State.RED) {
                if (touchais.Movement.IsMoving) {
                    touchais.Health.TakeDamage(20);
                    Die();
                }
            } else if (state == State.YELLOW) {
                if (!touchais.Movement.IsMoving) {
                    touchais.Health.TakeDamage(20);
                    Die();
                }
            } else {
                touchais.Health.TakeDamage(50);
                Die();
            }

        }
        if (collision.gameObject.tag == "Wall") {
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
