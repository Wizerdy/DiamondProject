using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public int life = 10;

    [Header("Movement")]
    public float speed;
    public float speedMultiplier;
    public AnimationCurve jumpCurve;
    public float jumpMultiplier;
    //public float jumpDuration;

    protected float direction;
    protected Coroutine onJump;

    protected float TimeDelta { get { return Time.deltaTime; } }


    public virtual void Move() {
        transform.position += new Vector3(direction * speed * TimeDelta, transform.position.y, transform.position.z);
    }

    public virtual void Jump() {
        if (onJump == null) {
            onJump = StartCoroutine(OnJump(jumpCurve[jumpCurve.length - 1].time + 0.1f));
        }
    }

    protected virtual IEnumerator OnJump(float duration) {
        float elapsed = 0.0f;

        while (elapsed < duration) {
            float y = jumpMultiplier * jumpCurve.Evaluate(elapsed);

            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);

            elapsed += TimeDelta;

            yield return null;
        }
        onJump = null;
    }

    public void LoseLife(int life) {
        this.life -= life;
        if (life <= 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }
}
