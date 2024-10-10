using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ToyStoreAPI.Data
{
    public class IdentityColumnGenerator : ValueGenerator<int>
    {
        private int _currentValue;

        public IdentityColumnGenerator(int startsfrom)
        {
            _currentValue = startsfrom;
        }

        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
        {
            return _currentValue++;
        }
    }
}