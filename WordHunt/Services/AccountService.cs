using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using WordHunt.Data;

namespace WordHunt.Services
{
    public class AccountService
    {
        private readonly IConfiguration configuration;

        public AccountService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<bool> AccountControlAsync(UserInfo user, HttpContext httpContext)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString("WordConnection"));

                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AccountControl";
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.Parameters.Add(new SqlParameter("@USERNAME", SqlDbType.NVarChar, 100) { Value = user.UserName });
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", SqlDbType.NVarChar, 100) { Value = user.Password });

                SqlDataReader authRdr = cmd.ExecuteReader();
                if (authRdr.HasRows)
                {
                    authRdr.Read();
                    var namesurname = authRdr["NAME"].ToString() + " " + authRdr["SURNAME"].ToString();

                    if (namesurname == "")
                    {
                        return false;
                    }
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, namesurname),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties();

                    await httpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
