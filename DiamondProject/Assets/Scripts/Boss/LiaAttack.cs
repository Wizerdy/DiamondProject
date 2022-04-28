using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class LiaAttack : MonoBehaviour {
    [SerializeField] Health _bossHealth;
    [SerializeField] BossShape _currentShape;
    [SerializeField] AttackLauncher _launcher;
    [SerializeField] float _waitTimeBeforeAction = 5f;
    [SerializeField] float _timeBetweenAttacks = 5f;

    float _timer = 0f;

    void Start() {
        _bossHealth.CanTakeDamage = false;
        _timer = _timeBetweenAttacks - _waitTimeBeforeAction;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
    }

    void Update() {
        if (_timer < _timeBetweenAttacks) {
            _timer += Time.deltaTime;
        } else {
            switch (_currentShape.Type) {
                case Shape.NEUTRAL:
                    Attack("Bullet Hell", "Leaf Beam", "ExploBush", "Trees");
                    break;
                case Shape.SPRING:
                    Attack("Bramble Ball");
                    break;
                case Shape.SUMMER:
                    Attack("Trees");
                    break;
                case Shape.FALL:
                    Attack("Leaf Beam");
                    break;
                case Shape.WINTER:
                    Attack("Ice Hell", "Bullet Hell", "Snow Absorption");
                    break;
                default:
                    break;
            }
            _timer = 0f;
        }
    }

    public void SetShape(BossShape shape) {
        _currentShape = shape;
    }

    public void WaitTime(float time) {
        _timer -= time;
    }

    public void Attack(params string[] attacksId) {
        if (attacksId.Length == 0) { return; }
        string winner = Tools.Random(attacksId);
        _launcher.LaunchAttack(winner);
    }
}
