using backend.models;


namespace backend.dtos
{
    public record BlogPostCreateDto(string Title, string Content, int AuthorId) : IModel;
    public record BlogPostUpdateDto(string? Title, string? Content) : IModel;

}



