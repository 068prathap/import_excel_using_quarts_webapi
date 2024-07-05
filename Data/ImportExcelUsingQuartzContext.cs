using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImportExcelUsingQuartz.Models;

namespace ImportExcelUsingQuartz.Data
{
    public class ImportExcelUsingQuartzContext : DbContext
    {
        public ImportExcelUsingQuartzContext (DbContextOptions<ImportExcelUsingQuartzContext> options)
            : base(options)
        {
        }

        public DbSet<ImportExcelUsingQuartz.Models.StudentList> StudentList { get; set; } = default!;
    }
}