using System;

namespace NPortugol2.Core
{
    public class Symbol
    {
        public string Name { get; set; }

		public int Index { get; set; }

        public Type Type { get; set; }

        public dynamic Value { get; set; }

		public bool IsArg { get; set; }

		public int IntValue
		{
			get { return int.Parse(Value.ToString()); }
		}
		
		public float FloatValue
		{
			get { return float.Parse(Value.ToString()); }
		}
    }
}