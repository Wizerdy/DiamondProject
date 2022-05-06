using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorpseManager : MonoBehaviour
{
    [SerializeField] private Color inPossessionColor;
    [SerializeField] private Color notInPossessionColor;

    [Header("Pas touche")]
    [SerializeField] private List<Image> corpseList = new List<Image>();
    [SerializeField] private PosterityObject posterity;
    private int numberOfCorpse;
    private Color deactivateCorpse = new Color(0, 0, 0, 0);

    public void SpawnFirstCorps() {
        corpseList[0].color = inPossessionColor;
    }

    public void ConsummeFirstCorps() {
        corpseList[0].color = notInPossessionColor;
    }

    public void ConsummeCorpse() {
        switch (numberOfCorpse) {
            case 0:
                Debug.Log("0 CORPSE");
                break;
            case 1:
                corpseList[0].color = notInPossessionColor;
                --numberOfCorpse;
                break;
            case 2:
                corpseList[1].color = notInPossessionColor;
                --numberOfCorpse;
                break;
            case 3:
                corpseList[2].color = notInPossessionColor;
                --numberOfCorpse;
                break;
        }
    }
    public void AddCorpse() {
        switch (numberOfCorpse) {
            case 0:
                corpseList[0].color = notInPossessionColor;
                ++numberOfCorpse;
                break;
            case 1:
                corpseList[1].color = notInPossessionColor;
                ++numberOfCorpse;
                break;
            case 2:
                corpseList[2].color = notInPossessionColor;
                ++numberOfCorpse;
                break;
            default:
                Debug.LogWarning("CORPSE NUMBER MAXED OUT");
                break;
        }
    }

    public void ResetCorpse() {
        numberOfCorpse = 0;
        foreach(var corpse in corpseList) {
            corpse.color = notInPossessionColor;
        }
    }

    public void UpdateNumberOfCorpse() {
        switch (numberOfCorpse) {
            case 1:
                corpseList[0].color = inPossessionColor;
                corpseList[1].color = notInPossessionColor;
                corpseList[2].color = notInPossessionColor;
                break;
            case 2:
                corpseList[0].color = inPossessionColor;
                corpseList[1].color = inPossessionColor;
                corpseList[2].color = notInPossessionColor;
                break;
            case 3:
                corpseList[0].color = inPossessionColor;
                corpseList[1].color = inPossessionColor;
                corpseList[2].color = inPossessionColor;
                break;
            default:
                Debug.Log("UPDATE ERROR");
                break;
        }
    }

    public int GetNumberOfCorpse() {
        return numberOfCorpse;
    }

    public void GetPosterityCorpse() {
        numberOfCorpse = posterity.nbCorpse;
    }

    public void UpdatePosterityCorpse() {
        if (numberOfCorpse < 3)
            posterity.nbCorpse = numberOfCorpse;
        else
            posterity.nbCorpse = 3;
    }

    public void DecreaseCorpseNumber() {
        --numberOfCorpse;
    }

    public void IncreaseCorpseNumber() {
        if (numberOfCorpse < 3)
            ++numberOfCorpse;
        else
            return;
    }
}
