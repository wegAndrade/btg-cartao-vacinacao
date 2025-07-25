namespace btg.cartao.vacina.domain.Entities
{
    public class VaccineRecord
    {
        public VaccineRecord() { }

        public Guid IdPessoa { get; set; }
        public Guid Vacina { get; set; }
        public int Doses { get; set; }
        public DateTime DataAplicacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
