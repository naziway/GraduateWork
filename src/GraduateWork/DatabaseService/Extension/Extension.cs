
namespace DatabaseService.Extension
{
    public static class Extension
    {
        public static Model.User ToUserModel(this User user)
        {
            return new Model.User
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