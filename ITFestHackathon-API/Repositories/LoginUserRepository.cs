﻿using System.Data;
using Dapper;
using ITFestHackathon_API.DTOs;
using ITFestHackathon_API.Interfaces;

namespace ITFestHackathon_API.Repositories
{
    public class LoginUserRepository : ILoginUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public LoginUserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> LoginUserAsyncRepo(UserCredentialsDTO userCredentialsDTO)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@Email", userCredentialsDTO.Email);
            parameters.Add("@Password", userCredentialsDTO.Password);
            parameters.Add("@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = _connectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("LoginUser", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("UserID");
            }
        }
    }
}
