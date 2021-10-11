using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjectManager.Tests {
    
    public class ProjectTests {

        [Fact]
        public void HasName() {

            string projectName = "project name";

            var project = new Project() { Name = projectName };

            Assert.Equal(projectName, project.Name);
        }

        [Fact]
        public void HasDescription() {

            string projectDescription = "project description";

            var project = new Project() { Description = projectDescription };

            Assert.Equal(projectDescription, project.Description);
        }

        [Fact]
        public void HasStartDate() {

            DateTime startdateProject = new DateTime(2021, 01, 01);

            var project = new Project() { StartDate = startdateProject };

            Assert.Equal(startdateProject, project.StartDate);
        }

        [Fact]
        public void HasFinishDate() {

            DateTime finishDateProject = new DateTime(2021, 01, 01);

            var project = new Project() { FinishDate = finishDateProject };

            Assert.Equal(finishDateProject, project.FinishDate);
        }

        [Fact]
        public void Project_WhenCreatesNewFinishDateIsNull() {

            var project = new Project();

            Assert.Null(project.FinishDate);
        }

        [Fact]
        public void Project_WhenCreatesNewAsignStartDate() {

            var project = new Project();

            Assert.NotEqual(DateTime.MinValue, project.StartDate);
        }

        [Fact]
        public void Project_ContainsAListOfTasks() {

            var project = new Project();

            Assert.Empty(project.Tasks);
        }

    }

    public class ProjectTaskTests {

        [Fact]
        public void HasName() {

            string name = "name";

            var task = new ProjectTask() { Name = name };

            Assert.Equal(name, task.Name);
        }

        [Fact]
        public void HasDescription() {

            string description = "description";

            var task = new ProjectTask() { Description = description };

            Assert.Equal(description, task.Description);
        }

        [Fact]
        public void HasCompleteField() {

            var task = new ProjectTask();

            Assert.False(task.Completed);
        }

        [Fact]
        public void HasAListOfTimes() {

            var task = new ProjectTask();

            Assert.Empty(task.Times);
        }
    }

    public class ProjectTask {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get;  set; }
        public IEnumerable Times { get; set; }
    }

    public class Project {

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get;  set; }
        public DateTime? FinishDate { get;  set; }
        public IEnumerable<ProjectTask> Tasks { get; set; }

        public Project() {
            StartDate = DateTime.Now;
            Tasks = new List<ProjectTask>();
        }

    }
}
