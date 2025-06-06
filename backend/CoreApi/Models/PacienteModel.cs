
namespace CoreApi.Models
{
    public class PacienteModel

    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public ICollection<ConsultaModel>? Consultas { get; set; } = new List<ConsultaModel>();

        public string Token { get; set; } = string.Empty;

    }
}