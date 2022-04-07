// Licence file C:\Users\jin.shi\Documents\ReversePOCO.txt not found.
// Please obtain your licence file at www.ReversePOCO.co.uk, and place it in your documents folder shown above.
// Defaulting to Trial version.


// ------------------------------------------------------------------------------------------------
// WARNING: Failed to load provider "System.Data.SqlClient" - 在与 SQL Server 建立连接时出现与网络相关的或特定于实例的错误。未找到或无法访问服务器。请验证实例名称是否正确并且 SQL Server 已配置为允许远程连接。 (provider: Named Pipes Provider, error: 40 - 无法打开到 SQL Server 的连接)
// Allowed providers:
//    "System.Data.Odbc"
//    "System.Data.OleDb"
//    "System.Data.OracleClient"
//    "System.Data.SqlClient"
//    "MySql.Data.MySqlClient"
//    "Microsoft.SqlServerCe.Client.4.0"

/*   在 System.Data.SqlClient.SqlInternalConnectionTds..ctor(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, Object providerInfo, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance, SqlConnectionString userConnectionOptions, SessionData reconnectSessionData, DbConnectionPool pool, String accessToken, Boolean applyTransientFaultHandling, SqlAuthenticationProviderManager sqlAuthProviderManager)
   在 System.Data.SqlClient.SqlConnectionFactory.CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, Object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
   在 System.Data.ProviderBase.DbConnectionFactory.CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
   在 System.Data.ProviderBase.DbConnectionPool.CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   在 System.Data.ProviderBase.DbConnectionPool.UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   在 System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   在 System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   在 System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   在 System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   在 System.Data.ProviderBase.DbConnectionClosed.TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   在 System.Data.SqlClient.SqlConnection.TryOpenInner(TaskCompletionSource`1 retry)
   在 System.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry)
   在 System.Data.SqlClient.SqlConnection.Open()
   在 Microsoft.VisualStudio.TextTemplating3DB1E5E173EE24C5733C8E6384BE0CB4B914EF02DF4E136E4D12B37CC6165DDA5E4DC9B64B86B42622FE072470B1B1C247955471BFEF0553A32B9C1BF8ED61F1.GeneratedTextTransformation.DatabaseReader.Init() 位置 C:\Users\jin.shi\source\repos\MyTool\ConsoleAppPOJO\EF.Reverse.POCO.v3.ttinclude:行号 11761
   在 Microsoft.VisualStudio.TextTemplating3DB1E5E173EE24C5733C8E6384BE0CB4B914EF02DF4E136E4D12B37CC6165DDA5E4DC9B64B86B42622FE072470B1B1C247955471BFEF0553A32B9C1BF8ED61F1.GeneratedTextTransformation.SqlServerDatabaseReader.Init() 位置 C:\Users\jin.shi\source\repos\MyTool\ConsoleAppPOJO\EF.Reverse.POCO.v3.ttinclude:行号 14932
   在 Microsoft.VisualStudio.TextTemplating3DB1E5E173EE24C5733C8E6384BE0CB4B914EF02DF4E136E4D12B37CC6165DDA5E4DC9B64B86B42622FE072470B1B1C247955471BFEF0553A32B9C1BF8ED61F1.GeneratedTextTransformation.Generator.Init(String singleDbContextSubNamespace) 位置 C:\Users\jin.shi\source\repos\MyTool\ConsoleAppPOJO\EF.Reverse.POCO.v3.ttinclude:行号 3873*/
// ------------------------------------------------------------------------------------------------

