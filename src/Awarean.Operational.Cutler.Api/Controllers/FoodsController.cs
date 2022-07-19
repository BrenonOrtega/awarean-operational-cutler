using System.Net;
using Awarean.Operational.Cutler.Application.Queries;
using Awarean.Operational.Cutler.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Awarean.Operational.Cutler.Api.Controllers;

[Route("[controller]")]
public class FoodsController : ControllerBase
{
    private readonly ILogger<FoodsController> _logger;
    private readonly IFoodRepository _repo;

    public FoodsController(ILogger<FoodsController> logger, IFoodRepository repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public async Task<IActionResult> GetFoods() => Ok((_repo.GetFoods(new GetFoodQuery { PageSize = 100, NextPageToken = "Operational_Cutler_Foods_1112101" })));


    [HttpGet("[Action]")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => BadRequest();
}
