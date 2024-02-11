namespace SimpleTask.BAL.DTOs
{
    public class SingleDocumentForReturnDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DueDate { get; set; }
        public int PriortyId { get; set; }


    }
}