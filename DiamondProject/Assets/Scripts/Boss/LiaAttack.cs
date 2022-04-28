using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class LiaAttack : MonoBehaviour {
    [SerializeField] BossShape _currentShape;
    [SerializeField] AttackLauncher _launcher;
    [SerializeField] float _waitTimeBeforeAction = 5f;
    [SerializeField] float _timeBetweenAttacks = 5f;

    float _timer = 0f;

    void Start() {
        _timer = _timeBetweenAttacks - _waitTimeBeforeAction;
    }

    void Update() {
        if (_timer < _timeBetweenAttacks) {
            _timer += Time.deltaTime;
        } else {
            switch (_currentShape.Type) {
                case Shape.NEUTRAL:
                    Attack(/*"Ice Hell", "Bramble Ball",*/ "ExploBush");
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
                    Attack("Ice Hell");
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

    public void Attack(params string[] attacksId) {
        if (attacksId.Length == 0) { return; }
        string winner = Tools.Random(attacksId);
        _launcher.LaunchAttack(winner);
    }
}
