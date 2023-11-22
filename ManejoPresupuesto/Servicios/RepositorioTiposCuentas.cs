using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id_tiposCuen);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int id_usuarios);
        Task<IEnumerable<TipoCuenta>> Obtener(int id_usuarios);
        Task<TipoCuenta> ObtenerPorId(int id_tiposCuen, int id_usuarios);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentas);
    }
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        //insertando datos a la BD
        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                ("TiposCuentasInsertar",
                new
                {
                    id_usuarios = tipoCuenta.id_usuarios,
                    Nombre = tipoCuenta.Nombre
                },
                commandType: System.Data.CommandType.StoredProcedure);

            tipoCuenta.id_tiposCuen = id;
        }

        //ver que no se repita el registro con el mismo id de usuarios 
        public async Task<bool> Existe(string nombre, int id_usuarios)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1
                FROM TiposCuentas
                WHERE Nombre = @Nombre AND id_usuarios = @id_usuarios;", new { nombre, id_usuarios });
            return existe == 1;
        }

        //listar las datos de la BD
        public async Task<IEnumerable<TipoCuenta>> Obtener(int id_usuarios)
        {
            using var con = new SqlConnection(connectionString);
            return await con.QueryAsync<TipoCuenta>($@"
            select id_tiposCuen,Nombre,Orden
            from TiposCuentas
            where id_usuarios = @id_usuarios
            ORDER BY Orden;
            ", new { id_usuarios });
        }

        //editar los datos 
        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"update TiposCuentas
            set Nombre = @Nombre
            where id_tiposCuen = @id_tiposCuen", tipoCuenta);
        }

        //obtener por id
        public async Task<TipoCuenta> ObtenerPorId(int id_tiposCuen, int id_usuarios)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(
                @"select id_tiposCuen,Nombre,Orden
                from TiposCuentas
                where id_tiposCuen = @id_tiposCuen
                AND id_usuarios = @id_usuarios", new { id_tiposCuen, id_usuarios });
        }

        //eliminar un campo de la BD
        public async Task Borrar(int id_tiposCuen)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE TiposCuentas
            WHERE id_tiposCuen = @id_tiposCuen", new { id_tiposCuen });

        }

        //ordenar los tipos cuentas desde la web
        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentas)
        {
            var query = "UPDATE TiposCuentas set Orden = @Orden where id_tiposCuen = @id_tiposCuen;";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tipoCuentas);
        }
    }
}
