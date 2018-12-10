namespace GWC.Web.Dtos
{
    public class WeblogConfigurationDto
    {
        
        public string ContentfulDeliveryApiKey { get; set; }
        public string ConnectionString { get; set; }
        public string DevelopmentMode { get; set; }
        public string LogFolder { get; set; }
        public string LogLevel { get; set; }
        public string RecaptchaSecret { get; set; }
        public string Secret { get; set; }
        public string SendGridApiKey { get; set; }
        public AzureAd AzureAd { get; set; }
    }

    public class AzureAd
    {
        public string AudienceId { get; set; }
        public string ClientId { get; set; }
        public string Tenant { get; set; }
        public string AadInstance { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }

   


}
