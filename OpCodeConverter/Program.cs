using Neo.VM;
using Neo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Reflection;
using Neo.SmartContract;

namespace OpCodeConverter
{
    static class Program
    {
        static void Main(string[] args)
        {
            var script = "DECKiNs7nm9rKamTRSQpjuRHmmKZlX0n1m89FfDzOvgcQIe7JfMnsg/4Ss1yHVwTxpmDjs1GWRcRyntZ06S81fIF";
            Analysis(script).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "DCEDrHZSlAddpveSfJa/49P2SuNoDF61D4L1UXCp8b6lna0LQQqQatQ=";
            Analysis(script).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "AgDh9QUMFHuv2LNVxgojhF87HIzMuK8GKakRDBTUzRIZzo4XK1AnOCPXmaNl+raw5BPADAh0cmFuc2ZlcgwU+fgUl8P5tiupP3PHEdQbHu/1DCNBYn1bUjk=";
            Analysis(script).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "AwDyBSoBAAAADBTUzRIZzo4XK1AnOCPXmaNl+raw5AwU1M0SGc6OFytQJzgj15mjZfq2sOQTwAwIdHJhbnNmZXIMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I5";
            Analysis(script).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();

            script = "EMAMBG5hbWUMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I=";
            Analysis(script).ForEach(p => Console.WriteLine(p));
            Console.WriteLine();


            Console.ReadLine();
        }

        public static string ToAsciiString(this byte[] byteArray)
        {
            var output = Encoding.Default.GetString(byteArray);
            if (output.Any(p => p < '0' || p > 'z')) return byteArray.ToHexString();
            return output;
        }
        public static List<string> Analysis(string base64, bool raw = false)
        {
            return Analysis(Convert.FromBase64String(base64).ToList(), raw);
        }

        public static List<string> Analysis(List<byte> scripts, bool raw)
        {
            //初始化所有 OpCode
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
            //初始化所有 InteropService
            var dic = new Dictionary<uint, string>();
            InteropService.SupportedMethods().ToList().ForEach(p => dic.Add(p.Hash, p.Method));

            //解析 Scripts
            var result = new List<string>();
            while (scripts.Count > 0)
            {
                var op = (OpCode)scripts[0];
                var operandSizePrefix = OperandSizePrefixTable[scripts[0]];
                var operandSize = OperandSizeTable[scripts[0]];
                scripts.RemoveAt(0);

                if (operandSize > 0)
                {
                    var operand = scripts.Take(operandSize).ToArray();
                    if (op.ToString().StartsWith("PUSHINT"))
                    {
                        result.Add(raw ? $"{op.ToString()} {operand.ToHexString()}" : $"{op.ToString()} {new BigInteger(operand)}");
                    }
                    else if (op == OpCode.SYSCALL)
                    {
                        result.Add(raw ? $"{op.ToString()} {operand.ToHexString()}" : $"{op.ToString()} {dic[BitConverter.ToUInt32(operand)]}");
                    }
                    else
                    {
                        result.Add($"{op.ToString()} {operand.ToHexString()}");
                    }
                    scripts.RemoveRange(0, operandSize);
                }
                if (operandSizePrefix > 0)
                {
                    var number = (int)new BigInteger(scripts.Take(operandSizePrefix).ToArray());
                    scripts.RemoveRange(0, operandSizePrefix);
                    var operand = scripts.Take(number).ToArray();

                    result.Add(raw ? $"{op.ToString()} LENGTH:{number} {operand.ToHexString()}" : $"{op.ToString()} {(number == 20 ? new UInt160(operand).ToString() : operand.ToAsciiString())}");
                    scripts.RemoveRange(0, number);
                }
            }
            return result;
        }
    }
}
