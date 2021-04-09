using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace TelegramVictorinaBot
{
    class QuizQuestion
    {
        List<QuestionItem> QuestionsAndAnswersList;
        Random _random;
        int _count;

        public QuizQuestion(string path = "VictorinaQuestions.txt")
        {
            var questionsAndAnswersArrayData = File.ReadAllLines("VictorinaQuestions.txt");
            QuestionsAndAnswersList = questionsAndAnswersArrayData
                .Select(s => s.Split("/"))
                .Select(s =>
                    new QuestionItem
                    {
                        Question = s[0],
                        Answer = s[1]
                    })
                .ToList();
        }

        public QuestionItem GetQuestionItem()
        {
            _random = new Random();
            if (_count < 1)
            {
                _count = QuestionsAndAnswersList.Count;
            }

            var index = _random.Next(QuestionsAndAnswersList.Count - 1);
            var questionAndAnswer = QuestionsAndAnswersList[index];
            QuestionsAndAnswersList.RemoveAt(index);
            QuestionsAndAnswersList.Add(questionAndAnswer);
            _count -= 1;
            return questionAndAnswer;
        }
    }
}
