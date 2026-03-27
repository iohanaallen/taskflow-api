using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlowApi.Data;
using TaskFlowApi.DTOs.TaskItem;
using TaskFlowApi.Models;

namespace TaskFlowApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue(ClaimTypes.Name);

        if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            return userId;

        var sub = User.FindFirstValue("sub");
        if (int.TryParse(sub, out userId))
            return userId;

        throw new UnauthorizedAccessException("Usuário não autenticado.");
    }

    [HttpPost]
public async Task<IActionResult> Create(CreateTaskItemDto dto)
{
    var userId = GetUserId();

    if (string.IsNullOrWhiteSpace(dto.Titulo))
        return BadRequest("O título da tarefa é obrigatório.");

    if (dto.Prioridade != "Baixa" && dto.Prioridade != "Media" && dto.Prioridade != "Alta")
        return BadRequest("Prioridade inválida. Use: Baixa, Media ou Alta.");

    var task = new TaskItem
    {
        Titulo = dto.Titulo,
        Descricao = dto.Descricao,
        Prioridade = dto.Prioridade,
        DataLimite = dto.DataLimite,
        Status = "Pendente",
        UserId = userId
    };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, new TaskItemResponseDto
        {
            Id = task.Id,
            Titulo = task.Titulo,
            Descricao = task.Descricao,
            Status = task.Status,
            Prioridade = task.Prioridade,
            DataCriacao = task.DataCriacao,
            DataLimite = task.DataLimite
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetMyTasks()
    {
        var userId = GetUserId();

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.DataCriacao)
            .Select(t => new TaskItemResponseDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Prioridade = t.Prioridade,
                DataCriacao = t.DataCriacao,
                DataLimite = t.DataLimite
            })
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();

        var task = await _context.Tasks
            .Where(t => t.Id == id && t.UserId == userId)
            .Select(t => new TaskItemResponseDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Prioridade = t.Prioridade,
                DataCriacao = t.DataCriacao,
                DataLimite = t.DataLimite
            })
            .FirstOrDefaultAsync();

        if (task is null)
            return NotFound("Tarefa não encontrada.");

        return Ok(task);
    }

   [HttpPut("{id}")]
public async Task<IActionResult> Update(int id, UpdateTaskItemDto dto)
{
    var userId = GetUserId();

    var task = await _context.Tasks
        .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    if (task is null)
        return NotFound("Tarefa não encontrada.");

    if (string.IsNullOrWhiteSpace(dto.Titulo))
        return BadRequest("O título da tarefa é obrigatório.");

    if (dto.Prioridade != "Baixa" && dto.Prioridade != "Media" && dto.Prioridade != "Alta")
        return BadRequest("Prioridade inválida. Use: Baixa, Media ou Alta.");

    if (dto.Status != "Pendente" && dto.Status != "EmAndamento" && dto.Status != "Concluida")
        return BadRequest("Status inválido. Use: Pendente, EmAndamento ou Concluida.");

    task.Titulo = dto.Titulo;
    task.Descricao = dto.Descricao;
    task.Status = dto.Status;
    task.Prioridade = dto.Prioridade;
    task.DataLimite = dto.DataLimite;

    await _context.SaveChangesAsync();

    return Ok(new
    {
        success = true,
        message = "Tarefa atualizada com sucesso."
    });
}

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();

        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (task is null)
            return NotFound("Tarefa não encontrada.");

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            success = true,
            message = "Tarefa removida com sucesso."
        });
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetByStatus(string status)
    {
        var userId = GetUserId();

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId && t.Status.ToLower() == status.ToLower())
            .OrderByDescending(t => t.DataCriacao)
            .Select(t => new TaskItemResponseDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Prioridade = t.Prioridade,
                DataCriacao = t.DataCriacao,
                DataLimite = t.DataLimite
            })
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("priority/{priority}")]
    public async Task<IActionResult> GetByPriority(string priority)
    {
        var userId = GetUserId();

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId && t.Prioridade.ToLower() == priority.ToLower())
            .OrderByDescending(t => t.DataCriacao)
            .Select(t => new TaskItemResponseDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Prioridade = t.Prioridade,
                DataCriacao = t.DataCriacao,
                DataLimite = t.DataLimite
            })
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueTasks()
    {
        var userId = GetUserId();

        var tasks = await _context.Tasks
            .Where(t => t.UserId == userId
                        && t.DataLimite.HasValue
                        && t.DataLimite.Value < DateTime.UtcNow
                        && t.Status != "Concluida")
            .OrderBy(t => t.DataLimite)
            .Select(t => new TaskItemResponseDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Status = t.Status,
                Prioridade = t.Prioridade,
                DataCriacao = t.DataCriacao,
                DataLimite = t.DataLimite
            })
            .ToListAsync();

        return Ok(tasks);
    }
     [HttpPatch("{id}/complete")]
public async Task<IActionResult> CompleteTask(int id)
{
    var userId = GetUserId();

    var task = await _context.Tasks
        .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    if (task is null)
        return NotFound("Tarefa não encontrada.");

    task.Status = "Concluida";

    await _context.SaveChangesAsync();

    return Ok(new
    {
        success = true,
        message = "Tarefa concluída com sucesso."
    });
  }
[HttpPatch("{id}/reopen")]
public async Task<IActionResult> ReopenTask(int id)
{
    var userId = GetUserId();

    var task = await _context.Tasks
        .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

    if (task is null)
        return NotFound("Tarefa não encontrada.");

    if (task.Status != "Concluida")
        return BadRequest("Apenas tarefas concluídas podem ser reabertas.");

    task.Status = "Pendente";

    await _context.SaveChangesAsync();

    return Ok(new
    {
        success = true,
        message = "Tarefa reaberta com sucesso."
    });
    }
}