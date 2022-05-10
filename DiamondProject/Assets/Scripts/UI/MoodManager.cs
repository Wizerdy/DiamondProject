using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Mood {
    public int id;
    public string name;
    public Sprite sprite;
}
public class MoodManager : MonoBehaviour
{
    [SerializeField] private List<Mood> listMood = new List<Mood>();
    [SerializeField] private Mood currentMood;
    [SerializeField] private GameObject moodGO;

    private Image currentMoodSprite;

    private void Start() {
        currentMoodSprite = GetComponent<Image>();
    }

    public void ChangeMood(Mood mood) {
        if (listMood.Contains(mood)) {
            currentMood = mood;
            ChangeMoodSprite(mood);
        } else {
            Debug.Log("ERROR MOOD DOESNT EXIST");
        }
    }

    public void ChangeMoodSprite(Mood mood) {
        currentMoodSprite.sprite = mood.sprite;
    }

    public Mood GetCurrentMood() {
        return currentMood;
    }

    public void MoodAppear() {
        moodGO.SetActive(true);
    }
}
