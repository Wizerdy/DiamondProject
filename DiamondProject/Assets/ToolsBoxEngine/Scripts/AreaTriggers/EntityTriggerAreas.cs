using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTriggerAreas : MonoBehaviour {
    [SerializeField] List<string> _groups = new List<string>();
    [SerializeField] Stack<AreaTrigger> _currentAreas = new Stack<AreaTrigger>();

    public List<string> Groups => _groups;

    public void EnterArea(AreaTrigger area) {
        if (_currentAreas.Count > 0) { // Sors de la dernière Area
            AreaTrigger lastArea = _currentAreas.Peek();
            if (lastArea.ExitOnEnteringNewTrigger) {
                lastArea.Exit(this);
            }
        }

        area.Enter(this);
        _currentAreas.Push(area);
    }

    public void ExitArea(AreaTrigger area) {
        _currentAreas.Pop().Exit(this);

        if (_currentAreas.Count > 0) { // Rentre dans la dernière Area
            AreaTrigger newArea = _currentAreas.Peek();
            if (newArea.ExitOnEnteringNewTrigger) {
                newArea.Enter(this);
            }
        }
    }
}
