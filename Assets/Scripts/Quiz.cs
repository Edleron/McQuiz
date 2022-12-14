using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] public List<QuestionSO> allQuestions = new List<QuestionSO>();
    internal List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] private QuestionSO currentQuestion;
    [SerializeField] private TextMeshProUGUI questionText;

    [Header("Answer")]
    [SerializeField] private GameObject[] answerButtons;
    private int correctAnswerIndex;
    private bool hasAnsweredEarly = true;
    private bool hasAnsweredPerformance = true;
    private string[] informationText = { "Birinci", "İkinci", "Üçüncü", "Dördüncü" };

    [Header("Button Colors")]
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;
    [SerializeField] private Sprite wrongAnswerSprite;

    [Header("Timer")]
    [SerializeField] private Image timerImage;
    private Timer timer;

    [Header("Scoring")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] private Slider progressBar;
    public bool isComplete;

    [Header("Levels")]
    [SerializeField] private TextMeshProUGUI levelText;
    public int levels = -1;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        levels = -1;
        onNextLevel();
        progressBar.value = 0;
        progressBar.maxValue = questions.Count;
        scoreText.text = "Skor: " + 0 + "%";
    }

    private void Update()
    {
        if (!isComplete)
        {
            timerImage.fillAmount = timer.fiilFraction;
            if (timer.loadNextQuestion)
            {
                Debug.Log("SORU SETLEME");
                if (progressBar.value == progressBar.maxValue)
                {
                    isComplete = true;
                    return;
                }
                timer.loadNextQuestion = false;
                GetNextQuestion();
            }

            if (!hasAnsweredEarly && !timer.isAnsweringQuestion && !hasAnsweredPerformance)
            {
                Debug.Log("SORU BOŞ GEÇİLİR İSE");

                DisplayAnswer(-1);
                SetButtonState(false);
                hasAnsweredPerformance = true;

                if (scoreKeeper.GetQuestionSeen() == progressBar.maxValue)
                {
                    Debug.Log(scoreKeeper.GetQuestionSeen() + " : " + progressBar.maxValue);
                    isComplete = true;
                    return;
                }
            }
        }
    }

    public void onNextLevel()
    {
        scoreText.text = "Skor: " + 0 + "%";

        scoreKeeper.setNextLevel();

        levels++;

        if (questions.Count > 0)
        {
            questions.Clear();
        }
        questions = allQuestions.Where(x => x.correctLevelIndex == levels).ToList();

        progressBar.value = 0;
        progressBar.maxValue = questions.Count;

        levelText.text = "Seviye : " + (levels + 1).ToString();

        Debug.Log("Kaç Tane Soru Var : " + questions.Count);
    }

    //Button Event States
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancalTimer();
        progressBar.value++;
        scoreText.text = "Skor: " + scoreKeeper.CalculateScore() + "%";
    }

    public void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Doğru !";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Üzgünüm, Doğru cevap; \n" + informationText[correctAnswerIndex] + " şıktır.";

            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;

            if (index != -1)
            {
                buttonImage = answerButtons[index].GetComponent<Image>();
                buttonImage.sprite = wrongAnswerSprite;
            }
            else
            {
                scoreKeeper.IncrementQuestionsNullable();
            }
        }
    }

    private void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            hasAnsweredPerformance = false;
            hasAnsweredEarly = false;
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    private void GetRandomQuestion()
    {
        // seviye sistemin eklenecek
        // 0'dan başlayıp x'inci seviyeye kadar.



        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
