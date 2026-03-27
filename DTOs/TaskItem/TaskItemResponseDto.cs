namespace TaskFlowApi.DTOs.TaskItem;

public class TaskItemResponseDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Prioridade { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public DateTime? DataLimite { get; set; }
}