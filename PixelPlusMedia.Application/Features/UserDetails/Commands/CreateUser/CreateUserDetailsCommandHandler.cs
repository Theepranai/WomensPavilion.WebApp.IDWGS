using PixelPlusMedia.Domain.Entities;
using MediatR;
using PixelPlusMedia.Application.Contracts.Persistence;
using AutoMapper;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using PixelPlusMedia.Application.Contracts.aws;
using PixelPlusMedia.Application.Models.Aws;

namespace PixelPlusMedia.Application.Features.UserDetails.Commands.CreateUser;
public class CreateUserDetailsCommandHandler : IRequestHandler<CreateUserDetailsCommand, CreateUserDetailsResponse>
{
    private readonly IUserDetailRepository _userRepo;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IAWSServiceRepository _awsRepo;

    public CreateUserDetailsCommandHandler(IMapper mapper,
                                           IUserDetailRepository userRepo,
                                           IConfiguration config, 
                                           IAWSServiceRepository awsRepo)
    {
        _mapper = mapper;
        _userRepo = userRepo;
        _config = config;
        _awsRepo = awsRepo;

    }

    public async Task<CreateUserDetailsResponse> Handle(CreateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserDetailsValidation();
        var validationResult = await validator.ValidateAsync(request);

        var response = new CreateUserDetailsResponse();

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();

            foreach (var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }

            return response;
        }

        // Uploading media to s3 bucket
        var configKey = new ConfigKey()
        {
            AwsBucketName = _config["AWS:BucketName"],
            AwsRegion = _config["AWS:RegionEndpoint"],
            AwsS3BaseUrl = _config["AWS:S3BaseUrl"],
            AccessKey = _config["AWS:AccessKey"],
            SecretKey = _config["AWS:SecretKey"],
            PartSize = Int32.Parse(_config["AWS:PartSize"])
        };

        // will retun url ? success : exception
        var url = await _awsRepo.UploadFileAsync(configKey, request.ImageFile);
        request.MediaUrl = url;

        if (!url.Contains(configKey.AwsS3BaseUrl))
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            response.ValidationErrors.Add("Invalid media type");

            return response;
        }

        var user = _mapper.Map<UserDetail>(request);
        user = await _userRepo.AddAsync(user);

        response.UserDetails = _mapper.Map<CreateUserDetailsDto>(user);


        return response;
    }
}
