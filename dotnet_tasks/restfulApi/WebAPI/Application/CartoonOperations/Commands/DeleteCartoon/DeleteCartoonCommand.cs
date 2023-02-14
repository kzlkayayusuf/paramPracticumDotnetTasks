using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPI.DBOperations;
using static WebAPI.Application.CartoonOperations.Queries.GetCartoonDetail.GetCartoonDetailQuery;

namespace WebAPI.Application.CartoonOperations.Commands.DeleteCartoon;

public class DeleteCartoonCommand
{
    public int CartoonId { get; set; }
    private readonly ICartoonDbContext context;
    private readonly IMapper mapper;

    public DeleteCartoonCommand(ICartoonDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public ServiceResponse<CartoonDetailViewModel> Handle()
    {
        var cartoon = context.Cartoons.Include(x => x.Characters).Where(c => c.ID == CartoonId).FirstOrDefault();
        //var cartoon = context.Cartoons.FirstOrDefault(c => c.ID == CartoonId);
        if (cartoon is null)
            throw new InvalidOperationException($"The cartoon with Id '{CartoonId}' to be deleted was not found");

        CartoonDetailViewModel vm = mapper.Map<CartoonDetailViewModel>(cartoon);

        context.Cartoons.Remove(cartoon);
        context.SaveChanges();

        return new ServiceResponse<CartoonDetailViewModel>(vm);
    }
}
