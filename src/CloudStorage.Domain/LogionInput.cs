using System.ComponentModel.DataAnnotations;

namespace CloudStorage.Domain
{
    public class LogionInput
    {
        [Required(ErrorMessage = "请输入账号！")]
        public string? Account { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [MinLength(5, ErrorMessage = "密码最短五位！")]
        public string? Password { get; set; }
    }
}