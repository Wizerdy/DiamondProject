using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrambleBall : BaseAttack {
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 20f;
    [SerializeField] private float delayBetweenLaunch = 1f;

    //[SerializeField] private int maxNumberOfBalls = 3;

    [SerializeField] private GameObject brambleBallEntity;

    public int numberOfBallsToLaunch = 3;

    //private void LaunchBall(Player player, Vector3 aimPosition) {
    private void LaunchBall(Vector3 pos) {
        GameObject bramble = Instantiate(brambleBallEntity, transform.position, Quaternion.identity);
        bramble.GetComponent<BrambleBallEntity>().Init(speed, damage, pos);
    }
    
    //protected override IEnumerator Launch(Player player, Boss boss, Vector3 aimPosition, float duration) {
    protected override IEnumerator Launch() {
        isPlaying = true;

        float launchRate = delayBetweenLaunch;
        //int numberOfBallsToLaunch = Random.Range(1, maxNumberOfBalls);
        int numberOfBallsLaunched = 0;

        while (numberOfBallsLaunched < numberOfBallsToLaunch) {
            launchRate -= Time.deltaTime;
            if(launchRate <= 0) {
                //LaunchBall(player, aimPosition);
                LaunchBall(player.transform.position);

                launchRate = delayBetweenLaunch;
                ++numberOfBallsLaunched;
            }
            yield return null;
        }

        UpdateIA();
        isPlaying = false;
    }
}
