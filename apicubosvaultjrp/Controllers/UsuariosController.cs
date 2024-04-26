using apicubosvaultjrp.Models;
using apicubosvaultjrp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apicubosvaultjrp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioCubo>>>
    GetUsuarios()
        {
            return await this.repo.GetUsuariosAsync();
        }


        [HttpPost]
        public async Task<ActionResult> PostUsuario(UsuarioCubo usuario )
        {
            await this.repo.InsertUsuarioAsync(usuario.Id, usuario.Nombre, usuario.Email, usuario.Pass, usuario.Imagen);
            return Ok();
        }

    }
}
