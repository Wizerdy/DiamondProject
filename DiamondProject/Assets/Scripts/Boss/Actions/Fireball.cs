using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : BossAction, IAction {
    [SerializeField] MagicBall magicBall = null;
    [SerializeField] float _magicBallSpeed = 1f;
    [SerializeField] float _rotationSpeed = 1f;
    [SerializeField] float _magicBallRate = 1f;
    [SerializeField] float _magicBallDistSpawn = 1f;
    public override IEnumerator StartAction() {
        Debug.Log("Fireball");
        _boss.Instance.ChangeState(GetState());
        float fireRateTimer = _magicBallRate;
        float durationTimer = _duration;
        while (durationTimer > 0) {
            durationTimer -= Time.deltaTime;
            _boss.Instance.transform.Rotate(new Vector3(0, 0, 360 * _rotationSpeed * Time.deltaTime));
            fireRateTimer -= Time.deltaTime;
            if (fireRateTimer <= 0) {
                fireRateTimer = _magicBallRate;
                Debug.Log(transform.position);
                Debug.Log(transform.right);
                Debug.Log( _magicBallDistSpawn);
                SpawnMagicBall(transform.right, _magicBallSpeed, transform.position + transform.right * _magicBallDistSpawn, MagicBall.State.RED);
                SpawnMagicBall(transform.right * -1, _magicBallSpeed, transform.position + transform.right * _magicBallDistSpawn * -1, MagicBall.State.YELLOW);
            }
            yield return null;
        }
        _boss.Instance.EndState(transitionTime);
    }

    void SpawnMagicBall(Vector3 direction, float speed, Vector3 position, MagicBall.State state) {
        MagicBall newMagicBall = Instantiate(magicBall.gameObject, position, Quaternion.identity).GetComponent<MagicBall>();
        newMagicBall.SetDirection(direction)
            .SetSpeed(speed)
            .SetState(state);
    }

    public override Boss.State GetState() {
        return Boss.State.FIREBALL;
    }
}
