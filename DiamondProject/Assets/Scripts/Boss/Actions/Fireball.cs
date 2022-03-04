using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : BossAction, IAction {
    [SerializeField] MagicBall magicBall = null;
    [SerializeField] MagicBall BigmagicBall = null;
    [SerializeField] float _magicBallSpeed = 1f;
    [SerializeField] float _rotationSpeed = 1f;
    [SerializeField] float _magicBallRate = 1f;
    [SerializeField] float _magicBallDistSpawn = 1f;
    public override void StartAction() {
        //Debug.Log("Fireball");
        _boss.Instance.ChangeState(GetState());
        StartCoroutine(FireMagicBall());
    }
    IEnumerator FireMagicBall() {
        float fireRateTimer = _magicBallRate;
        float durationTimer = _duration;
        SpawnBigMagicBall(transform.up + transform.right, _magicBallSpeed, transform.position + (transform.up + transform.right) * _magicBallDistSpawn, MagicBall.State.WHITE);
        SpawnBigMagicBall((transform.up + transform.right) * -1, _magicBallSpeed, transform.position + (transform.up + transform.right) * _magicBallDistSpawn * -1, MagicBall.State.WHITE);
        SpawnBigMagicBall(transform.up + transform.right * -1, _magicBallSpeed, transform.position + (transform.up + transform.right * -1) * _magicBallDistSpawn, MagicBall.State.WHITE);
        SpawnBigMagicBall((transform.up + transform.right * -1) * -1, _magicBallSpeed, transform.position + (transform.up + transform.right * -1) * _magicBallDistSpawn * -1, MagicBall.State.WHITE);
        while (durationTimer > 0) {
            durationTimer -= Time.deltaTime;
            _boss.Instance.transform.Rotate(new Vector3(0, 0, 360 * _rotationSpeed * Time.deltaTime));
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0) {
                fireRateTimer = _magicBallRate;
                SpawnMagicBall(transform.up, _magicBallSpeed, transform.position + transform.up * _magicBallDistSpawn, MagicBall.State.RED);
                SpawnMagicBall(transform.up * -1, _magicBallSpeed, transform.position + transform.up * _magicBallDistSpawn * -1, MagicBall.State.RED);
                SpawnMagicBall(transform.right, _magicBallSpeed, transform.position + transform.right * _magicBallDistSpawn, MagicBall.State.YELLOW);
                SpawnMagicBall(transform.right * -1, _magicBallSpeed, transform.position + transform.right * _magicBallDistSpawn * -1, MagicBall.State.YELLOW);
            }
            yield return null;
        }
        _boss.Instance.NewNextState("Teleport");
        Wait();
    }
    void SpawnMagicBall(Vector3 direction, float speed, Vector3 position, MagicBall.State state) {
        MagicBall newMagicBall = Instantiate(magicBall.gameObject, position, Quaternion.identity).GetComponent<MagicBall>();
        newMagicBall.SetDirection(direction)
            .SetSpeed(speed)
            .SetState(state);
    }

    void SpawnBigMagicBall(Vector3 direction, float speed, Vector3 position, MagicBall.State state) {
        MagicBall newMagicBall = Instantiate(BigmagicBall.gameObject, position, Quaternion.identity).GetComponent<MagicBall>();
        newMagicBall.SetDirection(direction)
            .SetSpeed(speed)
            .SetState(state);
    }

    public override Boss.State GetState() {
        return Boss.State.FIREBALL;
    }
}