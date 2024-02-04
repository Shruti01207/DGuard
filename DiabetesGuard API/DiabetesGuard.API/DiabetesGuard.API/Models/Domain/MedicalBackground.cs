namespace DiabetesGuard.API.Models.Domain
{
    public class MedicalBackground
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public bool isDiabetic { get; set; }
        public string DiabetesType { get; set; }
        public string FamilyHistory { get; set; }
        public string MedicalConditions { get; set;}


    }
}
