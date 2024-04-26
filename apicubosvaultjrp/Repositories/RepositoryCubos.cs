using apicubosvaultjrp.Data;
using apicubosvaultjrp.Models;
using Microsoft.EntityFrameworkCore;

namespace apicubosvaultjrp.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }


        //metodos
        //GET
        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        //cubos por marca
        public async Task<List<Cubo>> FindGroupAsync(string marca)
        {
            return await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        }



    }
}
