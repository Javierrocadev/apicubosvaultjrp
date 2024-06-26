﻿using apicubosvaultjrp.Helpers;
using apicubosvaultjrp.Models;
using apicubosvaultjrp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace apicubosvaultjrp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryUsuarios repo;
        private HelperActionServicesOAuth helper;

        public AuthController(RepositoryUsuarios repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        //NECESITAMOS UN METODO POST PARA VALIDAR EL  

        //USUARIO Y QUE RECIBIRA LoginModel 

        [HttpPost]

        [Route("[action]")]

        public async Task<ActionResult> Login(LoginModel model)

        {

            //BUSCAMOS AL EMPLEADO EN NUESTRO REPO 

            UsuarioCubo usuario=

                await this.repo.LogInUsuarioAsync

                (model.UserName, int.Parse(model.Password));

            if (usuario == null)

            {

                return Unauthorized();

            }

            else

            {

                //DEBEMOS CREAR UNAS CREDENCIALES PARA  

                //INCLUIRLAS DENTRO DEL TOKEN Y QUE ESTARAN  

                //COMPUESTAS POR EL SECRET KEY CIFRADO Y EL TIPO 

                //DE CIFRADO QUE DESEEMOS INCLUIR EN EL TOKEN 

                SigningCredentials credentials =

                    new SigningCredentials(

                        this.helper.GetKeyToken()

                        , SecurityAlgorithms.HmacSha256);

                //EL TOKEN SE GENERA CON UNA CLASE Y  

                //DEBEMOS INDICAR LOS ELEMENTOS QUE ALMACENARA  

                //DENTRO DE DICHO TOKEN, POR EJEMPLO, ISSUER, 

                //AUDIENCE O EL TIEMPO DE VALIDACION DEL TOKEN 



                string jsonUser =
                   JsonConvert.SerializeObject(usuario);
                //ESTO DEBE IR CIFRADO POR NOSOTROS

                //CREAMOS UN ARRAY DE CLAIMS CON TODA 
                //LA INFORMACION QUE DESEAMOS GUARDAR EN EL TOKEN
                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonUser)
                };



                JwtSecurityToken token =

                    new JwtSecurityToken(

                        issuer: this.helper.Issuer,

                        audience: this.helper.Audience,

                        signingCredentials: credentials,

                        expires: DateTime.UtcNow.AddMinutes(30),

                        notBefore: DateTime.UtcNow

                        );

                //POR ULTIMO, DEVOLVEMOS UNA RESPUESTA AFIRMATIVA 

                //CON UN OBJETO ANONIMO EN FORMATO JSON 

                return Ok(

                    new

                    {

                        response =

                        new JwtSecurityTokenHandler()

                        .WriteToken(token)

                    });

            }

        }




    }
}
