using System.Net;
using Hostele.Models;
using Hostele.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Hostele.Services;

public class GoogleCaptchaService
{
    private readonly IOptions<GoogleCaptchaConfig> _config;
    public GoogleCaptchaService(IOptions<GoogleCaptchaConfig>config)
    {
        _config = config;
    }
    public async Task<bool> VerifyToken(string token)
    {
        try
        {
            var url =
                $" https://www.google.com/recaptcha/api/siteverify?secret={_config.Value.SecretKey}&response={token}";
            using (var client = new HttpClient())
            {
                var httpResult = await client.GetAsync(url);
                if (httpResult.StatusCode != HttpStatusCode.OK) return false;
                var responseString = await httpResult.Content.ReadAsStringAsync();
                var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);
                return googleResult.success && googleResult.score >= 0.5;
            }
        }
        catch(Exception e)
        {
            return false;
        }
    }
}