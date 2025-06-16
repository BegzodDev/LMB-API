namespace LMB.Application.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public UserDto User { get; set; } = default!;

    }
}
