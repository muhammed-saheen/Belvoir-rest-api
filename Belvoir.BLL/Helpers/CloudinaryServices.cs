﻿using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
//using Google.Protobuf.Reflection;
using System.Security.Principal;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Belvoir.Bll.Helpers
{
    public interface ICloudinaryService
    {
        public Task<string> UploadImageAsync(IFormFile file);
    }


    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var apiSecret = Environment.GetEnvironmentVariable("cloudinarySecret") ?? string.Empty;
            var apiKey = Environment.GetEnvironmentVariable("cloudinaryAPIKey") ?? string.Empty;
            var cloudName = Environment.GetEnvironmentVariable("cloudName") ?? string.Empty;
            

            //var cloudName = configuration["CloudinarySettings:CloudName"];
            //var apiKey = configuration["CloudinarySettings:ApiKey"];
            //var apiSecret = configuration["CloudinarySettings:ApiSecret"];

            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new Exception("Cloudinary configuration is missing or incomplete");
            }

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.ToString();
            }
        }
    }

}
