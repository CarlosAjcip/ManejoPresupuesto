using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioCuentas
    {
        Task Actualizar(CuentasCreacionViewModel cuentas);
        Task<IEnumerable<Cuentas>> Buscar(int usuarioId);
        Task Crear(Cuentas cuentas);
        Task Elminar(int id_cuenta);
        Task<Cuentas> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioCuentas : IRepositorioCuentas
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;

        public RepositorioCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task Crear(Cuentas cuentas)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"insert into Cuentas (Nombre,Balance,Descripcion,id_TiposCuen)
                                                        values (@Nombre,@Balance,@Descripcion,@id_TiposCuen);
                                                        select SCOPE_IDENTITY();", cuentas);
            cuentas.id_cuenta = id;
        }

        public async Task<IEnumerable<Cuentas>> Buscar(int id_usuarios)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Cuentas>(@"
                     select c.id_cuenta,c.Nombre, c.Balance,tc.Nombre as tipoCuenta, c.id_TiposCuen  from Cuentas c
                     inner join TiposCuentas tc
                     on tc.id_tiposCuen = c.id_TiposCuen
                     where tc.id_usuarios = @id_usuarios
                     order by tc.Orden", new { id_usuarios });
        }

        public async Task<Cuentas> ObtenerPorId(int id_cuenta, int id_usuarios)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Cuentas>(
            @"select c.id_cuenta,c.Nombre, c.Balance,c.Descripcion, tc.id_tiposCuen
            from Cuentas c
            inner join TiposCuentas tc
            on tc.id_tiposCuen = c.id_TiposCuen
            where tc.id_usuarios = @id_usuarios AND c.id_cuenta = @id_cuenta", new { id_usuarios, id_cuenta });
        }

        public async Task Actualizar(CuentasCreacionViewModel cuentas)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE Cuentas
            set Nombre = @Nombre,Balance = @Balance, Descripcion = @Descripcion,
            id_TiposCuen = @id_TiposCuen
            where id_cuenta = @id_cuenta;", cuentas);
        }

        //Elminar La Cuneta
        public async Task Elminar(int id_cuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE FROM Cuentas where id_cuenta = @id_cuenta", new { id_cuenta });
        }
    }
}
