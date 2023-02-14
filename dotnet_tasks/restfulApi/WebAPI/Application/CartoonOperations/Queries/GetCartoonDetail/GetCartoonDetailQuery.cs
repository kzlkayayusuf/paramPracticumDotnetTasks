using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail;

public class GetCartoonDetailQuery
{
    public int CartoonId { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCartoonDetailQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CartoonDetailViewModel> Handle()
    {
        var cartoon = context.Cartoons.Include(x => x.Characters).Where(c => c.ID == CartoonId).SingleOrDefault();
        if (cartoon is null)
            throw new Exception($"Cartoon with Id '{CartoonId}' not found.");
        CartoonDetailViewModel vm = mapper.Map<CartoonDetailViewModel>(cartoon);

        return new ServiceResponse<CartoonDetailViewModel>(vm);
    }

    public class CartoonDetailViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public string ReleaseDate { get; set; }
        public string Topic { get; set; }
        public List<string> Characters { get; set; }
    }
}
