#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeBackEnd.Data;
using AcmeBackEnd.Models;
using AcmeBackEnd.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AcmeBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly DataContext _context;

        public QuizController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetQuizModel()
        {
            var list = await _context.QuizModel.Select(x => createQuizDto(x)).ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> GetQuizModel(int id)
        {
            var quiz = await _context.QuizModel.FindAsync(id);

            if (quiz == null)
                return NotFound();

            return Ok(createQuizDto(quiz));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizModel(int id, QuizDto req)
        {
            if (id != req.QuizId)
                return BadRequest();

            var quiz = await _context.QuizModel.FindAsync(id);

            if (quiz == null)
                return NotFound();

            quiz.Name = req.Name;
            quiz.Description = req.Description;
            quiz.UniqueId = req.UniqueId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<QuizDto>> PostQuizModel(QuizDto req)
        {
            var quiz = new QuizModel
            {
                Name = req.Name,
                Description = req.Description,
                UserRefId = req.UserRefId,
                UniqueId = System.Guid.NewGuid().ToString()
            };

            _context.QuizModel.Add(quiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuizModel", new { id = quiz.QuizId }, createQuizDto(quiz));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizModel(int id)
        {
            var quiz = await _context.QuizModel.FindAsync(id);
            if (quiz == null)
                return NotFound();

            _context.QuizModel.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static QuizDto createQuizDto(QuizModel field)
        {
            return new QuizDto
            {
                QuizId = field.QuizId,
                Name = field.Name,
                Description = field.Description,
                UniqueId = field.UniqueId,
                UserRefId = field.UserRefId
            };
        }
    }
}
