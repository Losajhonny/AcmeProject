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
    public class AnswerController : ControllerBase
    {
        private readonly DataContext _context;

        public AnswerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAnswerModel()
        {
            var list = await _context.AnswerModel.Select(x => createAnswerDto(x)).ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetAnswerModel(int id)
        {
            var answer = await _context.AnswerModel.FindAsync(id);

            if (answer == null)
                return NotFound();

            return Ok(createAnswerDto(answer));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswerModel(int id, AnswerDto req)
        {
            if (id != req.AnswerId)
                return BadRequest();

            var answer = await _context.AnswerModel.FindAsync(id);

            if (answer == null)
                return NotFound();

            answer.Result = req.Result;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<AnswerDto>> PostAnswerModel(AnswerDto req)
        {
            var answer = new AnswerModel
            {
                AnswerId = req.AnswerId,
                FieldRefId = req.FieldRefId,
                QuizRefId = req.QuizRefId,
                Result = req.Result
            };
            _context.AnswerModel.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswerModel", new { id = answer.AnswerId }, createAnswerDto(answer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswerModel(int id)
        {
            var answerModel = await _context.AnswerModel.FindAsync(id);
            if (answerModel == null)
                return NotFound();

            _context.AnswerModel.Remove(answerModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static AnswerDto createAnswerDto(AnswerModel answer)
        {
            return new AnswerDto
            {
                AnswerId = answer.AnswerId,
                FieldRefId = answer.FieldRefId,
                QuizRefId = answer.QuizRefId,
                Result = answer.Result
            };
        }
    }
}
