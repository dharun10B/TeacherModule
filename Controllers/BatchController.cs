using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeacherModule.DTOs;
using TeacherModule.Models; // Adjust based on your namespace structure

namespace TeacherModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly TeacherDBContext _context;

        public BatchController(TeacherDBContext context)
        {
            _context = context;
        }

        // 1. Get All Batches
        [HttpGet]
        public async Task<IActionResult> GetAllBatches()
        {
            var batches = await _context.Batches
                .Select(b => new BatchDto
                {
                    Id = b.Id,
                    BatchName = b.BatchName,
                    BatchTiming = b.BatchTiming,
                    BatchType = b.BatchType,
                    CourseId = b.CourseId
                })
                .ToListAsync();

            return Ok(batches);
        }

        // 2. Get Batch by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatchById(int id)
        {
            var batch = await _context.Batches
                .Where(b => b.Id == id)
                .Select(b => new BatchDto
                {
                    Id = b.Id,
                    BatchName = b.BatchName,
                    BatchTiming = b.BatchTiming,
                    BatchType = b.BatchType,
                    CourseId = b.CourseId
                })
                .FirstOrDefaultAsync();

            if (batch == null)
                return NotFound();

            return Ok(batch);
        }

        // 3. Create New Batch
        [HttpPost]
        public async Task<IActionResult> CreateBatch([FromBody] BatchDto batchDto)
        {
            if (batchDto == null)
                return BadRequest();

            var batch = new Batch
            {
                BatchName = batchDto.BatchName,
                BatchTiming = batchDto.BatchTiming,
                BatchType = batchDto.BatchType,
                CourseId = batchDto.CourseId
            };

            _context.Batches.Add(batch);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBatchById), new { id = batch.Id }, batchDto);
        }

        // 4. Update Batch
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBatch(int id, [FromBody] BatchDto batchDto)
        {
            if (batchDto == null || id != batchDto.Id)
                return BadRequest();

            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
                return NotFound();

            batch.BatchName = batchDto.BatchName;
            batch.BatchTiming = batchDto.BatchTiming;
            batch.BatchType = batchDto.BatchType;
            batch.CourseId = batchDto.CourseId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 5. Delete Batch
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
                return NotFound();

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}