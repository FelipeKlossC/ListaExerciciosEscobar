namespace Tutorial.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int? ProfessorId { get; set; }  // nullable com ?
        public Professor Professor { get; set; }
    }
}