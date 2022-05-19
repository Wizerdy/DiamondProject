using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class LiaAttack : MonoBehaviour {
    enum LIAState {
        MOVEMENT,
        ACTION,
        KO
    }

    [SerializeField] Health _bossHealth;
    [SerializeField] BossShape _currentShape;
    [SerializeField] AttackLauncher _launcher;
    [SerializeField] float _waitTimeBeforeAction = 5f;
    [SerializeField] float _timeBetweenAttacks = 5f;
    [SerializeField] Coroutine _currentBehaviour;
    [Header("Behaviour")]
    [SerializeField] LIAState state = LIAState.ACTION; 
    [SerializeField] List<string> availableAttacks;
    [Space]
    [Header("FALL")]
    [SerializeField] float _timeBetweenAttacksFALL = 3f;
    [SerializeField] List<string> _neutralAttacks = new List<string>();
    [SerializeField] List<string> _fallAttacks = new List<string>();
    [SerializeField] List<string> _winterAttacks = new List<string>();

    float _timer = 2f;
    [SerializeField] string lastAttack;
    [SerializeField] string lastAttackEnd;

    void Start() {
        _bossHealth.CanTakeDamage = false;
        _timer = _timeBetweenAttacks;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
    }

    public void ChangeBehaviour() {
        if (_currentBehaviour != null) {
            StopCoroutine(_currentBehaviour);
        }
        _currentBehaviour = ChooseBehaviour();
    }

    Coroutine ChooseBehaviour() {
        switch (_currentShape.Type) {
            case Shape.NEUTRAL:
            default:
                return StartCoroutine(NeutralBehaviour());
            case Shape.SPRING:
                return StartCoroutine(SpringBehaviour());
            case Shape.SUMMER:
                return StartCoroutine(SummerBehaviour());
            case Shape.FALL:
                return StartCoroutine(FallBehaviour());
            case Shape.WINTER:
                return StartCoroutine(WinterBehaviour());
        }
    }

    public void SetShape(BossShape shape) {
        _currentShape = shape;
    }

    public void WaitTime(float time) {
        _timer -= time;
    }

    public string Attack(params string[] attacksId) {
        if (attacksId.Length == 0) { return "null"; }
        string winner = Tools.Random(attacksId);
        _launcher.LaunchAttack(winner);
        return winner;
    }

    #region Behaviour
    public IEnumerator NeutralBehaviour() {
        availableAttacks = _neutralAttacks;
        _bossHealth.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
        yield return new WaitForSeconds(_waitTimeBeforeAction);
        float timer = 10f;
        while (true) {
            if(timer > 10f) {
                timer = 0f;
                lastAttack = "Movement";
                availableAttacks.Remove(Attack(lastAttack));
                while (!availableAttacks.Contains(lastAttack)) {
                    timer += Time.deltaTime;
                    yield return null;
                }
            }    
            availableAttacks.Remove("Movement");
            lastAttack = Tools.Random(availableAttacks.ToArray());
            availableAttacks.Remove(Attack(lastAttack));
            availableAttacks.Add("Movement");
            while (!availableAttacks.Contains(lastAttack)) {
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    public IEnumerator SpringBehaviour() {
        while (true) {
            yield return new WaitForSeconds(_timer);
            Attack("Bramble Ball");
        }
    }

    public IEnumerator SummerBehaviour() {
        while (true) {
            yield return new WaitForSeconds(_timer);
            Attack("Trees");
        }
    }
    public IEnumerator FallBehaviour() {
        availableAttacks = _fallAttacks;
        _bossHealth.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
        //yield return new WaitForSeconds(_waitTimeBeforeAction);
        Attack("Protector Trees");
        while (true) {
            state = LIAState.MOVEMENT;
            lastAttack = "FollowPlayer";
            availableAttacks.Remove(Attack(lastAttack));
            while (!availableAttacks.Contains(lastAttack)) {
                yield return null;
            }
            state = LIAState.ACTION;
            availableAttacks.Remove("FollowPlayer");
            lastAttack = Tools.Random(availableAttacks.ToArray());
            availableAttacks.Remove(Attack(lastAttack));
            availableAttacks.Add("FollowPlayer");
            while (!availableAttacks.Contains(lastAttack) && lastAttack != "Boomerang") {
                yield return null;
            }
            float random = Random.Range(0, 99);
            if(random < 40) {
                lastAttack = "FallDash";
                availableAttacks.Remove(Attack(lastAttack));
                while (!availableAttacks.Contains(lastAttack)) {
                    yield return null;
                }
            }
        }
    }

    public IEnumerator WinterBehaviour() {
        availableAttacks = _winterAttacks;
        _bossHealth.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
        yield return new WaitForSeconds(_waitTimeBeforeAction);
        while (true) {
            lastAttack = Tools.Random(availableAttacks.ToArray());
            availableAttacks.Remove(Attack(lastAttack));
            while (!availableAttacks.Contains(lastAttack)) {
                yield return null;
            }
        }
    }
    #endregion

    public void MovementEnd(BaseAttack attack) {
        if (attack == null) {
            return;
        }
        availableAttacks.Add(attack.id);
    }
}
