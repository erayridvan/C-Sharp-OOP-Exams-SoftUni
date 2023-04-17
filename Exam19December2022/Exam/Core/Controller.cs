using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private string[] allowedCategories = new string[]
            { "TechnicalSubject", "EconomicalSubject", "HumanitySubject"};

        private SubjectRepository subjects;
        private StudentRepository students;
        private UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }

        public string AddStudent(string firstName, string lastName)
        {
            int studentId = students.Models.Count() + 1;
            IStudent student = students.FindByName($"{firstName} {lastName}");

            if (student != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName).TrimEnd();
            }

            student = new Student(studentId, firstName, lastName);
            students.AddModel(student);

            return string.Format
                (OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name).TrimEnd();
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (!allowedCategories.Contains(subjectType))
            {
                return String.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            if (subjects.FindByName(subjectName) != null)
            {
                return String.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }
            Subject subject = null;
            if (subjectType == typeof(TechnicalSubject).Name)
            {
                subject = new TechnicalSubject(0, subjectName);
            }
            if (subjectType == typeof(EconomicalSubject).Name)
            {
                subject = new EconomicalSubject(0, subjectName);
            }
            if (subjectType == typeof(HumanitySubject).Name)
            {
                subject = new HumanitySubject(0, subjectName);
            }

            subjects.AddModel(subject);

            return String.Format(OutputMessages.SubjectAddedSuccessfully, subject.GetType().Name, subjectName, subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            int universityId = universities.Models.Count() + 1;

            if (universities.FindByName(universityName) != null)
            {
                return string.Format
                    (OutputMessages.AlreadyAddedUniversity, universityName).TrimEnd();
            }

            List<int> requiredSubjectsAsInt =
                requiredSubjects.Select(s => subjects.FindByName(s).Id).ToList();

            University university = new University(universityId, universityName,
                category, capacity, requiredSubjectsAsInt);

            universities.AddModel(university);

            return string.Format(OutputMessages.UniversityAddedSuccessfully,
                universityName, universities.GetType().Name).TrimEnd();
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string result = "";

            string firstName = studentName.Split(" ")[0];
            string lastName = studentName.Split(" ")[1];

            var student = this.students.FindByName(studentName);
            var university = this.universities.FindByName(universityName);

            if (student == null)
            {
                result = String.Format($"{studentName} is not registered in the application!");
            }
            else if (university == null)
            {
                result = string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }
            else if (!university.RequiredSubjects.All(x => student.CoveredExams.Any(e => e == x)))
            {
                result = string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }
            else if (student.University != null && student.University.Name == universityName)
            {
                result = string.Format(OutputMessages.StudentAlreadyJoined, firstName, lastName, universityName);
            }
            else
            {
                student.JoinUniversity(university);
                result = string.Format(OutputMessages.StudentSuccessfullyJoined, firstName, lastName, universityName);
            }

            return result.TrimEnd();
        }

        public string TakeExam(int studentId, int subjectId)
        {
            string result = "";

            if (this.students.FindById(studentId) == null)
            {
                result = string.Format(OutputMessages.InvalidStudentId);
            }
            else if (this.subjects.FindById(subjectId) == null)
            {
                result = string.Format(OutputMessages.InvalidSubjectId);
            }
            else if (students.FindById(studentId).CoveredExams.Any(e => e == subjectId))
            {
                result = string
                    .Format(OutputMessages
                    .StudentAlreadyCoveredThatExam,
                    students.FindById(studentId).FirstName,
                    students.FindById(studentId).LastName,
                    subjects.FindById(subjectId).Name);
            }
            else
            {
                var student = this.students.FindById(studentId);
                var subject = this.subjects.FindById(subjectId);

                student.CoverExam(subject);
                result = string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
            }

            return result.TrimEnd();
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = universities.FindById(universityId);
            int studentCount = this.students.Models.Where(s => s.University == university).Count();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentCount}");

            return sb.ToString().Trim();
        }
    }
}
