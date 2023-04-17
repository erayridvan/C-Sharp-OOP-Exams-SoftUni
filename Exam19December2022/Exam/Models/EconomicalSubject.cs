namespace UniversityCompetition.Models
{
    public class EconomicalSubject : Subject
    {
        private const double subjectRate = 1.0;

        public EconomicalSubject(int subjectId, string subjectName)
            : base(subjectId, subjectName, subjectRate)
        {
        }
    }
}
