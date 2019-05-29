using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Pegasus.API.Interfaces;
using Pegasus.Model;

namespace Pegasus.API.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private string _connectionstring;
        public IConfiguration _configuration { get; }

        public MemberRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionstring = _configuration.GetConnectionString("DefaultConnectionString");
        }

        public IDbConnection Connection
        {
            get
            {
                return  new  SqlConnection(_connectionstring);
            }
        }

        public IEnumerable<Member> GetMembers(Member member)
        {
            using(IDbConnection dbConnection = Connection)
            {

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MemberID", member.MemberID, DbType.Int32, ParameterDirection.Input,2);
                parameters.Add("@MemberName", member.MemberName, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@Email", member.Email, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@PhoneNumber", member.PhoneNumber, DbType.String, ParameterDirection.Input, 10);

                dbConnection.Open();
                var result = dbConnection.Query<Member>("MemberSelectAll", parameters,commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
        }

        public Member GetMember(int _memberID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MemberID", _memberID, DbType.Int32, ParameterDirection.Input, 2);
                dbConnection.Open();
                return dbConnection.Query<Member>("MemberSelectOne", parameters , commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
            }
        }

        public int AddMember(Member _member)
        {
            using (IDbConnection dbConnection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MemberName", _member.MemberName,DbType.String,ParameterDirection.Input,200);
                parameters.Add("@Email", _member.Email,DbType.String,ParameterDirection.Input,200);
                parameters.Add("@PhoneNumber", _member.PhoneNumber,DbType.String,ParameterDirection.Input,10);
                parameters.Add("@ImageID", _member.ImageID,DbType.Int32,ParameterDirection.Input,1);

                dbConnection.Open();

                return dbConnection.Execute("MemberAdd", parameters, commandType: CommandType.StoredProcedure);
            }

        }

        public bool UpdateMember(Member _member)
        {
            using (IDbConnection dbConnection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MemberName", _member.MemberName, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@Email", _member.Email, DbType.String, ParameterDirection.Input, 200);
                parameters.Add("@PhoneNumber", _member.PhoneNumber, DbType.String, ParameterDirection.Input, 10);
                parameters.Add("@ImageID", _member.ImageID, DbType.Int32, ParameterDirection.Input, 1);
                parameters.Add("@MemberID", _member.MemberID, DbType.Int32, ParameterDirection.Input, 2);

                dbConnection.Open();

                return dbConnection.Execute("MemberUpdate", parameters, commandType: CommandType.StoredProcedure) > 0 ? true : false;
            }
        }

        public bool DeleteMember(int _memberID)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@MemberID", _memberID, DbType.Int32, ParameterDirection.Input, 200);
                    dbConnection.Open();

                    return dbConnection.Execute("MemberDelete", parameters, commandType: CommandType.StoredProcedure) > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
