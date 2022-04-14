using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    [SerializeField] protected float duration = 5;
    [SerializeField] protected float coolDown = 1;
    [SerializeField] protected bool isPlaying = false;

    [SerializeField] protected Player player;
    [SerializeField] protected Boss boss;

    protected abstract IEnumerator Launch();
    //protected abstract IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration);

    protected virtual void UpdateIA() {
        //IAcooldDown = coolDown;
    }
}
