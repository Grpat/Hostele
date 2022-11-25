using Hostele.Data;
using Hostele.Models;

namespace Hostele.Repository;

public class CaptchaImagesRepository: Repository<CaptchaImages>, ICaptchaImagesRepository
{
    private readonly ApplicationDbContext _db;

    public CaptchaImagesRepository(ApplicationDbContext db): base(db)
    {
        _db = db;
    }

    public Task<CaptchaImages?> GetRandom()
    {
        Random rand = new Random();
        int skipper = rand.Next(0, _db.CaptchaImages.Count());
        return Task.FromResult(_db.CaptchaImages.Skip(skipper).FirstOrDefault());
    }

    public async void Add(string path, string answer)
    {
        _db.Add(new CaptchaImages
        {
            Path=path,
            Answer = answer
        });
        await _db.SaveChangesAsync();
    }
}