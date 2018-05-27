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
        private readonly List<Rule> _usedRules;
        private int _step;

        public BackwardChaining(string filename) {
            _step = 0;
            _rules = new List<Rule>();
            _facts = new List<char>();
            _goals = new List<char>();
            _usedRules = new List<Rule>();
            ReadFile(filename);
        }

        public bool Start() {
            return Iterate(_goals, _rules, 1);
        }

        public List<Rule> GetUsedRules() {
            return _usedRules;
        }

        public bool Iterate(List<char> goals, List<Rule> rules, int depth) {

            if (!goals.Except(_facts).Any()) {
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
                    Log.AddToLog(_step + ". Depth : " + depth + ". Rule used : R" + rule.GetNumber() + ". Result : New goals : " + new String(tempGoals.ToArray()));
                    _usedRules.Add(rule);

                    if (Iterate(tempGoals, tempRules, depth++)) {
                        return true;
                    }
                }
                else {
                    _step++;
                    Log.AddToLog(_step + ". Depth : " + depth + ". Rule used : R"+ rule.GetNumber() +". Result : Rule consiquent not found in goals.");
                }
            }
            return false;
        }


        private void ReadFile(string filename) {
            string status = "";
            int ruleNumber = 0;

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
                            ruleNumber++;
                            Rule rule = new Rule(line, ruleNumber);
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