using System;
using System.Collections.Generic;

namespace PitalicaSeminar.DAL.Entities
{
    public partial class Users
    {
        public Users()
        {
            ExamResults = new HashSet<ExamResults>();
            Exams = new HashSet<Exams>();
        }

        public int UserId { get; set; }
        public string Password { get; set; }
        public string StringUserId { get; set; }
        public string UserName { get; set; }
        public int SchoolId { get; set; }

        public Schools School { get; set; }
        public ICollection<ExamResults> ExamResults { get; set; }
        public ICollection<Exams> Exams { get; set; }
    }
}
