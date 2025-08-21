using System;
using System.Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
        InitializeDatabase();
    }

    /// <summary>
    /// Initialize the database and create tables/functions if they don't exist.
    /// </summary>
    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString); // Open a connection to the SQLite database
        connection.Open();

        // Create Users table
        var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL
            );";
        createTableCommand.ExecuteNonQuery();

        // Create functions (SQLite's equivalent to stored procedures)
        CreateDatabaseFunctions(connection);
    }

    /// <summary>
    /// Create custom functions for database operations.
    /// </summary>
    private static void CreateDatabaseFunctions(SqliteConnection connection)
    {
        connection.CreateFunction("GetUserById", (int id) =>
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Users WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return $"{reader.GetInt32(0)}|{reader.GetString(1)}";
            }
            return null;
        });
    }

    public async Task AddUserAsync(User user)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Users (Name) 
            VALUES (@name);
            SELECT last_insert_rowid();";
        command.Parameters.AddWithValue("@name", user.Name);

        var newId = await command.ExecuteScalarAsync();
        user.Id = Convert.ToInt32(newId);
    }

    public async Task DeleteUserAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Users WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        var rowsAffected = await command.ExecuteNonQueryAsync();
        if (rowsAffected == 0)
        {
            throw new InvalidOperationException($"User with ID {id} not found.");
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = new List<User>();

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name FROM Users ORDER BY Id";

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            users.Add(new User
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name")
            });
        }

        return users;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name FROM Users WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new User
            {
                Id = reader.GetInt32("Id"),
                Name = reader.GetString("Name")
            };
        }

        return null;
    }

    public async Task UpdateUserAsync(User user)
    {
        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Users 
            SET Name = @name 
            WHERE Id = @id";
        command.Parameters.AddWithValue("@name", user.Name);
        command.Parameters.AddWithValue("@id", user.Id);

        var rowsAffected = await command.ExecuteNonQueryAsync();
        if (rowsAffected == 0)
        {
            throw new InvalidOperationException($"User with ID {user.Id} not found.");
        }
    }
}
