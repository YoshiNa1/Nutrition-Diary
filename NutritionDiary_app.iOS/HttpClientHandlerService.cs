using Xamarin.Forms;
using System.Net.Http;
using NutritionDiary_app.iOS;

[assembly: Dependency(typeof(HttpClientHandlerService))]
namespace NutritionDiary_app.iOS
{
    public class HttpClientHandlerService :IHttpClientHandlerService
    {
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }

    public interface IHttpClientHandlerService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
