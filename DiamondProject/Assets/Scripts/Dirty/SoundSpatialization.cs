using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpatialization : MonoBehaviour {
    [SerializeField] CameraReference _cameraReference = null;
    [SerializeField] TransformReference _bossPositionReference = null;

    void Update() {
        if ((_cameraReference?.IsValid ?? false) && (_bossPositionReference?.IsValid ?? false)) {
            float rtpc_boss_position = _cameraReference.Instance.transform.position.x - _bossPositionReference.Instance.position.x;
            rtpc_boss_position = Mathf.Clamp(rtpc_boss_position, -50, 50) / 50;


            AkSoundEngine.SetRTPCValue("RTPC_Boss_Position", rtpc_boss_position);
        }
    }
}
