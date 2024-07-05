using ImportExcelUsingQuartz.Controllers;
using ImportExcelUsingQuartz.Data;
using ImportExcelUsingQuartz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OfficeOpenXml;
using Quartz;
using System.Configuration;

namespace ImportExcelUsingQuartz.jobs
{
    public class UpdateJob : Controller, IJob
    {
        private readonly IConfiguration configuration;

        public UpdateJob(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ImportExcelData()
        {
            var pathfile = "D:\\Learning\\c#\\ImportExcel\\Assests\\studentList.xlsx";
            var List = new List<StudentList>();
            using (var package = new ExcelPackage(pathfile))
            {
                try
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var RowCount = worksheet.Dimension.Rows;

                    string connectionstring1 = configuration.GetConnectionString("ImportExcelUsingQuartzContext");
                    SqlConnection connection1 = new SqlConnection(connectionstring1);

                    connection1.Open();

                    SqlCommand truncateCmd = new SqlCommand("TRUNCATE TABLE studentList;", connection1);

                    truncateCmd.ExecuteNonQuery();

                    connection1.Close();

                    for (int row = 2; row <= RowCount; row++)
                    {
                        List.Add(new StudentList
                        {
                            stuId = Convert.ToInt32(worksheet.Cells[row, 1].Value.ToString().Trim()),
                            name = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            standard = Convert.ToInt32(worksheet.Cells[row, 3].Value.ToString().Trim()),
                            phone = worksheet.Cells[row, 4].Value.ToString().Trim(),
                        });
                        Console.WriteLine(List.Last().name);

                        string connectionstring = configuration.GetConnectionString("ImportExcelUsingQuartzContext");
                        SqlConnection connection = new SqlConnection(connectionstring);

                        connection.Open();

                        SqlCommand cmd = new SqlCommand("insert into studentList (stuId,name,standard,phone)" + "VALUES(@stuId,@name,@standard,@phone)", connection);

                        cmd.Parameters.AddWithValue("@stuId", List.Last().stuId);
                        cmd.Parameters.AddWithValue("@name", List.Last().name);
                        cmd.Parameters.AddWithValue("@standard", List.Last().standard);
                        cmd.Parameters.AddWithValue("@phone", List.Last().phone);

                        cmd.ExecuteNonQuery();

                        connection.Close();
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                }
            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            ImportExcelData();
            Console.WriteLine($"Notify user at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}