﻿using Ignite.Areas.Admin.Controllers;
using Ignite.Areas.Admin.Services.Interfaces;
using Ignite.Areas.Admin.ViewModels;
using Ignite.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace Ignite.Tests.Admin.Controllers.AssignmentControllerTests
{
    [TestClass]
    public class AssignCourse_Should
    {
        [TestMethod]
        public void CallGetAllCoursesOnce()
        {
            // Arange
            var assignmentServiceMock = new Mock<IAssignmentService>();
            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(storeMock.Object);

            var courses = new List<Course>();

            assignmentServiceMock.Setup(m => m.GetAllCourses()).Returns(courses);

            var controller = new AssignmentController(assignmentServiceMock.Object, applicationUserManagerMock.Object);

            // Act
            controller.AssignCourse();

            // Assert
            assignmentServiceMock.Verify(a => a.GetAllCourses(), Times.Once);
        }

        [TestMethod]
        public void ReturnDefaultViewWithCorrectModel()
        {
            // Arange
            var assignmentServiceMock = new Mock<IAssignmentService>();
            var storeMock = new Mock<IUserStore<ApplicationUser>>();
            var applicationUserManagerMock = new Mock<ApplicationUserManager>(storeMock.Object);

            var courses = new List<Course>();

            assignmentServiceMock.Setup(m => m.GetAllCourses()).Returns(courses);

            var expectedModel = new ListAssignmentViewModel()
            {
                Courses = courses
            };

            var controller = new AssignmentController(assignmentServiceMock.Object, applicationUserManagerMock.Object);

            // Act & Assert
            controller
                .WithCallTo(c => c.AssignCourse())
                .ShouldRenderDefaultView()
                .WithModel<ListAssignmentViewModel>(m => Assert.AreSame(expectedModel.Courses, m.Courses));
        }
    }
}
