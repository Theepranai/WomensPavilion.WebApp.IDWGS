using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using PixelPlusMedia.Application.Contracts.aws;
using PixelPlusMedia.Application.Models.Aws;

namespace PixelPlusMedia.Persistence.Repositories;

public class AWSServiceRepository : IAWSServiceRepository
{
    public async Task<string> UploadFileAsync(ConfigKey config, IFormFile filePath)
    {
        var awsClient = new AmazonS3Client(config.AccessKey, config.SecretKey, RegionEndpoint.APSoutheast1);
        var fileTranferUtility = new TransferUtility(awsClient);

        try
        {
            var fileTransferRequest = new TransferUtilityUploadRequest
            {
                BucketName = config.AwsBucketName,
                InputStream = filePath.OpenReadStream(),
                StorageClass = S3StorageClass.StandardInfrequentAccess,
                PartSize=config.PartSize,
                Key = filePath.FileName,
                CannedACL = S3CannedACL.PublicRead
            };

            fileTranferUtility.UploadAsync(fileTransferRequest).GetAwaiter().GetResult();

            fileTranferUtility.Dispose();

            return config.AwsS3BaseUrl+ "/" + filePath.FileName;
        }
        catch(AmazonS3Exception ex)
        {
            return ex.Message;
        }
    }
}
