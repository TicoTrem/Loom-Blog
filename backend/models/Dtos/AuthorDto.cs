using backend.models;

namespace backend.dtos
{
    public record AuthorCreateDto(string Name) : IModel;
    public record AuthorUpdateDto(string? Name) : IModel;


}
