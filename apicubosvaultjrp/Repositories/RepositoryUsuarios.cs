using apicubosvaultjrp.Data;
using apicubosvaultjrp.Models;
using Microsoft.EntityFrameworkCore;

namespace apicubosvaultjrp.Repositories
{
    public class RepositoryUsuarios
    {
        private CubosContext context;

        public RepositoryUsuarios(CubosContext context)
        {
            this.context = context;
        }

        //seguridad
        public async Task<UsuarioCubo> LogInUsuarioAsync(string nombre, int id)
        {
            return await this.context.Usuarios
                .Where(x => x.Nombre == nombre && x.Id == id).FirstOrDefaultAsync();
        }

        //GET
        public async Task<List<UsuarioCubo>> GetUsuariosAsync()
        {
            return await this.context.Usuarios.ToListAsync();
        }


        //PUT
        public async Task InsertUsuarioAsync(int id, string nombre, string email, string pass, string imagen)
        {
            //construir el objeto
            UsuarioCubo usuario = new UsuarioCubo();
            usuario.Id = id;
            usuario.Nombre = nombre;
            usuario.Email = email;
            usuario.Pass = pass;
            usuario.Imagen = imagen;
            //añadir
            this.context.Usuarios.Add(usuario);
            await this.context.SaveChangesAsync();
        }


    }
}
