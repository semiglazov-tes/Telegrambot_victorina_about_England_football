using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramVictorinaBot
{
    class QuestionState
    {
        public QuestionItem Item { get; set; }
        public int Opened=0;
        public bool IsEnd => Opened >= Item.Answer.Length;
        public bool Win { get; set; }
        public string PromptForAnswer => Item.Answer
            .Substring(0,Opened)
            .PadRight(Item.Answer.Length, '_');

    }
}
