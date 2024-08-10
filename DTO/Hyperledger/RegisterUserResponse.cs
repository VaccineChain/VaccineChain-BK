namespace vaccine_chain_bk.DTO.HyperledgerResponse
{
    public class RegisterUserResponse
    {
        public bool success { get; set; }
        public string secret { get; set; }
        public string message { get; set; }
        public string token { get; set; }
    }

}
