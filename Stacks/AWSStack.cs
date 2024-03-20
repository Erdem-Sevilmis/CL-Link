using System.Collections.Generic;
using AWSBucket = Pulumi.AwsNative.S3.Bucket;

public class AWSStack
{
    public AWSStack()
    {
        // Create an AWS resource (S3 Bucket)
        var bucket = new AWSBucket("my-bucketAWS");

        // Export the name of the bucket
        new Dictionary<string, object?>
        {
            ["bucketName"] = bucket.Id
        };
    }
}
