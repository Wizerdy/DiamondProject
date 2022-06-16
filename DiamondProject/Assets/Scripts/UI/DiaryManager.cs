using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class DiaryManager : MonoBehaviour
{
    [SerializeField] private GameObject diary;
    [SerializeField] private Flowchart flowchart;


    public void EnableDiary() {
        diary.SetActive(true);
    }

    public void DisableDiary() {
        diary.SetActive(false);
    }

    public void GoToHub() {
        flowchart.SendFungusMessage("GoToHub");
    }

    public void CloseBook() {
        flowchart.SendFungusMessage("CloseBook");
    }
}