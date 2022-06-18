using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Quizz : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private Flowchart responseBookFlowchart;
    private int responseNumber = 0;
    public void QuestionReaction(int responseNum) {
        responseNumber = responseNum;
        flowchart.SendFungusMessage("OnResponseToQuestion");
    }

    public int GetResponseNumber() {
        return responseNumber;
    }

    public bool IsResponseCorrespondingToQuestion(bool isFall,int questionNum, int responseNum) {
        if (isFall) {
            if (questionNum == 3 && responseNum == 2)
                return true;
            if (questionNum == 5 && responseNum == 1)
                return true;

            return false;
        } else {
            if (questionNum == 2 && responseNum == 5)
                return true;
            if (questionNum == 4 && responseNum == 7)
                return true;

            return false;
        }
    }

    public bool AlreadyUseAnswer(int rightAnswerNum, int questionNum) {
        if (rightAnswerNum == questionNum)
            return true;
        return false;
    }

    public void OpenResopnseBook() {
        responseBookFlowchart.SendFungusMessage("OpenReponseBook");
    }
    public void CloseResopnseBook() {
        responseBookFlowchart.SendFungusMessage("CloseResponseBook");
    }
}
