using System;
using System.IO;
using System.Threading.Tasks;
using Google;
using File;
using AutoMapper;
using Models.Ad.Dtos;
using Models.Ad.Entities;
using Newtonsoft.Json;

namespace Services
{
    public class AdService : IAdService
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IFileRead _fileReadService;
        private readonly IGoogleStorage _googleStorage;
        private readonly IMapper _mapper;

        public AdService(IMapper mapper, IFileRead fileReadService, IGoogleStorage googleStorage)
        {
            _mapper = mapper;
            _fileReadService = fileReadService;
            _googleStorage = googleStorage;

            //Ad ad = new Ad() { AdId = 1, ActiveDays = 1, AdAssetId = Guid.Empty, CreatedDateTime = DateTime.Now, Email = "chinna.tatikella@khs-net.com", PhoneNumber = "515-707-7725" };
            //Advm advm = _mapper.Map<Advm>(ad);
            //advm.AdAssetId = Guid.NewGuid(); advm.PhoneNumber = "444444";
            //Ad dd = _mapper.Map<Ad>(advm);
        }

        public async Task UploadObjectInGoogleStorageAsync(string bucketName, Stream stream, string objectName, string contentType)
        {
            await _googleStorage.UploadObjectAsync(bucketName, stream, objectName, contentType);
        }

        public void UploadObjectInGoogleStorage(string fileName, int inMemoryCachyExpireDays, string objectName, string bucketName, object anonymousDataObject, string contentType, string CACHE_KEY)
        {
            string content = _fileReadService.FileAsString(fileName, inMemoryCachyExpireDays, CACHE_KEY);
            content = _fileReadService.FillContent(content, anonymousDataObject);
            Stream stream = _fileReadService.FileAsStream(content);
            _googleStorage.UploadObject(bucketName, stream, objectName, contentType);
        }
    }

    public interface IAdService
    {
        void UploadObjectInGoogleStorage(string fileName, int inMemoryCachyExpireDays, string objectName, string bucketName, object anonymousDataObject, string contentType, string CACHE_KEY);
        Task UploadObjectInGoogleStorageAsync(string bucketName, Stream stream, string objectName, string contentType);
    }
}
