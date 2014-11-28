using System.Collections.Generic;

namespace Infrastructure.CrossCutting.NetFramework.Structures
{
    public struct InputParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public Dictionary<string, string> Inputs { get; set; }
    }
}