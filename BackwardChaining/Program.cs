using System;

namespace BackwardChaining {
    public class Program
    {
        static void Main(string[] args) {
            string path = @"C:\Users\Vytautas\Desktop\AI\BackwardChaining\BackwardChaining\tests\";
            BackwardChaining backwardChaining = new BackwardChaining(path+"Test7.txt");
            if (backwardChaining.Start()) {
                string rulesPath = "";

                for (int i = 0; i < backwardChaining.GetUsedRules().Count; i++) {
                    rulesPath += "R" + backwardChaining.GetUsedRules()[i].GetNumber();
                    if (i != backwardChaining.GetUsedRules().Count - 1) {
                        rulesPath += " -> ";
                    }
                }

                Log.AddToLog("--------------------End--------------------");
                Log.AddToLog("Rules path : " + rulesPath);
                Log.AddToLog("Success");
                Log.WriteToFile();
            }
            else {
                string rulesPath = "";

                for (int i = 0; i < backwardChaining.GetUsedRules().Count; i++) {
                    rulesPath += "R" + backwardChaining.GetUsedRules()[i].GetNumber();
                    if (i != backwardChaining.GetUsedRules().Count - 1) {
                        rulesPath += " -> ";
                    }
                }

                Log.AddToLog("--------------------End--------------------");
                Log.AddToLog("Rules path : " + rulesPath);
                Log.AddToLog("Fail");
                Log.WriteToFile();
            }
        }
    }
}
