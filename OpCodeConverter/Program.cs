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
    public static class Program
    {
        static void Main(string[] args)
        {
            //交易的 script 字段，或者 witness.invocation、witnesses.verification 字段，Base64 编码
            var script = "AgDh9QUMFHuv2LNVxgojhF87HIzMuK8GKakRDBTUzRIZzo4XK1AnOCPXmaNl+raw5BPADAh0cmFuc2ZlcgwU+fgUl8P5tiupP3PHEdQbHu/1DCNBYn1bUjk=";
            ScriptsToOpCode(script).ForEach(p => Console.WriteLine(p));
            Console.ReadLine();
        }

        public static List<string> ScriptsToOpCode(string base64)
        {
            List<byte> scripts;
            try
            {
                scripts = Convert.FromBase64String(base64).ToList();
            }
            catch (Exception)
            {
                throw new FormatException();
            }

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
            ApplicationEngine.Services.ToList().ForEach(p => dic.Add(p.Hash, p.Name));

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
                        result.Add($"{op} {new BigInteger(operand)}");
                    }
                    else if (op == OpCode.SYSCALL)
                    {
                        result.Add($"{op} {dic[BitConverter.ToUInt32(operand)]}");
                    }
                    else
                    {
                        result.Add($"{op} {operand.ToHexString()}");
                    }
                    scripts.RemoveRange(0, operandSize);
                }
                if (operandSizePrefix > 0)
                {
                    var bytes = scripts.Take(operandSizePrefix).ToArray();
                    var number = bytes.Length == 1 ? bytes[0] : (int)new BigInteger(bytes);
                    scripts.RemoveRange(0, operandSizePrefix);
                    var operand = scripts.Take(number).ToArray();

                    var asicii = Encoding.Default.GetString(operand);
                    asicii = asicii.Any(p => p < '0' || p > 'z') ? operand.ToHexString() : asicii;

                    result.Add($"{op} {(number == 20 ? new UInt160(operand).ToString() : asicii)}");
                    scripts.RemoveRange(0, number);
                }
            }
            return result;
        }
    }
}
