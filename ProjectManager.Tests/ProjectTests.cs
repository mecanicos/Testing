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
        public void HasRequiredProperties() {

            string projectName = "project name";
            string projectDescription = "project description";
            DateTime startdateProject = new DateTime(2021, 01, 01);

            var project = new Project() { Name = projectName, 
                Description = projectDescription,
                StartDate = startdateProject,
            };

            Assert.Equal(projectName, project.Name);
            Assert.Equal(projectDescription, project.Description);
            Assert.Equal(startdateProject, project.StartDate);
            Assert.Empty(project.Tasks);
            Assert.Null(project.FinishDate);
        }

        [Fact]
        public void Create_AsignStartDate() {

            var project = new Project();

            Assert.NotEqual(DateTime.MinValue, project.StartDate);
        }

        [Fact]
        public void AddTask_AddsNewValidTaskToList() {

            var project = new Project();
            var newTask = new ProjectTask("task");

            project.AddTask(newTask);

            Assert.NotNull(project.Tasks.FirstOrDefault().Name);
        }

        [Fact]
        public void Finish_ThrowExceptionIfAlmostOneTaskIsNotCompleted() {

            var project = new Project() {
                Tasks = {
                    new ProjectTask(""),
                    new ProjectTask(""),
                }
            };

            Assert.Throws<InvalidOperationException>(() => project.Finish());
        }

        [Fact]
        public void Finish_SetFinishDateIfAllTasksAreCompleted() {

            var project = new Project() {
                Tasks = {
                    new ProjectTask(""){ Completed = true }
                }
            };

            project.Finish();

            Assert.NotNull(project.FinishDate);
        }
    }

    public class ProjectTaskTests {

        [Fact]
        public void HasRequiredProperties() {

            string name = "name";
            string description = "description";

            var task = new ProjectTask(name) { Description = description };

            Assert.Equal(name, task.Name);
            Assert.Equal(description, task.Description);
            Assert.False(task.Completed);
            Assert.Empty(task.Intervals);
        }

        [Fact]
        public void AddInterval_AddsNewIntervalToList() {

            var task = new ProjectTask("");
            var newInterval = new TimeInterval();

            task.AddInterval(newInterval);

            Assert.True(task.Intervals.Count > 0);
        }

        [Fact]
        public void Complete_SetCompletedAsTrue() {

            var task = new ProjectTask("");

            task.Complete();

            Assert.True(task.Completed);
        }
    }

    public class TimeIntervalTests {

        [Fact]
        public void HasRequiredProperties() {

            string comments = "comments";
            DateTime fromTime = new DateTime(2021, 01, 01, 1, 0, 0);
            DateTime toTime = new DateTime(2021, 01, 01, 2, 0, 0);

            var interval = new TimeInterval() { 
                Comments = comments,
                FromTime = fromTime,
                ToTime = toTime
            };

            Assert.Equal(comments, interval.Comments);
            Assert.Equal(fromTime, interval.FromTime);
            Assert.Equal(toTime, interval.ToTime);
        }
    }

    public class TimeInterval {
        public string Comments { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }

    public class ProjectTask {

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get;  set; }
        public ICollection<TimeInterval> Intervals { get; set; }

        public ProjectTask(string name) {
            Name = name;
            Intervals = new List<TimeInterval>();
        }

        public void AddInterval(TimeInterval interval) {
            Intervals.Add(interval);
        }

        public void Complete() {
            Completed = true;
        }
    }

    

    public class Project {

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get;  set; }
        public DateTime? FinishDate { get;  set; }
        public ICollection<ProjectTask> Tasks { get; set; }

        public Project() {
            StartDate = DateTime.Now;
            Tasks = new List<ProjectTask>();
        }

        public void AddTask(ProjectTask task) {
            Tasks.Add(task);
        }

        public void Finish() {
            if(Tasks.Any(t=>t.Completed == false)) {
                throw new InvalidOperationException();
            }
            FinishDate = DateTime.Now;
        }

    }
}
