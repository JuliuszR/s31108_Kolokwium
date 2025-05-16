﻿using Kolokwium_s31108.Exceptions;
using Kolokwium_s31108.Models.Dtos;
using Microsoft.Data.SqlClient;

namespace Kolokwium_s31108.Services;

public class DbServices : IDbServices
{
    private readonly string _connectionString;

    public DbServices(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
    }

    public async Task<VisitDetailsDto> GetVisitDetailsAsync(int visitId)
    {
        var query =
            @"SELECT v.date, c.first_name, c.last_name, c.date_of_birth, m.mechanic_id, m.licence_number, s.name, vs.service_fee
            FROM visit v
            Join client c on c.client_id = v.client_id
            JOin mechanic m on m.mechanic_id = v.mechanic_id
            join visit_service vs on vs.visit_id = v.visit_id
            join service s on s.service_id = vs.service_id
            WHERe v.visit_id = @visitId;";
        
     await using SqlConnection connection = new SqlConnection(_connectionString);
     await using SqlCommand command = new SqlCommand();
     
     command.Connection = connection;
     command.CommandText = query;
     await connection.OpenAsync();
     
     command.Parameters.AddWithValue("@visitId", visitId);
     var reader = await command.ExecuteReaderAsync();
     
     VisitDetailsDto? visitDetails = null;

     while (await reader.ReadAsync())
     {
         if (visitDetails is null)
         {
             visitDetails = new VisitDetailsDto()
             {
                 Date = reader.GetDateTime(0),
                 Client = new ClientDetailsDto()
                 {
                     FirstName = reader.GetString(1),
                     LastName = reader.GetString(2),
                     DateOfBirth = reader.GetDateTime(3),
                 },
                 Mechanic = new MechanicDetailsDto()
                 {
                     MechanicId = reader.GetInt32(4),
                     LicenceNumber = reader.GetString(5),
                 },
                 VisitServices = new List<VisitServicesDto>()
             };
         }
         string serviceName = reader.GetString(6);
         
         var service = visitDetails.VisitServices.FirstOrDefault(e => e.Name == serviceName);
         if (service is null)
         {
             service = new VisitServicesDto()
             {
                 Name = serviceName,
                 ServiceFee = reader.GetDecimal(7),
             };
             visitDetails.VisitServices.Add(service);
         }
     }

     if (visitDetails is null)
     {
         throw new NotFoundException("Id not found");
     }
     return visitDetails;
    }
}