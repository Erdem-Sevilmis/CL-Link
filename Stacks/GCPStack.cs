using System.Collections.Generic;
using Pulumi;
using Gcp = Pulumi.Gcp;

//using Pulumi.GoogleNative;
//using Apikeys = Pulumi.GoogleNative.APIKeys.V2;
//using Pulumi.GoogleNative.CloudResourceManager.V1;
using System;

public class GCPStack
{
    public GCPStack()
    {
        /*
        // Create a Google Cloud resource (Storage Bucket)
        var bucket = new GCPBucket("my-bucketGCP");

        // Export the DNS name of the bucket
        new Dictionary<string, object?>
        {
            ["bucketSelfLink"] = bucket.SelfLink
        };



        var speechToTextApiKey = new Apikeys.Key("speechToTextApiKey", new Apikeys.KeyArgs
        {
            Project = "cl-link", // Replace with your GCP project ID
            Location = "global", // API keys are global resources
            DisplayName = "Speech to Text API Key",
            
            Restrictions = new Apikeys.Inputs.V2RestrictionsArgs
            {
                ApiTargets = new Apikeys.Inputs.V2ApiTargetArgs
                {
                    Service = "speech.googleapis.com" // Service name for the Speech-to-Text API
                }
            }
        });
        

        var basic = new Gcp.Organizations.Project("basic", new()
        {
            ProjectId = "cl-link",
            Name = "cl-link"
        });
        */

        var primary = new Gcp.Projects.ApiKey("primary", new()
        {
            Name = "key",
            DisplayName = "speach-to-text-key",
            Project = "cl-link",
            Restrictions = new Gcp.Projects.Inputs.ApiKeyRestrictionsArgs
            {
                ApiTargets = new[]
                {
                new Gcp.Projects.Inputs.ApiKeyRestrictionsApiTargetArgs
                {
                    Service = "speech.googleapis.com",
                    Methods = new[]
                    {
                        "GET*",
                    },
                },
            },
                BrowserKeyRestrictions = new Gcp.Projects.Inputs.ApiKeyRestrictionsBrowserKeyRestrictionsArgs
                {
                    AllowedReferrers = new[]
                    {
                    ".*",
                },
                },
            },
        });
        this.ApiKeyId = primary.Id;
        // Print out the API key in the console as part of the stack's output
        primary.Id.Apply(v => $"API Key ID: {v}");
    }

    // Export the API Key ID for reference
    [Output] public Output<string> ApiKeyId { get; set; }
}
