using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int correctAnswers = 0;
    private int questionsSeen = 0;
    private int nullableAnswers = 0;


    #region Correct
    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }
    #endregion

    #region Seen
    public int GetQuestionSeen()
    {
        return questionsSeen;
    }

    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }
    #endregion

    #region Nullable
    public int GetNullableAnswers()
    {
        return nullableAnswers;
    }

    public void IncrementQuestionsNullable()
    {
        nullableAnswers++;
    }
    #endregion

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }

    public void setNextLevel()
    {
        correctAnswers = 0;
        questionsSeen = 0;
        nullableAnswers = 0;
    }
}
