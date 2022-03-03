using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public int life = 10;

    [Header("Movement")]
    public float speed;
    public float speedMultiplier;
    //public float jumpDuration;

    protected Vector2 direction;

    protected float TimeDelta { get { return Time.deltaTime; } }


    public virtual void Move()
    {
        transform.position += new Vector3(direction.x * speed * TimeDelta, direction.y * speed * TimeDelta, transform.position.z);
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
