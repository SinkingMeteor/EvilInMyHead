namespace Sheldier.Common.Utilities
{
    public class Counter
    {
        public int Amount => _amount;
        private int _amount;

        public Counter(int amount)
        {
            _amount = amount;
        }

        public void Remove(int amount)
        {
            _amount -= amount;
        }

        public void Set(int amount)
        {
            _amount = amount;
        }

        public void Add(int amount)
        {
            _amount += amount;
        }
        
    }
}