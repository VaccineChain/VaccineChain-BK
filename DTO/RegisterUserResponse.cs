namespace vaccine_chain_bk.DTO
{
    public class RegisterUserResponse
    {
        public bool Success { get; set; }
        public string Secret { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }

}
