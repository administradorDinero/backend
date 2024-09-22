
namespace Entidades
{
    public class categoriaDto
    {
            public int Id { get; set; }
            public string CategoriaNo { get; set; }
            public List<TransaccionDto> Transacciones { get; set; } = new List<TransaccionDto>();
            public string? color { get; set; }

    }
}
