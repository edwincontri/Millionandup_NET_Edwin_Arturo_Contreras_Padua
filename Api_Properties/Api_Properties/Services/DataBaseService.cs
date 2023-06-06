using Api_Properties.DataContext;
using Api_Properties.Interfaces;
using Api_Properties.Models;
using Api_Properties.Models.DataTransferObjects;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace Api_Properties.Services
{
    public class DataBaseService : IDatabaseService
    {        
        private readonly DapperContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public DataBaseService(DapperContext context, IMapper mapper, IConfiguration config) 
        {            
            _mapper = mapper;
            _context = context;
            _config = config;
        }

        public async Task<List<Property>> GetProperty(string name)
        {
            using var conectSql = _context.CreateConnection();

            try
            {
                if (conectSql.State == ConnectionState.Closed)
                    conectSql.Open();

                string sqlQuery = @"SELECT * 
                                    FROM [dbo].[Property]
                                    WHERE [Name] = @Name";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Name", name, DbType.String);

                var resultDto = await conectSql.QueryAsync<PropertyDto>(sqlQuery, parameters);
                List<Property> result = _mapper.Map<List<Property>>(resultDto);

                return result;
            }
            finally
            {
                conectSql.Close();
            }
        }

        public async Task InsertProperty(Property property)
        {
            using var conectSql = _context.CreateConnection();

            try
            {
                if (conectSql.State == ConnectionState.Closed)
                    conectSql.Open();

                string sqlQuery = @"INSERT INTO [dbo].[Property]
                                               ([IdProperty], [Name], [Address], [Price]
                                               ,[CodeInternal], [Year], [IdOwner])
                                         VALUES
                                               (@IdProperty, @Name, @Address, @Price
                                               ,@CodeInternal, @Year, @IdOwner)";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("IdProperty", property.PkProperty, DbType.String);
                parameters.Add("Name", property.PropertyName, DbType.String);
                parameters.Add("Address", property.PropertyAddress, DbType.String);
                parameters.Add("Price", property.PropertyPrice, DbType.Decimal);
                parameters.Add("CodeInternal", property.PropertyCodeInternal, DbType.String);
                parameters.Add("Year", property.PropertyYear, DbType.Int16);
                parameters.Add("IdOwner", property.FkOwner, DbType.String);

                await conectSql.ExecuteAsync(sqlQuery, parameters);
            }
            finally
            {
                conectSql.Close();
            }
        }

        public async Task InsertImageProperty(PropertyImage propertyImage)
        {
            using var conectSql = _context.CreateConnection();

            try
            {
                if (conectSql.State == ConnectionState.Closed)
                    conectSql.Open();

                string file = propertyImage.File;
                string sqlQuery = @"INSERT INTO [dbo].[PropertyImage]
                                               ([IdPropertyImage], [IdProperty], [File], [Enabled])
                                         VALUES
                                               (@IdPropertyImage, @IdProperty, @File, @Enabled)";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("IdPropertyImage", propertyImage.IdPropertyImage, DbType.String);
                parameters.Add("IdProperty", propertyImage.IdProperty, DbType.String);
                parameters.Add("File", file, DbType.String);
                parameters.Add("Enabled", propertyImage.Enabled, DbType.Boolean);

                await conectSql.ExecuteAsync(sqlQuery, parameters);
            }
            finally
            {
                conectSql.Close();
            }
        }

        public async Task UpdatePriceProperty(UpdatePrice property)
        {
            using var conectSql = _context.CreateConnection();

            try
            {
                if(conectSql.State == ConnectionState.Closed)
                    conectSql.Open();                
                
                string sqlQuery = @"UPDATE [dbo].[Property]
                                       SET Price = @Price
                                     WHERE IdProperty = @IdProperty";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Price", property.price, DbType.Decimal);
                parameters.Add("IdProperty", property.idProperty, DbType.String);

                await conectSql.ExecuteAsync(sqlQuery, parameters);
            }
            finally
            {
                conectSql.Close();
            }
        }

        public async Task<bool> ExistsProperty(string idProperty)
        {
            using var conectSql = _context.CreateConnection();

            try
            {
                if (conectSql.State == ConnectionState.Closed)
                    conectSql.Open();

                string sqlQuery = @"SELECT * 
                                    FROM [dbo].[Property]
                                    WHERE IdProperty = @IdProperty";

                DynamicParameters parameters = new DynamicParameters();                
                parameters.Add("IdProperty", idProperty, DbType.String);

                var result = await conectSql.QueryAsync(sqlQuery, parameters);
                return result.Any();
            }
            finally
            {
                conectSql.Close();
            }
        }

        public async Task UpdateAllProperty(Property property)
        {
            using var conectSql = _context.CreateConnection();

            try
            {
                if(conectSql.State == ConnectionState.Closed)
                    conectSql.Open();

                string sqlQuery = @"UPDATE [dbo].[Property]
                                       SET [IdProperty] = case when @IdProperty is null or @IdProperty = '' then [IdProperty] else @IdProperty end
                                          ,[Name] = case when @Name is null or @Name = '' then [Name] else @Name end
                                          ,[Address] = case when @Address is null or @Address = '' then [Address] else @Address end
                                          ,[Price] = case when @Price = 0 then [Price] else @Price end
                                          ,[CodeInternal] = case when @CodeInternal is null or @CodeInternal = '' then [CodeInternal] else @CodeInternal end
                                          ,[Year] = case when @Year = 0 then [Year] else @Year end
                                          ,[IdOwner] = case when @IdOwner is null or @IdOwner = '' then [IdOwner] else @IdOwner end
                                     WHERE IdProperty = @IdProperty";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("IdProperty", property.PkProperty, DbType.String);
                parameters.Add("Name", property.PropertyName, DbType.String);
                parameters.Add("Address", property.PropertyAddress, DbType.String);
                parameters.Add("Price", property.PropertyPrice, DbType.Decimal);
                parameters.Add("CodeInternal", property.PropertyCodeInternal, DbType.String);
                parameters.Add("Year", property.PropertyYear, DbType.Int16);
                parameters.Add("IdOwner", property.FkOwner, DbType.String);

                await conectSql.ExecuteAsync(sqlQuery, parameters);
            }
            finally
            {
                conectSql.Close();
            }
        }

        public async Task<bool> ExistsOwnerName(string name)
        {
            using var conectSql = _context.CreateConnection();

            try
            {
                if (conectSql.State == ConnectionState.Closed)
                    conectSql.Open();

                string sqlQuery = @"SELECT * 
                                    FROM [dbo].[Owner]
                                    WHERE [Name] = @Name";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Name", name, DbType.String);

                var result = await conectSql.QueryAsync(sqlQuery, parameters);
                return result.Any();
            }
            finally
            {
                conectSql.Close();
            }
        }       

        public string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
