using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour {

    public Vector3 direction;
    public float speed;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public enum State {
        RED,
        YELLOW
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
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            PlayerController touchais = collision.gameObject.GetComponent<PlayerController>();
            if (state == State.RED) {
                touchais.TakeDamage(-1);
            } else if (state == State.YELLOW) {
                if (touchais.IsMoving()) {
                    touchais.TakeDamage(-1);
                }
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
