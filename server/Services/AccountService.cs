namespace real_all_spice.Services;

public class AccountService(AccountsRepository repo)
{
  private readonly AccountsRepository _repo = repo;

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
  // NOTE I started writing out my own before I saw this
  // internal Account Edit(Account editData, string userEmail)
  // {
  //   Account original = GetProfileByEmail(userEmail);
  //   original.Name = editData.Name?.Length > 0 ? editData.Name : original.Name;
  //   original.Picture = editData.Picture?.Length > 0 ? editData.Picture : original.Picture;
  //   return _repo.Edit(original);
  // }
  internal Account EditAccount(Account user, string accountId, Account accountData)
  {
    if (user.Id != accountId)
    {
      throw new Exception("Either you are not logged in or this is not your account");
    }
    Account account = GetProfileByEmail(user.Email);
    account.Picture = accountData.Picture ?? account.Picture;
    account.Name = accountData.Name ?? account.Name;
    Account updatedAccount = _repo.EditAccount(account);
    return updatedAccount;
  }
}
