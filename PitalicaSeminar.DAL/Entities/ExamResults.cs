using System;
using System.Collections.Generic;

namespace PitalicaSeminar.DAL.Entities
{
    public partial class ExamResults
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Score { get; set; }
        public int UserId { get; set; }
        public DateTime WriteDate { get; set; }

        public Exams Exam { get; set; }
        public Users User { get; set; }
    }
}
