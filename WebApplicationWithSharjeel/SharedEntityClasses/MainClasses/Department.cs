namespace SharedEntityClasses.MainClasses
{
    public class Department
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } // dto use
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
