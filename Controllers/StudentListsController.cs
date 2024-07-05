using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImportExcelUsingQuartz.Data;
using ImportExcelUsingQuartz.Models;
using OfficeOpenXml;

namespace ImportExcelUsingQuartz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentListsController : ControllerBase
    {
        private readonly ImportExcelUsingQuartzContext _context;

        public StudentListsController(ImportExcelUsingQuartzContext context)
        {
            _context = context;
        }

        // GET: api/StudentLists
        [HttpGet]
        //[Route("Emp/All")]
        public async Task<ActionResult<IEnumerable<StudentList>>> GetStudentList()
        {
            return await _context.StudentList.ToListAsync();
        }

        // GET: api/StudentLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentList>> GetStudentList(int id)
        {
            var studentList = await _context.StudentList.FindAsync(id);
            if (studentList == null)
            {
                return NotFound();
            }

            return studentList;
        }

        // PUT: api/StudentLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentList(int id, StudentList studentList)
        {
            if (id != studentList.stuId)
            {
                return BadRequest();
            }

            _context.Entry(studentList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentList>> PostStudentList(StudentList studentList)
        {
            _context.StudentList.Add(studentList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentList", new { id = studentList.stuId }, studentList);
        }

        // DELETE: api/StudentLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentList(int id)
        {
            var studentList = await _context.StudentList.FindAsync(id);
            if (studentList == null)
            {
                return NotFound();
            }

            _context.StudentList.Remove(studentList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentListExists(int id)
        {
            return _context.StudentList.Any(e => e.stuId == id);
        }
    }
}