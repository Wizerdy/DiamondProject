using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Quizz : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    private int responseNumber = 0;
    public void QuestionReaction(int responseNum) {
        responseNumber = responseNum;
        flowchart.SendFungusMessage("OnResponseToQuestion");
    }

    public int GetResponseNumber() {
        return responseNumber;
    }

    public bool IsResponseCorrespondingToQuestion(int questionNum, int responseNum) {
        if (questionNum == responseNum)
            return true;
        return false;
    }
}
