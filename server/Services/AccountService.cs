
namespace KeeprFinalCheckpoint.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }
  internal Account GetProfileById(string profileId)
  {
    Account profile = _repo.GetById(profileId);
    return profile;
  }

  internal Account EditAccount(Account editData, string userId)
  {
    Account profile = _repo.GetById(userId);
    profile.Name = editData.Name?.Length > 0 ? editData.Name : profile.Name;
    profile.Picture = editData.Picture?.Length > 0 ? editData.Picture : profile.Picture;
    profile.CoverImg = editData.CoverImg?.Length > 0 ? editData.CoverImg : profile.CoverImg;
    return _repo.Edit(profile);
  }
}
