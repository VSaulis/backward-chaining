using System;

namespace ForwardChaining
{
    public class Program
    {
        static void Main(string[] args) {
            string path = @"C:\Users\Vytautas\source\repos\BackwardChaining\BackwardChaining\tests\";
            BackwardChaining backwardChaining = new BackwardChaining(path+"Test11.txt");
            if (backwardChaining.Start()) {
                Log.AddToLog("Success");
                Log.WriteToFile();
            }
            else {
                Log.AddToLog("Fail");
                Log.WriteToFile();
            }
        }
    }
}
