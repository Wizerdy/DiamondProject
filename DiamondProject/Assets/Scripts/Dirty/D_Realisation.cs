using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class D_Realisation : MonoBehaviour {
    [SerializeField] PosterityObject _posterity;
    [SerializeField] MMFeedbacks _longIntro;
    [SerializeField] MMFeedbacks _shortIntro;

    private void Start() {
        if (_posterity.deathCount > 0) {
            _shortIntro.Initialization();
            _shortIntro.PlayFeedbacks();
        } else {
            _longIntro.Initialization();
            _longIntro.PlayFeedbacks();
        }
    }
}
