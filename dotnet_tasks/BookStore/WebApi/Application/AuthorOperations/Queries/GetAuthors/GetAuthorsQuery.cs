using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<AuthorsViewModel> Handle()
    {
        var authorList = context.Authors.OrderBy(a => a.Id).ToList<Author>();
        return mapper.Map<List<AuthorsViewModel>>(authorList);
    }

    public class AuthorsViewModel
    {
        public string FullName { get; set; }
        public string Birthday { get; set; }
    }
}
