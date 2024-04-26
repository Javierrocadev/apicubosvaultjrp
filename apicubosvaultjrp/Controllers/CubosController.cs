using apicubosvaultjrp.Models;
using apicubosvaultjrp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apicubosvaultjrp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }
        //metodos
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Cubo>>>
            GetCubos()
        {
            return await this.repo.GetCubosAsync();
        }
        [Authorize]
        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>> GetCubosMarca(string marca)
        {
            return await this.repo.FindGroupAsync(marca);
        }

    }
}
