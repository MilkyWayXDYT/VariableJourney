using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Data.SQLite;
using UnityEngine.SceneManagement;
using System.Text;
using System;
using System.IO;

public class TestManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text questionText;
    [SerializeField]
    private List<TMP_Text> answersText;

    private List<Question> questions;

    private List<Question> userAnswers;

    private int currentQuestionNum = 0;

    private bool timerGo = false;
    private float timer = 10;

    private void Start()
    {
        questions = new List<Question>();
        userAnswers = new List<Question>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GetQuestionsFromDatabase();
        GetAnswersFromDatabase();
        SetQuestion();
    }

    private void GetAnswersFromDatabase()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            for (int i = 0; i < questions.Count; i++)
            {
                string query = $"select answer, isRight from Answers where questionId = {i + 1}";

                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string text = reader.GetString(0);
                            bool isRight = reader.GetByte(1) == 1 ? true : false;
                            (string, bool) answer = (text, isRight);
                            questions[i].answers.Add(answer);
                        }
                    }
                }
            }
        }
    }

    private void GetQuestionsFromDatabase()
    {
        using (var connection = DBHelper.GetConnection())
        {
            connection.Open();

            string query = "select * from Questions";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Question question = new Question();

                        question.questionNum = reader.GetInt32(0);
                        question.question = reader.GetString(1);

                        questions.Add(question);
                    }
                }
            }
        }
    }

    private void SetQuestion()
    {
        questionText.text = questions[currentQuestionNum].question;

        (string, bool)[] randAnswers = new (string, bool)[4];

        questions[currentQuestionNum].answers.CopyTo(randAnswers);

        for (int i = 0; i < randAnswers.Length; i++)
        {
            int j = UnityEngine.Random.Range(0, randAnswers.Length - 1);

            var temp = randAnswers[j];
            randAnswers[j] = randAnswers[i];
            randAnswers[i] = temp;
        }

        for (int i = 0; i < randAnswers.Length; i++)
        {
            answersText[i].text = randAnswers[i].Item1;
        }

        currentQuestionNum++;
    }

    public void AnswerQuestion(TMP_Text answerObj)
    {
        Question answer = new Question();
        answer.questionNum = currentQuestionNum;
        answer.question = questionText.text;
        answer.answers.Add(questions[currentQuestionNum - 1].answers.Find(item => item.Item1 == answerObj.text));

        userAnswers.Add(answer);

        if (currentQuestionNum == 12)
        {
            ResetGame();
            return;
        }

        SetQuestion();
    }

    private void ResetGame()
    {
        questionText.text = "Результаты прохождения теста успешно выгружены на рабочий стол";

        for (int i = 0; i < answersText.Count; i++)
        {
            answersText[i].GetComponentInParent<Transform>().gameObject.SetActive(false);
        }

        SaveResultsToFile();
        timerGo = true;
    }

    private void Update()
    {
        if (timerGo)
            timer -= Time.deltaTime;
        if (timer < 0)
            SceneManager.LoadScene("MainMenu");
    }

    private void SaveResultsToFile()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fileName = "TestResult.csv";
        string filePath = Path.Combine(desktopPath, fileName);

        StringBuilder csv = new StringBuilder();

        csv.AppendLine("Question number;Question;Answer;Is right");

        foreach (var userAnswer in userAnswers)
        {
            csv.AppendLine($"{userAnswer.questionNum};{userAnswer.question};{userAnswer.answers[0].Item1};{userAnswer.answers[0].Item2}");
        }
        try
        {
            File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
        }
        catch
        {
            questionText.text = "Возникла ошибка сохранения результатов";
        }
    } 
}
