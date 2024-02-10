namespace SimpleTask.DAL.Domains
{
    public class DocumentFile
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }
        public Document document { get; set; }
        public string? File_Path { get; set; }
    }
}