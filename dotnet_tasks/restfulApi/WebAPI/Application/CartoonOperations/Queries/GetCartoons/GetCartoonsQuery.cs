using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;

namespace WebAPI.Application.CartoonOperations.Queries.GetCartoons;

public class GetCartoonsQuery
{
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public GetCartoonsQuery(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<List<CartoonsViewModel>> Handle()
    {
        var cartoonList = context.Cartoons.Include(x => x.Characters).OrderBy(c => c.ID).ToList<Cartoon>();
        List<CartoonsViewModel> vm = mapper.Map<List<CartoonsViewModel>>(cartoonList);

        return new ServiceResponse<List<CartoonsViewModel>>(vm);
    }

    public class CartoonsViewModel
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public string ReleaseDate { get; set; }
        public string Topic { get; set; }
        public List<string> Characters { get; set; }
    }
}
