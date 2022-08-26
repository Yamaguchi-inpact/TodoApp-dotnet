using System.ComponentModel.DataAnnotations;

namespace aspnetserver.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public bool IsComplete { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        public DateTime Modified { get; set; }
    }
}
