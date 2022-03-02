using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockShield : Shield {
    List<Rock> rocks = new List<Rock>();

    public void AddRock(Rock rock) {
        if (rocks.Count == 0) {
            Protect();
        }
        rocks.Add(rock);
    }

    public void RemoveRock(Rock rock) {
        rocks.Remove(rock);
        if (rocks.Count == 0) {
            StopProtect();
        }
        Destroy(gameObject);
    }
}
