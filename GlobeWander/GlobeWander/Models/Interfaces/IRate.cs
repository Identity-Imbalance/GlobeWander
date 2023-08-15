namespace GlobeWander.Models.Interfaces
{
    public interface IRate
    {
        Task<Rate> Create (Rate rate);
        Task <Rate> GetRateById(int id);
        Task <List<Rate>> GetAllRate();
        Task<Rate> UpdateRate(int id,Rate rate);
        Task<Rate> DeleteRate(int id);
      

    }
}
