namespace vaccine_chain_bk.Models
{
    public class SensorReading
    {
        public string fcn { get; set; }
        public List<string> peers { get; set; }
        public string chaincodeName { get; set; }
        public string channelName { get; set; }
        public List<string> args { get; set; }
    }

}
