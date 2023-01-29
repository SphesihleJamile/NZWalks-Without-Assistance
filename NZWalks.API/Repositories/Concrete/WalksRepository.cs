using NZWalks.API.Data;
using NZWalks.API.Repositories.Abstract;

namespace NZWalks.API.Repositories.Concrete
{
    public class WalksRepository : IWalksRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalksRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

    }
}
