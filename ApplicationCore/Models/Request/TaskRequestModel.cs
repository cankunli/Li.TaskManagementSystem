using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Request
{
    public class TaskRequestModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        [StringLength(50)] public string Title { get; set; }
        [StringLength(500)] public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public char Priority { get; set; }
        [StringLength(500)] public string Remarks { get; set; }
        public DateTime Completed { get; set; }
    }
}
