namespace LuxuryProperty.Application.DTOs
{
  public class PropertyFilterDto
  {
    /// <summary>
    /// Nombre de la propiedad a buscar (opcional)
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Dirección de la propiedad a buscar (opcional)
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Precio mínimo (opcional)
    /// </summary>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Precio máximo (opcional)
    /// </summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>
    /// Página actual para paginación (opcional, default = 1)
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// Cantidad de elementos por página (opcional, default = 10)
    /// </summary>
    public int? PageSize { get; set; }
  }
}
