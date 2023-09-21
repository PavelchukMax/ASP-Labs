namespace lab_3.services
{

    public interface ITimeService
    {
        string Daytime();
    }
    public class TimeService : ITimeService 
    {   
        public string Daytime()
        {
            int time = DateTime.Now.Hour;
            if (time < 7) { return ("зараз ніч"); }
            else if (time > 6 && time <= 12) { return ("зараз ранок"); }
            else if (time > 12 && time <= 18) { return ("зараз день"); }
            else { return ("зараз вечір"); }
        }
    }
}
