namespace HP32SII.Logic
{
    public interface IDatabase
    {
        void Store(string key, double value);
        double Recall(string key);
        void ClearAll();
    }
}
