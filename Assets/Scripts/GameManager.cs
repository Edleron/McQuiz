using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Quiz quiz;
    private EndScreen endScreen;
    private GameObject sonScreen;

    private bool performans = false;


    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
        sonScreen = GameObject.FindGameObjectWithTag("sonScreen");
    }

    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
        sonScreen.SetActive(false);
        performans = true;
    }

    void Update()
    {
        if (quiz.isComplete && performans)
        {
            Debug.Log("TTT");
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
            performans = false;
        }
    }

    public void onReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onNextLevel()
    {
        Debug.Log(quiz.levels);
        if (quiz.levels < 1)
        {
            quiz.isComplete = false;
            performans = true;
            quiz.gameObject.SetActive(true);
            endScreen.gameObject.SetActive(false);
            quiz.onNextLevel();
        }
        else
        {
            sonScreen.SetActive(true);
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(false);
        }
    }
}
