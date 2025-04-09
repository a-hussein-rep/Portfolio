using Microsoft.AspNetCore.Mvc;

using OnlineShopPlattfrom.WebAPI.Data.Entities;
using OnlineShopPlattfrom.WebAPI.Repositories.Interfaces;

namespace OnlineShopPlattfrom.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseProductController<T> : ControllerBase where T : Product
    {
        protected readonly IGenericRepository<T> Repository;

        protected BaseProductController(IGenericRepository<T> repository)
        {
            Repository = repository;
        }

        protected bool ProductExists(Guid id)
        {
            return Repository.GetByIdAsync(id) is not null;
        }
    }
}
