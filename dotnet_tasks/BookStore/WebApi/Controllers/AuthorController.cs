using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public AuthorController(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public ActionResult GetAuthors()
    {
        GetAuthorsQuery query = new(context, mapper);
        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        GetAuthorDetailQuery query = new(context, mapper);
        query.AuthorId = id;
        GetAuthorDetailQueryValidator validator = new();
        validator.ValidateAndThrow(query);
        var result = query.Handle();

        return Ok(result);
    }

    [HttpPost]
    public ActionResult AddAuthor([FromBody] CreateAuthorCommand.CreateAuthorModel newAuthor)
    {
        CreateAuthorCommand command = new(context, mapper);
        command.Model = newAuthor;

        CreateAuthorCommandValidator validator = new();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public ActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommand.UpdateAuthorModel updatedAuthor)
    {
        UpdateAuthorCommand command = new(context);
        command.AuthorId = id;
        command.Model = updatedAuthor;

        UpdateAuthorCommandValidator validator = new();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteAuthor(int id)
    {
        DeleteAuthorCommand command = new(context);
        command.AuthorId = id;

        DeleteAuthorCommandValidator validator = new();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();
    }
}
