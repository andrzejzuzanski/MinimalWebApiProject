namespace MyBoardsProject.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public User Author { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}

        //Referencje
        public WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }
        public Guid AuthorId { get; set; }

    }
}
