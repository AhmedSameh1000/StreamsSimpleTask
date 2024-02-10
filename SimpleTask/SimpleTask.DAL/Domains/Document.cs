namespace SimpleTask.DAL.Domains
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created_Date { get; set; }

        public DateTime Due_Date { get; set; }

        public int PriorityId { get; set; }
        public Priority priority { get; set; }
        public List<DocumentFile> documents { get; set; }
    }
}