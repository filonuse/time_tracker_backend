namespace Core.Models.Models.Bind_Models
{
    public class UserGroupModel : BaseEntity
    {
        public UserGroupModel() { }

        public string UserId { get; set; }
        public virtual UserModel User { get; set; }

        public string GroupId { get; set; }
        public virtual GroupModel Group { get; set; }

        public bool IsManager { get; set; }
    }
}
