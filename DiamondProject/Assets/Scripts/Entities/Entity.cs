using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
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
}
