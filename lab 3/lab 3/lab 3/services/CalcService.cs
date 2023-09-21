namespace lab_3.services
{
    public interface ICalcService
    {
        string Sum(int a,int b);
        string Difference(int a, int b);
        string Product(int a, int b);
        string Quotient(int a, int b);
        string Pow(int a, int b);
    }
    public class CalcService : ICalcService
    {
        public string Sum( int a, int b)
        {
           int c= a + b;
            return c.ToString(); 
        }

        public string Difference(int a, int b) 
        {
            int c =a - b;
            return c.ToString();
        }

        public string Product(int a, int b) 
        {
            int c=a* b;
            return c.ToString(); 
        }
        
        public string Quotient(int a, int b)
        {
            double c= a / b;
            return c.ToString();
        }

        public string Pow (int a, int b)
        {
            double c=Math.Pow(a, b);
            return c.ToString();
        }


    }
}
