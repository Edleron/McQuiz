using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI trueText;
    [SerializeField] TextMeshProUGUI falseText;
    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Muhteşem!\n Senin Puanın " +
                                scoreKeeper.CalculateScore() + "%";

        trueText.text = "Doğru Sayısı : " + scoreKeeper.GetCorrectAnswers();

        falseText.text = "Yanlış Sayısı : " + ((scoreKeeper.GetQuestionSeen() - scoreKeeper.GetCorrectAnswers()) - scoreKeeper.GetNullableAnswers());
    }
}
