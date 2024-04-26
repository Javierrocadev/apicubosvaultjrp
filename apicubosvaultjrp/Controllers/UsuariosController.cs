using apicubosvaultjrp.Models;
using apicubosvaultjrp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

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



        //[Authorize]
        //[HttpGet]
        //[Route("[action]")]
        //public async Task<ActionResult<UsuarioCubo>>
        //    PerfilUsuario()
        //{
        //    //INTERNAMENTE, CUANDO RECIBIMOS EL TOKEN 
        //    //EL USUARIO ES VALIDADO Y ALMACENA DATOS 
        //    //COMO HttpContext.User.Identity.IsAuthenticated
        //    //COMO HEMOS INCLUIDO LA KEY DE LOS Claims, 
        //    //AUTOMATICAMENTE TAMBIEN TENEMOS DICHOS CLAIMS
        //    //COMO EN LAS APLICACIONES MVC
        //    Claim claim = HttpContext.User
        //        .FindFirst(x => x.Type == "UserData");
        //    //RECUPERAMOS EL JSON DEL EMPLEADO
        //    string jsonUser = claim.Value;
        //    UsuarioCubo usuario =
        //        JsonConvert.DeserializeObject<UsuarioCubo>(jsonUser);
        //    return usuario;
        //}



    }
}
