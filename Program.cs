using System;
using System.IO;
using Amazon;
using Amazon.S3;
using Amazon.Runtime;
using Amazon.S3.Transfer;
using Amazon.S3.Model;

namespace PumoS3UploadTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AmazonS3Config S3Config = new AmazonS3Config { ServiceURL = "https://s3.pumo.com.tw" };

            string bucketName = "touchin";
            string s3ImageRootUrl = string.Format("https://{0}.s3.pumo.com.tw", bucketName);
            string filePathName =  @"./exciting.jpg";
            string keyName = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), Path.GetFileName(filePathName));

            string accessKeyId = "0BQ2H59GHLKOD7XH26YF";
            string secretAccessKey = "YqXwvhiC7pOitkGqI9DBcMuoaNCObF3pyYfMngg9";

            string s3ImageUrl = "";

            BasicAWSCredentials credentials = new BasicAWSCredentials(accessKeyId, secretAccessKey);

            AmazonS3Client s3Client = new AmazonS3Client(credentials, S3Config);

            try
            {
                PutObjectRequest ObjectRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    FilePath = filePathName,
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };

                PutObjectResponse myResponse = s3Client.PutObjectAsync(ObjectRequest).Result;

                s3ImageUrl = string.Format("{0}/{1}", s3ImageRootUrl, keyName);

                Console.WriteLine("Image Url: {0}", s3ImageUrl);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("AmazonS3Exception: {0}", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }
    }
}
