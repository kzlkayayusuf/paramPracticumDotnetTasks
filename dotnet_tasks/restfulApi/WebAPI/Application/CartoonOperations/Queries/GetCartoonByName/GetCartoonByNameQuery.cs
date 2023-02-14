using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonByName;

public class GetCartoonByNameQuery
{
    public string CartoonName { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCartoonByNameQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<GetCartoonByNameViewModel> Handle()
    {
        var cartoon = context.Cartoons.Include(x => x.Characters).Where(c => c.Name.ToUpper().Contains(CartoonName.ToUpper())).SingleOrDefault();
        if (cartoon is null)
            throw new Exception($"Cartoon with Name '{CartoonName}' not found.");
        GetCartoonByNameViewModel vm = mapper.Map<GetCartoonByNameViewModel>(cartoon);

        return new ServiceResponse<GetCartoonByNameViewModel>(vm);
    }

    public class GetCartoonByNameViewModel
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public string ReleaseDate { get; set; }
        public string Topic { get; set; }
        public List<string> Characters { get; set; }
    }
}

