namespace TaskFlowApi.DTOs.TaskItem;

public class CreateTaskItemDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Prioridade { get; set; } = "Media";
    public DateTime? DataLimite { get; set; }
}