using System;
using System.Collections.Generic;

namespace PitalicaSeminar.DAL.Entities
{
    public partial class Questions
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public string Definition { get; set; }
        public int ExamId { get; set; }
        public int Score { get; set; }
        public bool? Visibility { get; set; }

        public Exams Exam { get; set; }
    }
}
