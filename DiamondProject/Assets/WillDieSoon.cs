using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsBoxEngine;

public class WillDieSoon : MonoBehaviour {
    [SerializeField] static List<WillDieSoon> _particlesHitsUsed;
    [SerializeField] GameObject _particlesHit;
    [SerializeField] float _particlesHitDelay;
    [SerializeField] Color _color;

    public WillDieSoon SetParticule(GameObject particule) {
        _particlesHit = particule;
        return this;
    }

    public WillDieSoon SetColor(Color color) {
        _color = color;
        return this;
    }

    public WillDieSoon SetDelay(float delay) {
        _particlesHitDelay = delay;
        return this;
    }

    public WillDieSoon SetPosition(Vector3 position) {
        transform.position = position;
        return this;
    }

    public WillDieSoon SetRotation(Vector3 rotation) {
        transform.LookAt(transform.position + rotation);
        return this;
    }

    public WillDieSoon SetList(List<WillDieSoon> list) {
        if (_particlesHitsUsed == null) {
            _particlesHitsUsed = list;
        }
        return this;
    }

    public void IveHit() {
        ParticleSystem newParticles = transform.GetChild(0).GetComponent<ParticleSystem>();
        SetColor(newParticles, _color);
        newParticles.Clear();
        newParticles.Play();
        StartCoroutine(Tools.Delay(WillDiesooooon, this, _particlesHitDelay));
    }

    public void WillDiesooooon(WillDieSoon particule) {
        particule.gameObject.SetActive(false);
        _particlesHitsUsed.Add(particule);
    }

    public void SetColor(ParticleSystem ps, Color color) {
        ParticleSystem.MainModule main = ps.main;
        main.startColor = color;
    }
}
