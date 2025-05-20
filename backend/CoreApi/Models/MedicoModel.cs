using CoreApi.Enums;

namespace CoreApi.Models
{
    public class MedicoModel

    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public StatusEnum Status { get; set; }
        public ICollection<ConsultaModel>? Consultas { get; set; } = new List<ConsultaModel>();



        public DateTime DataInicioDisponivel { get; set; }
        public DateTime DataFimDisponivel { get; set; }

        public string Token { get; set; } = string.Empty;

    }
}