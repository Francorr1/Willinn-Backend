# Use the SQL Server image
FROM mcr.microsoft.com/mssql/server:2022-latest



# Start SQL Server
CMD ["/opt/mssql/bin/sqlservr"]