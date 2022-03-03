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
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            TempPlayerController touchais = collision.gameObject.GetComponent<TempPlayerController>();
            if (state == State.RED) {
                if (touchais.IsMoving) {
                    touchais.TakeDamage(2);
                    Die();
                }
            } else if (state == State.YELLOW) {
                if (!touchais.IsMoving) {
                    touchais.TakeDamage(2);
                    Die();
                }
            } else {
                touchais.TakeDamage(5);
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
