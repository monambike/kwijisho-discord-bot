using System.Threading.Tasks;

namespace KWIJisho.Models.Events
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
