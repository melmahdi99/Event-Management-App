namespace Application.Comment;
public interface ICommentService
{
    public Task<IEnumerable<ReadCommentDto>> GetComments(Guid id);

    public Task<ReadCommentDto> CreateComment(CreateCommentDto c);
}