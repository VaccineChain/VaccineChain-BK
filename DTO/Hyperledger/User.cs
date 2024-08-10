namespace vaccine_chain_bk.DTO.HyperledgerResponse
{
    public class User
    {

        public User(string username, string org)
        {
            this.username = username;
            orgName = org;
        }

        public string username { get; set; }
        public string orgName { get; set; }
    }
}
