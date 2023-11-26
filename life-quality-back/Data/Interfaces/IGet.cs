namespace life_quality_back.Data.Interfaces
{
    public interface IGet<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
    }
}
