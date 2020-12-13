using System;
namespace HandheldHalting
{
    public class Instruction
    {
        public string Text { get; set; }
        public string Prefix { get; set; }
        public int Value { get; set; }
        public bool Visited { get; set; }
    }
}
