using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBeam : BaseAttack
{
    [SerializeField] Reference<Transform> _target;
    [SerializeField] private float raySpeed = 10f;
    [SerializeField] private int rayDamage = 5;
    [SerializeField] private float damageFrequency = 1f;
    [SerializeField] private float raySpeedIfFar = 75f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private GameObject leafBeam;

    //protected override IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration) {

    //SpawnTree(Player player, aimPosition) {
    private void ShootBeam() {
        GameObject beam = Instantiate(leafBeam, transform.position, Quaternion.identity);
        beam.GetComponent<LeafBeamEntity>().Init(_target?.Instance, raySpeed, rayDamage, damageFrequency, duration, raySpeedIfFar, distance);
    }

    protected override IEnumerator IExecute() {
        isPlaying = true;
        ShootBeam();
        //UpdateIA();
        isPlaying = false;
        yield return null;
    }
}
