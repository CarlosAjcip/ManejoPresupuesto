using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTransaccines
    {
        Task Actualizar(Transaccion transaccion, decimal montoAnterior, int cuentaAnterior);
        Task Borrar(int id_transacciones);
        Task Crear(Transaccion transaccion);
        Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo);
        Task<Transaccion> ObtenerPorId(int id_transacciones, int id_usuarios);
        Task<IEnumerable<Transaccion>> ObtenerPorUsuarioId(ParametroObtenerTransaccionesPorUsuario modelo);
    }
    public class RepositorioTransacciones : IRepositorioTransaccines
    {
        private readonly string connectionString;

        public RepositorioTransacciones(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }
        public async Task Crear(Transaccion transaccion)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Transaccion_Insertar",
                new
                {
                    transaccion.fechaTransaccion,
                    transaccion.monto,
                    transaccion.nota,
                    transaccion.id_usuarios,
                    transaccion.id_cuenta,
                    transaccion.id_categorias
                },
                commandType: System.Data.CommandType.StoredProcedure);
            transaccion.id_transacciones = id;
        }

        public async Task Actualizar(Transaccion transaccion,decimal montoAnterior, int cuentaAnteriorId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("Transacciones_Actualizar",
                new
                {
                    transaccion.id_transacciones,
                    transaccion.fechaTransaccion,
                    transaccion.monto,
                    transaccion.id_categorias,
                    transaccion.id_cuenta,
                    transaccion.nota,
                    montoAnterior,
                    cuentaAnteriorId
                },  commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<Transaccion> ObtenerPorId(int id_transacciones, int id_usuarios)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Transaccion>(@"SELECT tr.*, cat.id_tiposOp FROM Transacciones tr
                INNER JOIN categorias cat
                ON cat.id_categorias = tr.id_categorias
                WHERE tr.id_transacciones = @id_transacciones AND tr.id_usuarios = @id_usuarios", new { id_transacciones, id_usuarios });
        }

        public async Task Borrar(int id_transacciones)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync("Transacciones_Borrar", new { id_transacciones }, commandType: System.Data.CommandType.StoredProcedure);
        }

        //Movimientos cuentas 
        public async Task<IEnumerable<Transaccion>> ObtenerPorCuentaId(ObtenerTransaccionesPorCuenta modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaccion>(@"SELECT t.id_transacciones,t.monto,t.fechaTransaccion,c.Nombre as Categoria,
            cu.Nombre as Cuenta, c.id_tiposOp
            FROM Transacciones t
            inner join categorias c
            on c.id_categorias = t.id_categorias
            inner join Cuentas cu
            on cu.id_cuenta = t.id_cuenta
            WHERE t.id_cuenta = @id_cuenta AND t.id_usuarios= @id_usuarios 
            AND fechaTransaccion between @FechaInicio  AND @FechaFin", modelo);
        }

        public async Task<IEnumerable<Transaccion>> ObtenerPorUsuarioId(ParametroObtenerTransaccionesPorUsuario modelo)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaccion>(@"SELECT t.id_transacciones,t.monto,t.fechaTransaccion,c.Nombre as Categoria,
            cu.Nombre as Cuenta, c.id_tiposOp
            FROM Transacciones t
            inner join categorias c
            on c.id_categorias = t.id_categorias
            inner join Cuentas cu
            on cu.id_cuenta = t.id_cuenta
            WHERE  t.id_usuarios= @id_usuarios 
            AND fechaTransaccion between @FechaInicio  AND @FechaFin
            ORDER BY t.FechaTransaccion DESC", modelo);
        }
    }
}
