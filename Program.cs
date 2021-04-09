using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace TelegramVictorinaBot
{
    class Program
    {
        private static TelegramBotClient Bot;
        private static QuizQuestion QuizObject;
        private static Dictionary<long, QuestionState> QuestionState;

        static void Main(string[] args)
        {
            QuizObject = new QuizQuestion();
            QuestionState = new Dictionary<long, QuestionState>();
            var token = "1606967873:AAFV-Zod8MhSj6Yd_uyr7Pyz8b5uAyYVuIY";
            Bot = new TelegramBotClient(token);
            Bot.OnMessage += Bot_OnMessage;
            Bot.StartReceiving();
            Console.ReadLine();
        }

        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var chatId = e.Message.Chat.Id;
            var message = e.Message.Text;
            if (message == "/start")
            {
                NewRound(chatId);
            }
            else
            {
                if (QuestionState.TryGetValue(chatId, out var questionState))
                {
                    if (message.Trim().ToLowerInvariant() == questionState.Item.Answer.Trim().ToLowerInvariant())
                    {
                        questionState.Win = true;
                        var winMessage = $@"Правильно! Это-{questionState.Item.Answer}
Колличество правильных ответов:";
                        Bot.SendTextMessageAsync(chatId, winMessage);
                        NewRound(chatId);
                    }
                    else
                    {
                        questionState.Opened++;
                        if (questionState.IsEnd)
                        {
                            var loseMessage = $@"Никто не угадал! Это был-{questionState.Item.Answer}";
                            Bot.SendTextMessageAsync(chatId, loseMessage);
                            NewRound(chatId);
                        }
                        else
                        {
                            Bot.SendTextMessageAsync(chatId,
                                $@"{questionState.Item.Question}
{questionState.PromptForAnswer}");
                        }
                    }
                }
            }
        }
            public static void NewRound(long chatId)
        {
            var question = QuizObject.GetQuestionItem();
            var state = new QuestionState()
            {
                Item = question
            };
            QuestionState[chatId] = state;
            Bot.SendTextMessageAsync(chatId,
                $@"{state.Item.Question}
{state.PromptForAnswer}");
        }
    }
}