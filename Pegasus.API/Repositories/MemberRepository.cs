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

        public IEnumerable<Member> GetMembers(object paramaters)
        {
            using(IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Member>("MemberSelectAll", paramaters).ToList();
            }
        }

        public IEnumerable<Member> GetMember(int _memberID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Member>("MemberSelectOne", new object []{
                    "@MemberID",_memberID
                }).ToList();
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
                parameters.Add("@ImageID", _member.MemberName,DbType.Int32,ParameterDirection.Input,1);

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
                parameters.Add("@ImageID", _member.MemberName, DbType.Int32, ParameterDirection.Input, 1);
                parameters.Add("@MemberID", _member.MemberID, DbType.String, ParameterDirection.Input, 200);

                dbConnection.Open();

                return dbConnection.Execute("MemberUpdate", parameters, commandType: CommandType.StoredProcedure) > 0 ? true : false;
            }
        }

        public bool DeleteMember(int _memberID)
        {
            using (IDbConnection dbConnection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MemberID", _memberID, DbType.String, ParameterDirection.Input, 200);
                dbConnection.Open();

                return dbConnection.Execute("MemberDelete", parameters, commandType: CommandType.StoredProcedure) > 0 ? true : false;
            }
        }
    }
}
