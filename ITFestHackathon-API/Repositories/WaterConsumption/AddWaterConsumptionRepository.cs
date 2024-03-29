﻿using Dapper;
using ITFestHackathon_API.DTOs;
using ITFestHackathon_API.Interfaces;
using System.Data;

namespace ITFestHackathon_API.Repositories.WaterConsumption
{
    public class AddWaterConsumptionRepository : IAddWaterConsumptionRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public AddWaterConsumptionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddWaterConsumptionAsyncRepo(WaterConsumptionDTO waterConsumptionDTO)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IdUser", waterConsumptionDTO.IdUser);
            parameters.Add("@WaterGlasses", waterConsumptionDTO.WaterGlasses);
            parameters.Add("@Date", waterConsumptionDTO.Date);
            parameters.Add("Success", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("InsertWaterConsumption", parameters, commandType: CommandType.StoredProcedure);
                var result = parameters.Get<int>("Success");
                return result;
            }
        }
    }
}
