using System.ComponentModel.DataAnnotations;

namespace DemoAPI.DAL
{
    public class RefreshTokenModel
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RefreshToken { get; set; }

    }

}
