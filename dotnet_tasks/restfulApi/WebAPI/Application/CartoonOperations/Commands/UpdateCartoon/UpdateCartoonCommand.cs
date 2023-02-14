using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail.GetCartoonDetailQuery;

namespace WebAPI.Application.CartoonOperations.Commands.UpdateCartoon;

public class UpdateCartoonCommand
{
    public UpdateCartoonModel Model { get; set; }
    public int CartoonId { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public UpdateCartoonCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CartoonDetailViewModel> Handle()
    {
        var cartoon = context.Cartoons.Include(x => x.Characters).Where(c => c.ID == CartoonId).SingleOrDefault();
        if (cartoon is null)
            throw new InvalidOperationException($"The cartoon with Id '{CartoonId}' to be updated was not found");

        //cartoon = mapper.Map<Cartoon>(Model);
        //cartoon.ID = CartoonId;
        //cartoon.Characters = default;

        cartoon.Name = Model.Name != default ? Model.Name : cartoon.Name;
        cartoon.Genre = Model.Genre != default ? Model.Genre : cartoon.Genre;
        cartoon.ReleaseDate = Model.ReleaseDate != default ? Model.ReleaseDate : cartoon.ReleaseDate;
        cartoon.Topic = Model.Topic != default ? Model.Topic : cartoon.Topic;

        context.SaveChanges();

        CartoonDetailViewModel vm = mapper.Map<CartoonDetailViewModel>(cartoon);
        return new ServiceResponse<CartoonDetailViewModel>(vm);
    }


    public class UpdateCartoonModel
    {
        public string Name { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Topic { get; set; }
    }
}
