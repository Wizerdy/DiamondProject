using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour {
    [SerializeField] List<string> _triggerables = new List<string>();
    [SerializeField] bool _exitOnEnteringNewTrigger = false;
    [SerializeField] int _priority = 0;

    public bool ExitOnEnteringNewTrigger => _exitOnEnteringNewTrigger;
    public int Priority => _priority;

    #region Unity Callbacks

    private void OnCollisionEnter2D(Collision2D collision) {
        OnEnterArea(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        OnExitArea(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnEnterArea(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        OnExitArea(collision.gameObject);
    }

    #endregion

    public void OnEnterArea(GameObject target) {
        EntityTriggerAreas entity = target.GetComponent<EntityTriggerAreas>();
        if (entity == null) { return; }
        if (!TriggerableBy(entity.Groups)) { return; }

        entity.EnterArea(this);
    }

    public void OnExitArea(GameObject target) {
        EntityTriggerAreas entity = target.GetComponent<EntityTriggerAreas>();
        if (entity == null) { return; }
        if (!TriggerableBy(entity.Groups)) { return; }

        entity.ExitArea(this);
    }

    public void Enter(EntityTriggerAreas entity) {

    }

    public void Exit(EntityTriggerAreas entity) {

    }

    public bool TriggerableBy(List<string> groups) {
        if (_triggerables.Count <= 0) { return true; }
        if (groups.Count <= 0) { return false; }

        for (int i = 0; i < groups.Count; i++) {
            if (_triggerables.Contains(groups[i])) {
                return true;
            }
        }
        return false;
    }
}
