using System;
using System.Diagnostics;

namespace CrmSdkInegration
{
    internal class Program
    {
        static void Main()
        {
            CreateRandomAccounts createRandomAccounts = new CreateRandomAccounts();
            Stopwatch stopwatch = Stopwatch.StartNew();
            createRandomAccounts.CreateAccountsOnSingleThread(10000);
            //createRandomAccounts.CreateAccountsInBatchesWithOneTransaction(10000,1000);
            //createRandomAccounts.CreateAccountsInBatches(10000, 1000);
            //createRandomAccounts.CreateAccountsInParallel(10000);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());
        }
    }
}
