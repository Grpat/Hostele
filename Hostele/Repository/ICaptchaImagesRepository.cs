using Hostele.Models;

namespace Hostele.Repository;

public interface ICaptchaImagesRepository:IRepository<CaptchaImages>
{
    public void Add(string path, string answer);
    public Task<CaptchaImages?> GetRandom();
}