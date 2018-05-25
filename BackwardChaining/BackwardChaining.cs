using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ForwardChaining {
    public class BackwardChaining {
        private readonly List<Rule> _rules;
        private readonly List<char> _facts;
        private readonly List<char> _goals;
        private int _step;

        public BackwardChaining(string filename) {
            _step = 0;
            _rules = new List<Rule>();
            _facts = new List<char>();
            _goals = new List<char>();
            ReadFile(filename);
        }

        public bool Start() {
            return Iterate(_goals, _rules, 1);
        }

        public bool Iterate(List<char> goals, List<Rule> rules, int depth) {

            if (!goals.Except(_facts).Any()) {
                _step++;
                Log.AddToLog(_step + ". Depth : " + depth + ". All goals found in facts list.");
                return true;
            }

            foreach (Rule rule in rules) {
                if (!rule.GetConsequent().Except(goals).Any()) {

                    var tempGoals = goals.Except(rule.GetConsequent()).ToList();

                    foreach (var letter in rule.GetAntecedent()) {
                        if (!tempGoals.Contains(letter)) {
                            tempGoals.Add(letter);
                        }
                    }

                    var tempRules = rules.Except(new List<Rule>{rule}).ToList();
                    _step++;
                    Log.AddToLog(_step + ". Depth : " + depth + ". Rule consiquent found in goals. New goals : " + new String(tempGoals.ToArray()));

                    if (Iterate(tempGoals, tempRules, depth++)) {
                        return true;
                    }
                }
                else {
                    _step++;
                    Log.AddToLog(_step + ". Depth : " + depth + ". Rule consiquent not found in goals.");
                }
            }
            return false;
        }


        private void ReadFile(string filename) {
            string status = "";

            List<string> lines = File.ReadAllLines(filename).ToList();
            foreach (var line in lines) {
                if (line.Contains("Rules")) {
                    status = "Rules";
                }
                else if (line.Contains("Facts")) {
                    status = "Facts";
                }
                else if (line.Contains("Goal")) {
                    status = "Goal";
                }
                else {
                    if (status == "") {
                        Console.WriteLine("Wrong file");
                        return;
                    }

                    if (status == "Rules") {
                        if (!String.IsNullOrEmpty(line)) {
                            Rule rule = new Rule(line);
                            _rules.Add(rule);
                        }
                    }
                    else if (status == "Facts") {
                        foreach (char c in line)
                            if (Char.IsLetter(c)) {
                                char letter = Char.ToUpper(c);
                                _facts.Add(letter);
                            }
                    }
                    else if (status == "Goal") {
                        foreach (char c in line)
                            if (Char.IsLetter(c)) {
                                char letter = Char.ToUpper(c);
                                _goals.Add(letter);
                            }
                    }
                    else {
                        Console.WriteLine("Wrong file");
                        return;
                    }
                }
            }
        }
    }
}