using System;
using System.Linq;
using WebAPI.DBOperations;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail.GetCartoonDetailQuery;

namespace WebAPI.Application.CartoonOperations.Commands.CreateCartoon;

public class CreateCartoonCommand
{
    public CreateCartoonModel Model { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public CreateCartoonCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CartoonDetailViewModel> Handle()
    {
        var newCartoon = context.Cartoons.FirstOrDefault(c => c.Name.ToLower() == Model.Name.ToLower());
        if (newCartoon is not null)
            throw new InvalidOperationException("That Cartoon already exists");

        newCartoon = mapper.Map<Cartoon>(Model);

        context.Cartoons.Add(newCartoon);
        context.SaveChanges();

        CartoonDetailViewModel vm = mapper.Map<CartoonDetailViewModel>(newCartoon);
        return new ServiceResponse<CartoonDetailViewModel>(vm);
    }

    public class CreateCartoonModel
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Topic { get; set; }
    }
}
