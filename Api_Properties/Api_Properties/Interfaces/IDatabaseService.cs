using Api_Properties.Models;

namespace Api_Properties.Interfaces
{
    public interface IDatabaseService
    {
        public Task<List<Property>> GetProperty(string name);
        public Task InsertProperty(Property property);
        public Task InsertImageProperty(PropertyImage propertyImage);
        public Task UpdatePriceProperty(UpdatePrice property);
        public Task<bool> ExistsProperty(string idProperty);
        public Task UpdateAllProperty(Property property);
        public Task<bool> ExistsOwnerName(string name);       
        public string GenerateJwtToken(string username);
    }
}
