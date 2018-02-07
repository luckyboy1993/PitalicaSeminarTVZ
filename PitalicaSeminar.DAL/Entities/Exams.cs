using System;
using System.Collections.Generic;

namespace PitalicaSeminar.DAL.Entities
{
    public partial class Exams
    {
        public Exams()
        {
            ExamResults = new HashSet<ExamResults>();
            Questions = new HashSet<Questions>();
        }

        public int ExamId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ExamName { get; set; }
        public int? MaxScore { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }

        public Users User { get; set; }
        public ICollection<ExamResults> ExamResults { get; set; }
        public ICollection<Questions> Questions { get; set; }
    }
}
