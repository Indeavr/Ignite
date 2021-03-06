﻿using Ignite.Data;
using Ignite.Data.Models;
using Ignite.Services;
using Ignite.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ignite.Tests.Services.QuizServiceTests
{
    [TestClass]
    public class GetTest_Should
    {
        [TestMethod]
        public void ReturnCorrectModel_WhenCourseIdIsNotNull()
        {
            // Arange
            var contextMock = new Mock<ApplicationDbContext>();

            var courseId = 1;
            var assignmentId = 1;
            var username = "goshkata";

            var statement = "Statement";
            var correctAnswer = "A";

            var answerTextA = "answerA";
            var answerLetterA = "A";


            var httpContext = new Mock<HttpContextBase>();
            var mockIdentity = new Mock<IIdentity>();
            httpContext.SetupGet(x => x.User.Identity).Returns(mockIdentity.Object);
            mockIdentity.Setup(x => x.Name).Returns(username);

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new ApplicationUserManager(userStore.Object);

            var user = new ApplicationUser()
            {
                UserName = username
            };

            var answers = new List<Answer>()
            {
                new Answer() { Letter = answerLetterA , Text = answerTextA }
            };

            var questions = new List<Question>()
            {
                new Question() { Statement= statement, Answers = answers, CorrectAnswer = correctAnswer }
            };

            var courses = new List<Course>()
            {
                new Course() { Id = courseId, Questions = questions }
            };

            var assignments = new List<Assignment>()
            {
                new Assignment() {  CourseId = courseId, Id= assignmentId, User = user }
            };


            var dbSetAnswers = new Mock<DbSet<Answer>>().SetupData(answers);
            var dbSetQuestions = new Mock<DbSet<Question>>().SetupData(questions);
            var dbSetCourses = new Mock<DbSet<Course>>().SetupData(courses);
            var dbSetAssignments = new Mock<DbSet<Assignment>>().SetupData(assignments);

            contextMock.SetupGet(m => m.Courses).Returns(dbSetCourses.Object);
            contextMock.SetupGet(m => m.Questions).Returns(dbSetQuestions.Object);
            contextMock.SetupGet(m => m.Answer).Returns(dbSetAnswers.Object);
            contextMock.SetupGet(m => m.Assignments).Returns(dbSetAssignments.Object);

            var answersExpected = new List<AnsweViewModel>();
            answersExpected.Add(new AnsweViewModel()
            {
                Letter = answerLetterA,
                Text = answerTextA
            });

            var questionsExpected = new List<QuizQuestion>();
            questionsExpected.Add(new QuizQuestion()
            {
                Statement = statement,
                Answers = answersExpected
            });

            var quizExpected = new Quiz()
            {
                AssignmentId = assignmentId,
                Questions = questionsExpected
            };

            var service = new QuizService(contextMock.Object);

            // Act
            var result = service.GetTest(courseId, username);

            // Assert
            Assert.AreEqual(quizExpected.AssignmentId, result.AssignmentId);
            Assert.AreEqual(questionsExpected.First().Statement, result.Questions.First().Statement);
            Assert.AreEqual(questionsExpected.First().Answers.First().Letter, result.Questions.First().Answers.First().Letter);
            Assert.AreEqual(questionsExpected.First().Answers.First().Text, result.Questions.First().Answers.First().Text);
        }
    }
}
