using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ToolsBoxEngine;

public class Rock : MonoBehaviour {
    public int life = 0;
    [SerializeField] Reference<Boss> _boss;
    [SerializeField] RockShield rockShield;
    [HideInInspector] public Sprite sprite;

    public void AddRockShield(RockShield rockShield) {
        this.rockShield = rockShield;
    }


    private void Start() {
        sprite = GetComponent<SpriteRenderer>().sprite;
        ShowZone(true);
        StartCoroutine(Tools.Delay(ShowZone, false, 0.5f));
        rockShield.AddRock(this);
    }

    void ShowZone(bool show) {
        transform.GetChild(0).gameObject.SetActive(show);
    }

    public void LoseLife(int life) {
        this.life -= life;
        if (this.life <= 0) {
            Die();
        }
    }

    public void Die() {
        rockShield.RemoveRock(this);
        Destroy(gameObject);
    }
}
