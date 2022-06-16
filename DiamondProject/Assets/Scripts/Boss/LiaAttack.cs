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

    [Header("References")]
    [SerializeField] IMeetARealBoss _boss;
    [SerializeField] Health _bossHealth;
    [SerializeField] AttackLauncher _launcher;
    [SerializeField] AttackSystem _attackSystem;
    [Header("Values")]
    [SerializeField] bool _acting = false;
    [SerializeField] float _waitTimeBeforeAction = 5f;
    [SerializeField] float _timeBetweenAttacks = 5f;
    [SerializeField] float _timeBetweenAttacksFALL = 3f;
    [SerializeField] Color _stuntColor = Color.gray;
    [Header("Attacks")]
    [SerializeField] List<string> _neutralAttacks = new List<string>();
    [SerializeField] List<string> _fallAttacks = new List<string>();
    [SerializeField] List<string> _winterAttacks = new List<string>();

    LIAState state = LIAState.ACTION; 
    List<string> availableAttacks = new List<string>();
    Coroutine _currentBehaviour = null;
    BossShape _currentShape = null;
    float _timer = 2f;
    bool _stunt = false;
    string lastAttack = "";
    //string lastAttackEnd;

    public bool CanAct { get => !_stunt && _acting; set => Acting(value); }

    void Start() {
        //_bossHealth.CanTakeDamage = false;
        _timer = _timeBetweenAttacks;
        //if (_acting) {
        //    StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
        //}
    }

    public void Acting(bool state) {
        _acting = state;
    }

    public void ChangeBehaviour() {
        if (_currentBehaviour != null) {
            StopCoroutine(_currentBehaviour);
        }
        _currentBehaviour = ChooseBehaviour();
    }

    public void StopBehaviour() {
        if (_currentBehaviour != null) {
            StopCoroutine(_currentBehaviour);
        }
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

    public void Stunt(float time) {
        _attackSystem.ClearAttacks();
        if (time <= 0f) { return; }
        _stunt = true;
        _boss?.ChangeColor(_stuntColor, time);
        StartCoroutine(Tools.Delay(() => _stunt = false, time));
    }

    public string Attack(params string[] attacksId) {
        if (attacksId.Length == 0) { return "null"; }
        string winner = Tools.Random(attacksId);
        _launcher.LaunchAttack(winner);
        return winner;
    }

    public void ClearAttacks() {
        _attackSystem.ClearAttacks();
    }

    #region Behaviour
    public IEnumerator NeutralBehaviour() {
        availableAttacks.Clear();
        availableAttacks = new List<string>(_neutralAttacks);
        _bossHealth.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
        yield return new WaitForSeconds(_waitTimeBeforeAction);
        float timer = 10f;
        while (true) {
            while (!CanAct) { yield return null; }
            if (timer > 10f) {
                timer = 0f;
                lastAttack = "Movement";
                availableAttacks.Remove(Attack(lastAttack));
                while (!availableAttacks.Contains(lastAttack)) {
                    timer += Time.deltaTime;
                    yield return null;
                }
                while (!CanAct) { yield return null; }
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
        availableAttacks.Clear();
        availableAttacks = new List<string>(_fallAttacks);
        _bossHealth.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
        yield return new WaitForSeconds(_waitTimeBeforeAction);
        Attack("Protector Trees");
        while (true) {
            while (!CanAct) { yield return null; }
            state = LIAState.MOVEMENT;
            lastAttack = "FollowPlayer";
            availableAttacks.Remove(Attack(lastAttack));
            while (!availableAttacks.Contains(lastAttack)) {
                yield return null;
            }
            while (!CanAct) { yield return null; }
            state = LIAState.ACTION;
            availableAttacks.Remove("FollowPlayer");
            lastAttack = Tools.Random(availableAttacks.ToArray());
            availableAttacks.Remove(Attack(lastAttack));
            availableAttacks.Add("FollowPlayer");
            while (!availableAttacks.Contains(lastAttack) && lastAttack != "Boomerang") {
                yield return null;
            }
            while (!CanAct) { yield return null; }
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
        availableAttacks.Clear();
        availableAttacks = new List<string>(_winterAttacks);
        _bossHealth.CanTakeDamage = false;
        StartCoroutine(Tools.Delay(() => _bossHealth.CanTakeDamage = true, _waitTimeBeforeAction));
        yield return new WaitForSeconds(_waitTimeBeforeAction);
        while (true) {
            while (!CanAct) { yield return null; }
            lastAttack = Tools.Random(availableAttacks.ToArray());
            availableAttacks.Remove(Attack(lastAttack));
            while (!availableAttacks.Contains(lastAttack)) {
                yield return null;
            }
        }
    }
    #endregion

    public void MovementEnd(BaseAttack attack) {
        if (attack == null) { return; }

        switch (_currentShape.Type) {
            case Shape.NEUTRAL:
            default:               
                if (_neutralAttacks.Contains(attack.id)){
                    availableAttacks.Add(attack.id);
                }
                break;
            case Shape.FALL:
                if (_fallAttacks.Contains(attack.id)){
                    availableAttacks.Add(attack.id);
                }
                break;
            case Shape.WINTER:
                if (_winterAttacks.Contains(attack.id)){
                    availableAttacks.Add(attack.id);
                }
                break;
        }
    }
}
