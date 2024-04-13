using Amazon.S3;
using MediatR;
using Microsoft.Extensions.Configuration;
using PixelPlusMedia.Application.Contracts.aws;
using PixelPlusMedia.Application.Models.Aws;

namespace PixelPlusMedia.Application.Features.UploadImage;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadImageCommandResponse>
{

    private readonly IAWSServiceRepository _awsRepo;
    private readonly IConfiguration _config;
 
    public UploadImageCommandHandler(IConfiguration config, IAWSServiceRepository awsRepo)
    {
        _config = config;
        _awsRepo = awsRepo;
    }
    public async Task<UploadImageCommandResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {

        var response = new UploadImageCommandResponse();
        var validator = new UploadImageCommandValidator();
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            
            
            foreach(var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        var configKey = new ConfigKey()
        {
            AwsBucketName = _config["AWS:BucketName"],
            AwsRegion = _config["AWS:RegionEndpoint"],
            AwsS3BaseUrl = _config["AWS:S3BaseUrl"],
            AccessKey = _config["AWS:AccessKey"],
            SecretKey = _config["AWS:SecretKey"],
            PartSize = Int32.Parse(_config["AWS:PartSize"])
        };

        var url = await _awsRepo.UploadFileAsync(configKey,  request.ImagePath);
        response.Url = url;

        return response;
    }
}
