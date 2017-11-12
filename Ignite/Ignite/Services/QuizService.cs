﻿using Bytes2you.Validation;
using Ignite.Data;
using Ignite.Data.Enums;
using Ignite.Data.Models;
using Ignite.Services.Contracts;
using Ignite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ignite.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext context;

        public QuizService(ApplicationDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public Quiz GetTest(int? courseId)
        {
            courseId = 4;

            var questions = this.context.Courses.First(c => c.Id == courseId).Questions.ToList();

            var quizQuestions = new List<QuizQuestion>();
            var counter = 1;
            foreach (var question in questions)
            {
                var currentQ = new QuizQuestion();
                currentQ.Statement = question.Statement;
                currentQ.CorrectAnswer = question.CorrectAnswer;
                foreach (var answer in question.Answers)
                {
                    var answerView = new AnsweViewModel(answer.Text, answer.Letter);

                    answerView.Id = counter;
                    currentQ.Answers.Add(answerView);
                    counter++;
                }
                quizQuestions.Add(currentQ);
            }

            var quiz = new Quiz();
            quiz.Questions = quizQuestions;
            //quiz.AssignmentId = this.context.Assignments.First(a => a.CourseId == courseId).Id;

            quiz.AssignmentId = 1;

            return quiz;
        }

        public double SubmitTest(Quiz quiz)
        {
            var questionsCount = quiz.Questions.Count;
            var correctAnswerCount = 0;

            // Fix correct Answer not to show 
            //var questions = this.context.Courses
            //    .First(c => c.Id == this.context.Assignments.First(a => a.Id == quiz.AssignmentId).Id)
            //    .Questions
            //    .ToList();

            foreach (var question in quiz.Questions)
            {
                if (question.CorrectAnswer == question.ChosenAnswer)
                {
                    correctAnswerCount++;
                }
            }

            double requiredScore = this.context.Assignments
                .First(a => a.Id == quiz.AssignmentId)
                .Course
                .RequiredScore;

            double score = (correctAnswerCount / questionsCount) * 100;

            if (score >= requiredScore)
            {
                this.context.Assignments
                    .First(a => a.Id == quiz.AssignmentId)
                    .State = AssignmentState.Completed;
            }

            this.context.Assignments
                .First(a => a.Id == quiz.AssignmentId)
                .TestResult = score;


            return score;
        }
    }
}