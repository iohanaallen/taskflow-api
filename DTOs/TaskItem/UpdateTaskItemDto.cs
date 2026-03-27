namespace TaskFlowApi.DTOs.TaskItem;

public class UpdateTaskItemDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Status { get; set; } = "Pendente";
    public string Prioridade { get; set; } = "Media";
    public DateTime? DataLimite { get; set; }
}