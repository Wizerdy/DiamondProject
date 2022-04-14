using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeLibrary : MonoBehaviour {
    [SerializeField] List<BossShape> _bossShapes;

    public BossShape GetBossShape(Shape bossShape) {
        for (int i = 0; i < _bossShapes.Count; i++) {
            if (bossShape == _bossShapes[i]._shape) {
                return _bossShapes[i];
            }
        }
        return null;
    }
}
