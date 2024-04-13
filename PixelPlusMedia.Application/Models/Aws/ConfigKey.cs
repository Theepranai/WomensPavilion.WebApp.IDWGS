namespace PixelPlusMedia.Application.Models.Aws;

public class ConfigKey
{
    public string AwsBucketName { get; set; }
    public string AwsS3BaseUrl { get; set; }
    public string AwsRegion { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public int PartSize { get; set; }
}
