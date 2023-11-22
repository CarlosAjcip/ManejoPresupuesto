using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioCategorias
    {
        Task Actualizar(Categoria categoria);
        Task Crear(Categoria categoria);
        Task Eliminar(int id_categorias);
        Task<IEnumerable<Categoria>> Obtener(int usuarioId);
        Task<IEnumerable<Categoria>> Obtener(int id_usuarios, TipoOperacion id_tiposOp);
        Task<Categoria> ObtenerPorId(int id_categoria, int id_usuarios);
    }

    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly string connectionString;

        public RepositorioCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("defaultConnection");
        }

        public async Task Crear(Categoria categoria)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO Categorias (Nombre,id_usuarios,id_tiposOp)
                                                            VALUES(@Nombre,@id_usuarios,@id_tiposOp)
                                                            SELECT SCOPE_IDENTITY();", categoria);
            categoria.id_categorias = id;
        }

        public async Task<IEnumerable<Categoria>> Obtener(int id_usuarios)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categoria>(@"SELECT * FROM categorias WHERE id_usuarios = @id_usuarios", new { id_usuarios });
        }

        public async Task<Categoria> ObtenerPorId(int id_categoria, int id_usuarios)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Categoria>(@"SELECT * FROM categorias
                                                            where id_usuarios = @id_usuarios AND id_usuarios = @id_usuarios", new { id_categoria, id_usuarios });
        }

        public async Task Actualizar(Categoria categoria)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE categorias set
                        Nombre = @Nombre, id_tiposOp = @id_tiposOp
                        WHERE id_categorias = @id_categorias;", categoria);

        }

        public async Task Eliminar(int id_categorias)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"DELETE FROM categorias
                                            WHERE id_categorias = @id_categorias;", new { id_categorias });
        }

        public async Task<IEnumerable<Categoria>> Obtener(int id_usuarios, TipoOperacion id_tiposOp)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Categoria>(@"SELECT * FROM categorias WHERE id_usuarios = @id_usuarios AND id_tiposOp = @id_tiposOp", new { id_usuarios, id_tiposOp });
        }
    }
}
