using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Supabase;
using Supabase.Gotrue;
using Supabase.Postgrest;


namespace PRN222.Assignment.Services.Implementations
{
    public class SupabaseService
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseService(IConfiguration configuration)
        {
            var url = configuration["Supabase:Url"];
            var key = configuration["Supabase:Key"];

            var options = new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(url, key, options);
        }

        public Supabase.Client GetClient()
        {
            return _supabaseClient;
        }

        public Supabase.Postgrest.Client GetDatabase<T>() where T : Supabase.Postgrest.Models.BaseModel, new()
        {
            return (Supabase.Postgrest.Client)_supabaseClient.From<T>();
        }

        //public Supabase.Postgrest.Client<T> GetTable<T>(string tableName) where T : Supabase.Postgrest.Models.BaseModel, new()
        //{
        //    return _supabaseClient.Postgrest.Table<T>(tableName);
        //}

        public Supabase.Gotrue.Client GetAuth()
        {
            return (Supabase.Gotrue.Client)_supabaseClient.Auth;
        }

        public async Task<User> SignUpAsync(string email, string password)
        {
            var response = await _supabaseClient.Auth.SignUp(email, password);
            return response.User;
        }

        public async Task<Session> SignInAsync(string email, string password)
        {
            var response = await _supabaseClient.Auth.SignIn(email, password);
            return response;
        }

        public async Task SignOutAsync()
        {
            await _supabaseClient.Auth.SignOut();
        }
    }
}
