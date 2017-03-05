using Model;

namespace DatabaseService.Extension
{
    public static class Extension
    {
        public static User ToUserModel(this Users user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Login = user.Login,
                Password = user.Password
            };
        }
    }
}