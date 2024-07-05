using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ImportExcelUsingQuartz.Models
{
    public class StudentList
    {
        [Key]
        public int stuId { get; set; }
        public string name { get; set; }
        public int standard { get; set; }
        public string phone { get; set; }
    }
}