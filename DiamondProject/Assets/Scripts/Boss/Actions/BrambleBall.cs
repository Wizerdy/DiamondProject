using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrambleBall : BaseAttack {
    [SerializeField] private Player _player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float delayBetweenLaunch = 1f;

    [SerializeField] private int maxNumberOfBalls = 3;

    [SerializeField] private BrambleBallEntity brambleBallEntity;

    //private void LaunchBall(Player player, Vector3 aimPosition) {
    private void LaunchBall(Vector3 aimPosition) {
        BrambleBallEntity bramble = Instantiate(brambleBallEntity, transform.position, Quaternion.identity);
        bramble.Init(speed, damage, aimPosition);
    }
    
    //protected override IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration) {
    protected override IEnumerator IExecute() {
        float launchRate = delayBetweenLaunch;
        int numberOfBallsToLaunch = Random.Range(1, maxNumberOfBalls);
        int numberOfBallsLaunched = 0;

        while (numberOfBallsLaunched > numberOfBallsToLaunch) {
            launchRate -= Time.deltaTime;
            if(launchRate <= 0) {
                //LaunchBall(player, aimPosition);
                LaunchBall(_player.gameObject.transform.position);

                launchRate = delayBetweenLaunch;
                ++numberOfBallsLaunched;
            }
            yield return null;
        }
    }
}
