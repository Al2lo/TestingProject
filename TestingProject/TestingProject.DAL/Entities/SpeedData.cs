using TestingProject.DAL.Errors;

namespace TestingProject.DAL.Entities
{
    public class SpeedData
    {
        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set 
            {
                if (value > DateTime.Now)
                    throw new Exception(ErrorConsts.DateOrTimeError);
                _dateTime = value;
            }
        }
        public string CarNumber { get; set; }
        public double Speed { get; set; }
    }
}
