using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Consinco.Complemento.Produto.Data.Configuration
{
    public interface IUniversal
    {
        Task<int> ExecuteNonQueryAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters);
        Task<object> ExecuteScalarAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters);
        Task<DataTable> ExecuteDataTableAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters);
        Task<DataRow> ExecuteDataRowAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters);
    }

    public class Universal : IUniversal
    {
        private readonly ILogger<IUniversal> _logger;
        private readonly OracleConnection _connection;
        private OracleTransaction _transaction;

        public Universal(string connectionString, ILogger<IUniversal> logger)
        {
            _logger = logger;
            _connection = new OracleConnection(connectionString);
        }

        #region Connection Management

        private async Task OpenConnectionAsync()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                    await _connection.OpenAsync();
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, "OpenConnectionAsync() - OracleException");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OpenConnectionAsync()");
                throw;
            }
        }

        private void CloseConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, "CloseConnection() - OracleException");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CloseConnection()");
                throw;
            }
        }

        #endregion

        #region Transaction Management

        private void BeginTransaction()
        {
            try
            {
                _transaction = _connection.BeginTransaction();
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, "BeginTransaction() - OracleException");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BeginTransaction()");
                throw;
            }
        }

        private void CancelTransaction()
        {
            try
            {
                _transaction.Rollback();
                _transaction = null;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, "CancelTransaction() - OracleException");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CancelTransactionAsync()");
                throw;
            }
        }

        private void CommitTransaction()
        {
            try
            {
                _transaction.Commit();
                _transaction = null;
            }
            catch (OracleException ex)
            {
                _logger.LogError(ex, "CommitTransaction() - OracleException");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CommitTransaction()");
                throw;
            }
        }

        #endregion

        #region Execute Methods

        /// <summary>
        /// Executa um comando que nao retorna dados (INSERT, UPDATE, DELETE, PROCEDURE sem cursor).
        /// Gerencia transacao automaticamente.
        /// </summary>
        public async Task<int> ExecuteNonQueryAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters)
        {
            try
            {
                await OpenConnectionAsync();
                BeginTransaction();

                using (var cmd = new OracleCommand(command, _connection))
                {
                    cmd.Transaction = _transaction;
                    cmd.CommandType = type;
                    cmd.BindByName = true; // Oracle: vincula parametros pelo nome, nao pela posicao

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    var affectedRows = await cmd.ExecuteNonQueryAsync();

                    CommitTransaction();

                    return affectedRows;
                }
            }
            catch (OracleException ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteNonQueryAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                CancelTransaction();
                throw;
            }
            catch (Exception ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteNonQueryAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                CancelTransaction();
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Executa um comando e retorna o valor da primeira coluna da primeira linha.
        /// Util para obter IDs gerados ou contagens.
        /// </summary>
        public async Task<object> ExecuteScalarAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters)
        {
            try
            {
                await OpenConnectionAsync();
                BeginTransaction();

                using (var cmd = new OracleCommand(command, _connection))
                {
                    cmd.Transaction = _transaction;
                    cmd.CommandType = type;
                    cmd.BindByName = true;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    var result = await cmd.ExecuteScalarAsync();

                    CommitTransaction();

                    return result;
                }
            }
            catch (OracleException ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteScalarAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                CancelTransaction();
                throw;
            }
            catch (Exception ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteScalarAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                CancelTransaction();
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Executa um comando e retorna a primeira linha do resultado.
        /// Retorna null caso nenhum registro seja encontrado.
        /// </summary>
        public async Task<DataRow> ExecuteDataRowAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters)
        {
            try
            {
                await OpenConnectionAsync();

                using (var cmd = new OracleCommand(command, _connection))
                {
                    cmd.CommandType = type;
                    cmd.BindByName = true;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);

                        if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                            return null;

                        return ds.Tables[0].Rows[0];
                    }
                }
            }
            catch (OracleException ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteDataRowAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                throw;
            }
            catch (Exception ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteDataRowAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Executa um comando e retorna todos os resultados em um DataTable.
        /// Ideal para consultas com multiplos registros (listagens, grids).
        /// </summary>
        public async Task<DataTable> ExecuteDataTableAsync(string command, CommandType type = CommandType.Text, params OracleParameter[] parameters)
        {
            try
            {
                await OpenConnectionAsync();

                using (var cmd = new OracleCommand(command, _connection))
                {
                    cmd.CommandType = type;
                    cmd.BindByName = true;

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);

                        return ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                    }
                }
            }
            catch (OracleException ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteDataTableAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                throw;
            }
            catch (Exception ex)
            {
                var errorMessage = CreateErrorMessage("ExecuteDataTableAsync", command, type, parameters);
                _logger.LogError(ex, errorMessage, command, type);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region Helpers

        private string CreateErrorMessage(string name, string command, CommandType type, params OracleParameter[] parameters)
        {
            try
            {
                var message = $"{name}(";
                message += "command: {command}, type: {type},";

                if (parameters != null)
                {
                    foreach (OracleParameter p in parameters)
                    {
                        message += $" {p.ParameterName}: {p.Value?.ToString()?.Replace("{\"", "").Replace("\"}", "")},";
                    }
                }

                message = message.Remove(startIndex: message.Length - 1, count: 1);
                message = message.Insert(startIndex: message.Length, value: ")");

                return message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao montar mensagem de erro. Metodo: {name}", name);
                return ex.Message;
            }
        }

        #endregion
    }
}
