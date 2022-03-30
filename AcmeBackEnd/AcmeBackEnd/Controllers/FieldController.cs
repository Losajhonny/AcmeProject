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
    public class FieldController : ControllerBase
    {
        private readonly DataContext _context;

        public FieldController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldDto>>> GetFieldModel()
        {
            var list = await _context.FieldModel.Select(x => createFieldDto(x)).ToListAsync();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FieldDto>> GetFieldModel(int id)
        {
            var field = await _context.FieldModel.FindAsync(id);

            if (field == null)
                return NotFound();

            return Ok(createFieldDto(field));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFieldModel(int id, FieldDto req)
        {
            if (id != req.FieldId)
                return BadRequest();

            var field = await _context.FieldModel.FindAsync(id);

            if (field == null)
                return NotFound();

            field.Name = req.Name;
            field.Title = req.Title;
            field.Required = req.Required;
            field.TypeField = req.TypeField;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<FieldDto>> PostFieldModel(FieldDto req)
        {
            var field = new FieldModel
            {
                Name = req.Name,
                Title = req.Title,
                Required = req.Required,
                TypeField = req.TypeField,
                QuizRefId = req.QuizRefId
            };

            _context.FieldModel.Add(field);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFieldModel", new { id = field.FieldId }, createFieldDto(field));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFieldModel(int id)
        {
            var field = await _context.FieldModel.FindAsync(id);
            if (field == null)
                return NotFound();

            _context.FieldModel.Remove(field);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static FieldDto createFieldDto(FieldModel field)
        {
            return new FieldDto
            {
                FieldId = field.FieldId,
                Name = field.Name,
                Title = field.Title,
                Required = field.Required,
                TypeField = field.TypeField,
                QuizRefId = field.QuizRefId
            };
        }
    }
}
