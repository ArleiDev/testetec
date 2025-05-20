
namespace CoreApi.Models
{
    public class ConsultaModel

    {
        public int Id { get; set; }
        public DateTime DataConsulta { get; set; }
        public int PacienteId { get; set; }
        public required PacienteModel PacienteDaConsulta { get; set; }
        public int MedicoId { get; set; }
        public required MedicoModel MedicoDaConsulta { get; set; }


    }
}