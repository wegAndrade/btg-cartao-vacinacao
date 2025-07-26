namespace btg.cartao.vacina.domain.Entities
{
    public class VaccineRecord
    {
        public VaccineRecord() { }

        public Guid PersonId { get; set; }
        public Guid VaccineId { get; set; }
        public int Applications { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
