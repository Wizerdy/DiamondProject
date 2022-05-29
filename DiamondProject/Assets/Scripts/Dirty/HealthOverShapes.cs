using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class HealthOverShapes : MonoBehaviour {
    [System.Serializable]
    struct HealthByShape {
        public Shape shape;
        public HUDHealth hud;

        public HealthByShape(Shape shape, HUDHealth hud) {
            this.shape = shape;
            this.hud = hud;
        }
    }

    [Header("Reference")]
    [SerializeField] BossShapeSystemReference _shapeSystem;
    [SerializeField] HUDHealth _baseHealth;
    [SerializeField] List<HealthByShape> _otherHealth;
    [Header("Options")]
    [SerializeField] int _healthLoseOnShapeChange = 500;

    Dictionary<Shape, HUDHealth> _shapeHealth;
    HUDHealth _saveOwner;
    HealthData _save = null;

    #region Editor

    private void Reset() {
        _otherHealth = new List<HealthByShape>();
        _otherHealth.Add(new HealthByShape(Shape.FALL, null));
        _otherHealth.Add(new HealthByShape(Shape.WINTER, null));
        UpdateDictionnary();
    }

    private void OnValidate() {
        UpdateDictionnary();
    }

    private void UpdateDictionnary() {
        _shapeHealth = new Dictionary<Shape, HUDHealth>();
        for (int i = 0; i < _otherHealth.Count; i++) {
            _shapeHealth.Add(_otherHealth[i].shape, _otherHealth[i].hud);
        }
    }

    #endregion

    void Start() {
        _baseHealth.gameObject.SetActive(true);
        foreach (KeyValuePair<Shape, HUDHealth> hud in _shapeHealth) {
            hud.Value.gameObject.SetActive(false);
            hud.Value.Attach();
            hud.Value.Active = false;
        }

        if (!_shapeSystem.IsValid()) { return; }
        _shapeSystem.Instance.OnExitShape += _OnExitShape;
        _shapeSystem.Instance.OnEnterShape += _OnEnterShape;
    }

    public void EnableBar(HUDHealth hud) {
        if (_saveOwner == hud) {
            LoadSaveTo(hud);
        }
        hud.Active = true;
        hud.UpdateHUD(0);
    }

    public void LoadSaveTo(HUDHealth hud) {
        hud.HealthReference.Load(_save);
        _save = null;
        _saveOwner = null;
    }

    public void FreezeBar(HUDHealth hud) {
        hud.Active = false;
        if (hud.HealthReference != null) {
            _save = hud.HealthReference.Save();
            _saveOwner = hud;
        }
    }

    private void _OnExitShape(BossShape shape) {
        switch (shape.Type) {
            case Shape.NEUTRAL:
                FreezeBar(_baseHealth);
                break;
            default:
                _shapeHealth[shape.Type].Active = false;
                _shapeHealth[shape.Type].gameObject.SetActive(false);
                if (_save != null) { _save.currentHealth -= _healthLoseOnShapeChange; }
                break;
        }
    }

    private void _OnEnterShape(BossShape shape) {
        switch (shape.Type) {
            case Shape.NEUTRAL:
                EnableBar(_baseHealth);
                break;
            default:
                _shapeHealth[shape.Type].HealthReference.Percentage = 1f;
                _shapeHealth[shape.Type].gameObject.SetActive(true);
                EnableBar(_shapeHealth[shape.Type]);
                break;
        }
    }
}
