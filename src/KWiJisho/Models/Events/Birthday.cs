using System.Threading.Tasks;

namespace KWiJisho.Models.Events
{
    internal class Birthday
    {

        public TaskScheduler BirthdayTaskScheduler { get; set; }

        public static void GiveBirthdayMessage()
        {
            //BirthdayTaskScheduler.
        }
    }
}
