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
    public class SupabaseStorageService
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly string _bucketName = "milktea-images";

        public SupabaseStorageService(IConfiguration configuration)
        {
            var url = configuration["Supabase:Url"];
            var key = configuration["Supabase:Key"];
            _supabaseClient = new Supabase.Client(url, key);

            InitializeBucketAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeBucketAsync()
        {
            var buckets = await _supabaseClient.Storage.ListBuckets();
            if (!buckets.Any(b => b.Name == _bucketName))
            {
                await _supabaseClient.Storage.CreateBucket(_bucketName);
            }
        }

        //public async Task<string> UploadProductImageAsync(Stream fileStream, string fileName, string contentType)
        //{
        //    var uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}_{fileName}";

        //    await _supabaseClient.Storage
        //        .From(_bucketName)
        //        .Upload(fileStream, uniqueFileName, new Supabase.Storage.UploadOptions
        //        {
        //            ContentType = contentType,
        //            Upsert = true 
        //        });

        //    return GetImageUrl(uniqueFileName);
        //}

        public string GetImageUrl(string filePath)
        {
            return _supabaseClient.Storage
                .From(_bucketName)
                .GetPublicUrl(filePath);
        }

        public async Task DeleteImageAsync(string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            await _supabaseClient.Storage
                .From(_bucketName)
                .Remove(new List<string> { fileName });
        }
    }
}
