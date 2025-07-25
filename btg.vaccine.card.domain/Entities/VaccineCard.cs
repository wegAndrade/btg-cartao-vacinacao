namespace btg.cartao.vacina.domain.Entities
{
    public class VaccineCard
    {
        public Person Pessoa { get; set; }
        public List<VaccineRecord> RegistrosVacina { get; set; }

        public VaccineCard(Person pessoa, List<VaccineRecord> registrosVacina)
        {
            Pessoa = pessoa;
            RegistrosVacina = registrosVacina;
        }
    }
}
