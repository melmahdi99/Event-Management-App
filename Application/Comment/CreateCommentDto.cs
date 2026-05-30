using System;

namespace Application.Comment;

public class CreateCommentDto
{
    public Guid ActivityId {get;set;}

    public string UserComment {get;set;}
}
