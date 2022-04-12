using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour {
    public enum State { TRANSITION, FORMSWITCH, TELEPORT, ROCKFALL, FIREMISSILE, FIREBALL, FIREBOTH, EXPLOSIVROCKFALL, DISMANTLE, FISSURE, GUARDIANSEED }
    public enum Form { PASSIVE = 0, FIRST = 1, SECOND = 2, DEAD = 3 }

    [SerializeField] private State currentState = State.TRANSITION;
    public Form form = Form.FIRST;

    [SerializeField] MonoBossActionSelectorRandom _currentForm;
    [SerializeField] MonoBossActionSelectorRandom _allActions;
    [SerializeField] List<BossAction> bossActionsCoroutines = new List<BossAction>();
    public List<GameObject> todestroyondeath = new List<GameObject>();
    public UnityAction die;

    public State CurrentState { get { return currentState; } }

    public void StartIn(float time) {
        StartCoroutine(Tools.Delay(NextState, "Random", -1f, time));
    }

    private void Start() {
        StartIn(5f);
        //NextState();
        die = () => Die();
        gameObject.GetComponent<Health>().OnDeath += die;
        //NextState();
    }

    #region State
    public void NextState(string nextState = "Random", float duration = -1) {
        BossAction action;
        switch (nextState) {
            case "Random":
                action = _currentForm.Get();
                if (action == null) {
                    StartCoroutine(RetryNextState(1));
                }
                break;
            default:
                action = _allActions.Get(nextState);
                break;
        }
        if (action != null) {
            action.StartAction();
            bossActionsCoroutines.Add(action);
        }
    }
    public void StopActions() {
        if (currentState == State.TELEPORT || currentState == State.FORMSWITCH) { return; }
        for (int i = 0; i < bossActionsCoroutines.Count; i++) {
            Debug.Log(bossActionsCoroutines[i]);
            bossActionsCoroutines[i].StopAllCoroutines();
        }
        bossActionsCoroutines.Clear();

    }

    public void ChangeState(State state) {
        //Debug.Log("NewState: " + state);
        currentState = state;
    }

    public void NewWeightAction(BossAction action, float weight) {
        _currentForm.NewWeight(action, weight);
    }

    public void RemoveCoroutines(BossAction bossaction) {
        if (bossActionsCoroutines.Contains(bossaction))
            bossActionsCoroutines.Remove(bossaction);
    }

    public IEnumerator RetryNextState(float delay) {
        yield return new WaitForSeconds(delay);
        NextState("Random");
    }
    #endregion

    public void Die() {
        for (int i = 0; i < todestroyondeath.Count; i++) {
            Destroy(todestroyondeath[i]);
        }
        Destroy(gameObject);
    }
}