using PitalicaSeminar.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PitalicaSeminar.DAL.Models
{
    public static class Seeder
    {
        public static void SeedInitial(this PitalicaDbContext _pitalicaDbContext)
        {
            if (!_pitalicaDbContext.ExamResults.Any())
            {
                //SeedSchools(_pitalicaDbContext);

                //SeedUsers(_pitalicaDbContext);

                //SeedExams(_pitalicaDbContext);

                //SeedQuestions(_pitalicaDbContext);

                SeedExamResults(_pitalicaDbContext);
            }
        }

        private static void SeedSchools(PitalicaDbContext _pitalicaDbContext)
        {
            var schools = new List<Schools>();

            for (int i = 1; i < 6; i++)
            {
                var school = new Schools
                {
                    Name = "Škola" + i,
                    Address = new Addresses
                    {
                        CityName = "Zagreb",
                        Country = "Hrvatska",
                        StreetName = "Našička" + i + i
                    },

                };

                schools.Add(school);
            }

            _pitalicaDbContext.Schools.AddRange(schools);

            _pitalicaDbContext.SaveChanges();
        }

        private static void SeedUsers(PitalicaDbContext _pitalicaDbContext)
        {
            var users = new List<Users>();

            for (int i = 1; i < 6; i++)
            {
                var user1 = new Users
                {
                    SchoolId = i,
                    UserName = "User" + i,
                    Password = "1234",
                    StringUserId = "User" + i,

                };

                var user2 = new Users
                {
                    SchoolId = i,
                    UserName = "Osoba" + i,
                    Password = "1234",
                    StringUserId = "Osoba" + i,

                };

                users.Add(user1);
                users.Add(user2);
            }

            _pitalicaDbContext.Users.AddRange(users);

            _pitalicaDbContext.SaveChanges();
        }

        private static void SeedExams(PitalicaDbContext _pitalicaDbContext)
        {
            var exams = new List<Exams>();

            for (int i = 1; i < 6; i++)
            {
                var exam1 = new Exams
                {
                    ExamName = "Exam" + i,
                    MaxScore = i * 2,
                    Password = "1234",
                    UserId = i,

                };

                var exam2 = new Exams
                {
                    ExamName = "Test" + i,
                    MaxScore = i * 2,
                    Password = "1234",
                    UserId = i,

                };

                var exam3 = new Exams
                {
                    ExamName = "Ispit" + i,
                    MaxScore = i * 2,
                    Password = "1234",
                    UserId = i,

                };

                exams.Add(exam1);
                exams.Add(exam2);
                exams.Add(exam3);
            }

            _pitalicaDbContext.Exams.AddRange(exams);

            _pitalicaDbContext.SaveChanges();
        }

        private static void SeedQuestions(PitalicaDbContext _pitalicaDbContext)
        {
            var questions = new List<Questions>();

            for (int i = 1; i < 16; i++)
            {
                var question1 = new Questions
                {
                    Definition = "PrvoPitanje",
                    Answer = "Ogovor" + i,
                    ExamId = i,
                    Score = i,
                    Visibility = true

                };

                var question2 = new Questions
                {
                    Definition = "DrugoPitanje",
                    Answer = "Ogovor" + i,
                    ExamId = i,
                    Score = i,
                    Visibility = true

                };

                var question3 = new Questions
                {
                    Definition = "TrećePitanje",
                    Answer = "Ogovor" + i,
                    ExamId = i,
                    Score = i,
                    Visibility = true

                };

                questions.Add(question1);
                questions.Add(question2);
                questions.Add(question3);
            }

            _pitalicaDbContext.Questions.AddRange(questions);

            _pitalicaDbContext.SaveChanges();
        }

        private static void SeedExamResults(PitalicaDbContext _pitalicaDbContext)
        {
            var examResults = new List<ExamResults>();

            /*for (int i = 0; i < 4; i++)
            {*/
                var examResults1 = new ExamResults
                {
                    ExamId = 1,
                    UserId = 2 + 1,
                    Score = "10"

                };
                /*
                var examResults2 = new ExamResults
                {
                    ExamId = i,
                    UserId = i + 2,
                    Score = "20"

                };

                var examResults3 = new ExamResults
                {
                    ExamId = i,
                    UserId = i + 3,
                    Score = "30"

                };*/

                examResults.Add(examResults1);
                //examResults.Add(examResults2);
                //examResults.Add(examResults3);
            //}

            _pitalicaDbContext.ExamResults.AddRange(examResults);

            _pitalicaDbContext.SaveChanges();
        }
    }
}

/*
 * Users = new List<Users>
                    {
                        new Users
                        {
                            UserName = "Lucian",
                            Password = "1234",
                            StringUserId = "Lucian",
                            Exams = new List<Exams>
                            {
                                new Exams
                                {
                                    ExamName = "Test1"
                                }

                            },
                        },
                    },*/
