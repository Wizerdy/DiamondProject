using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorpseManager : MonoBehaviour
{

    [Range(0,1)]
    [SerializeField] private float opacity = 0.6f;

    [Header("Pas touche")]
    [SerializeField] private GameObject firstCorpse;
    [SerializeField] private GameObject secondCorpse;
    [SerializeField] private GameObject thirdCorpse;
    [SerializeField] private PosterityObject posterity;

    private int numberOfCorpse;
    private Image firstCorpseImage;
    private Image secondCorpseImage;
    private Image thirdCorpseImage;
    // Start is called before the first frame update
    void Start()
    {
        firstCorpseImage = firstCorpse.GetComponent<Image>();
        secondCorpseImage = secondCorpse.GetComponent<Image>();
        thirdCorpseImage = thirdCorpse.GetComponent<Image>();
    }

    public void SpawnFirstCorps() {
        firstCorpse.SetActive(true);
        firstCorpseImage.color = new Color(1,1,1,1);
    }

    public void ConsummeFirstCorps() {
        firstCorpse.SetActive(true);
        firstCorpseImage.color = new Color(1, 1, 1, opacity);
    }

    public void ConsummeCorpse() {
        switch (numberOfCorpse) {
            case 0:
                Debug.Log("0 CORPSE");
                break;
            case 1:
                firstCorpseImage.color = new Color(1, 1, 1, opacity);
                --numberOfCorpse;
                break;
            case 2:
                secondCorpseImage.color = new Color(1, 1, 1, opacity);
                --numberOfCorpse;
                break;
            case 3:
                thirdCorpseImage.color = new Color(1, 1, 1, opacity);
                --numberOfCorpse;
                break;
            default:
                Debug.Log("NOT ENOUGH CORPSE");
                break;
        }
    }
    public void AddCorpse() {
        switch (numberOfCorpse) {
            case 0:
                firstCorpse.SetActive(true);
                firstCorpseImage.color = new Color(1, 1, 1, 1);
                ++numberOfCorpse;
                break;
            case 1:
                secondCorpse.SetActive(true);
                secondCorpseImage.color = new Color(1, 1, 1, 1);
                ++numberOfCorpse;
                break;
            case 2:
                thirdCorpse.SetActive(true);
                thirdCorpseImage.color = new Color(1, 1, 1, 1);
                ++numberOfCorpse;
                break;
            default:
                Debug.Log("CORPSE FULL");
                break;
        }
    }

    public void ResetCorpse() {
        numberOfCorpse = 0;

        firstCorpse.SetActive(true);
        firstCorpseImage.color = new Color(1, 1, 1, opacity);
        secondCorpse.SetActive(true);
        secondCorpseImage.color = new Color(1, 1, 1, opacity);
        thirdCorpse.SetActive(true);
        thirdCorpseImage.color = new Color(1, 1, 1, opacity);
    }

    public void UpdateNumberOfCorpse(int nb) {
        numberOfCorpse = nb;

        switch (numberOfCorpse) {
            case 1:
                firstCorpse.SetActive(true);
                firstCorpseImage.color = new Color(1, 1, 1, 1);
                secondCorpse.SetActive(true);
                secondCorpseImage.color = new Color(1, 1, 1, opacity);
                thirdCorpse.SetActive(true);
                thirdCorpseImage.color = new Color(1, 1, 1, opacity);
                break;
            case 2:
                firstCorpse.SetActive(true);
                firstCorpseImage.color = new Color(1, 1, 1, 1);
                secondCorpse.SetActive(true);
                secondCorpseImage.color = new Color(1, 1, 1, 1);
                thirdCorpse.SetActive(true);
                thirdCorpseImage.color = new Color(1, 1, 1, opacity);
                break;
            case 3:
                firstCorpse.SetActive(true);
                firstCorpseImage.color = new Color(1, 1, 1, 1);
                secondCorpse.SetActive(true);
                secondCorpseImage.color = new Color(1, 1, 1, 1);
                thirdCorpse.SetActive(true);
                thirdCorpseImage.color = new Color(1, 1, 1, 1);
                break;
            default:
                Debug.Log("CORPSE FULL");
                break;
        }
        //for (int i = 0; i < numberOfCorpse; i++) {
        //    Debug.Log("call addcopresr");
        //    AddCorpse();
        //}
    }

    public int GetNumberOfCorpse() {
        return numberOfCorpse;
    }

    public void PosterityCorpseUpdate(int corpseNumber) {
        if (corpseNumber < 3)
            posterity.nbCorpse = corpseNumber;
        else
            posterity.nbCorpse = 3;
    }
}
