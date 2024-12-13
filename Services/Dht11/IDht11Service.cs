
using vaccine_chain_bk.DTO.Dht11;

namespace vaccine_chain_bk.Services.Dht11
{
    public interface IDht11Service
    {
        Task<string> ProcessData(Dht11Dto dht11);

    }
}
