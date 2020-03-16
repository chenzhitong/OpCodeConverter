using Neo.VM;
using Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Reflection;

namespace OpCodeConverter
{
    static class Program
    {
        static void Main(string[] args)
        {
            var script = "EQwU1M0SGc6OFytQJzgj15mjZfq2sOQMFHuv2LNVxgojhF87HIzMuK8GKakRE8AMCHRyYW5zZmVyDBSJdyDYzXb08Aq/o3wO3YicII/em0FifVtSOQ==";
            Process(Convert.FromBase64String(script)).ForEach(p => Console.WriteLine(p));
            Console.ReadLine();
        }

        public static List<string> Process(IEnumerable<byte> script)
        {
            var OperandSizePrefixTable = new int[256];
            var OperandSizeTable = new int[256];

            foreach (FieldInfo field in typeof(OpCode).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute<OperandSizeAttribute>();
                if (attribute == null) continue;
                int index = (int)(OpCode)field.GetValue(null);
                OperandSizePrefixTable[index] = attribute.SizePrefix;
                OperandSizeTable[index] = attribute.Size;
            }
            var input = script.ToList();
            var result = new List<string>();

            while (input.Count > 0)
            {
                var op = (OpCode)input[0];
                var operandSizePrefix = OperandSizePrefixTable[input[0]];
                var operandSize = OperandSizeTable[input[0]];
                input.RemoveAt(0);

                if (operandSize > 0)
                {
                    result.Add($"{op.ToString()} {input.Take(operandSize).ToArray().ToHexString()}");
                    input.RemoveRange(0, operandSize);
                }
                if (operandSizePrefix > 0)
                {
                    var number = (int)new BigInteger(input.Take(operandSizePrefix).ToArray());
                    input.RemoveRange(0, operandSizePrefix);

                    result.Add($"{op.ToString()} LENGTH:{number} {input.Take(number).ToArray().ToHexString()}");
                    input.RemoveRange(0, number);
                }
            }
            return result;
        }
    }
}
