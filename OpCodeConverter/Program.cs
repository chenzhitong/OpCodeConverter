using Neo.VM;
using Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Reflection;
using Neo.Wallets;

namespace OpCodeConverter
{
    static class Program
    {
        static void Main(string[] args)
        {
            var script = "DECKiNs7nm9rKamTRSQpjuRHmmKZlX0n1m89FfDzOvgcQIe7JfMnsg/4Ss1yHVwTxpmDjs1GWRcRyntZ06S81fIF";
            Process(Convert.FromBase64String(script)).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "DCEDrHZSlAddpveSfJa/49P2SuNoDF61D4L1UXCp8b6lna0LQQqQatQ=";
            Process(Convert.FromBase64String(script)).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "AgDh9QUMFHuv2LNVxgojhF87HIzMuK8GKakRDBTUzRIZzo4XK1AnOCPXmaNl+raw5BPADAh0cmFuc2ZlcgwU+fgUl8P5tiupP3PHEdQbHu/1DCNBYn1bUjk=";
            Process(Convert.FromBase64String(script)).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "AwDyBSoBAAAADBTUzRIZzo4XK1AnOCPXmaNl+raw5AwU1M0SGc6OFytQJzgj15mjZfq2sOQTwAwIdHJhbnNmZXIMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I5";
            Process(Convert.FromBase64String(script)).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "EMAMBG5hbWUMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I=";
            Process(Convert.FromBase64String(script)).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();


            Console.ReadLine();
        }

        public static string ToASCIIString(this byte[] byteArray)
        {
            var output = Encoding.Default.GetString(byteArray);
            if (output.Any(p => p < '0' || p > 'z')) return byteArray.ToHexString();
            return output;
        }

        public static List<string> Process(IEnumerable<byte> script, bool raw = true)
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
                    var operand = input.Take(operandSize).ToArray();
                    if (raw)
                    {
                        result.Add($"{op.ToString()} {operand.ToHexString()}");
                    }
                    else
                    {
                        if (op.ToString().StartsWith("PUSHINT"))
                        {
                            result.Add($"{op.ToString()} {new BigInteger(operand)}");
                        }
                        else
                        {
                            result.Add($"{op.ToString()} {operand.ToHexString()}");
                        }
                    }
                    input.RemoveRange(0, operandSize);
                }
                if (operandSizePrefix > 0)
                {
                    var number = (int)new BigInteger(input.Take(operandSizePrefix).ToArray());
                    input.RemoveRange(0, operandSizePrefix);
                    var operand = input.Take(number).ToArray();

                    if (raw)
                    {
                        result.Add($"{op.ToString()} LENGTH:{number} {operand.ToHexString()}");
                    }
                    else
                    {
                        result.Add($"{op.ToString()} {(number == 20 ? new UInt160(operand).ToString() : operand.ToASCIIString())}");
                    }
                    input.RemoveRange(0, number);
                }
            }
            return result;
        }
    }
}
