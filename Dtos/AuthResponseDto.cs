namespace GymManagmentSystem.Dtos
{
    public class AuthResponseDto
    {

       
        
            public string Token { get; set; } = null!;
            public DateTime ExpiresAt { get; set; }
            public string UserId { get; set; } = null!;
            public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
        
    }
}
