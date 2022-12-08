using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;

    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] GameObject[] answerButtons;

    private void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }
}
