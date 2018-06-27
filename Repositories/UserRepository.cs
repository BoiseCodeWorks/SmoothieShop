using System;
using System.Data;
using Dapper;
using smoothie_shack.Models;

namespace smoothie_shack.Repositories
{
  public class UserRepository
  {
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
      _db = db;
    }

    public User Register(UserRegistration creds)
    {
      try
      {

        var sql = @"
      INSERT INTO users (id, name, email, password)
      VALUES (@Id, @Name, @Email, @Password);
      ";
        creds.Password = BCrypt.Net.BCrypt.HashPassword(creds.Password);
        var id = Guid.NewGuid().ToString();
        _db.ExecuteScalar<string>(sql, new
        {
          Id = id,
          Name = creds.Name,
          Email = creds.Email,
          Password = creds.Password
        });

        return new User()
        {
          Id = id,
          Name = creds.Name,
          Email = creds.Email
        };
      }
      catch (Exception e)
      {
        Console.Write(e.Message);
        return null;
      }
    }



  }
}