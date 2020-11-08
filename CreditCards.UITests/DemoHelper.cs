using System.Threading;

namespace CreditCards.UITests
{
    internal static class DemoHelper
    {
        public static void Pause(int setToPause = 3000) 
        {
            Thread.Sleep(setToPause);
        }
    }
}
