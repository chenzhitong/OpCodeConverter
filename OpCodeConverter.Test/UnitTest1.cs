using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo;
using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace OpCodeConverter.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //常见的 invocation
            var script = "DEA5gbDAJXc7I3erY9MDBxQF0eX36LrvukMNg5EHVpqbJvG1EI+YRno6p8YNTLJEpYBl/NgWnkIOueOP85mRV1sj";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "PUSHDATA1 3981b0c025773b2377ab63d303071405d1e5f7e8baefba430d839107569a9b26f1b5108f98467a3aa7c60d4cb244a58065fcd8169e420eb9e38ff39991575b23\r\n");
        }

        [TestMethod]
        public void TestMethod2()
        {
            //常见的 verification
            var script = "EQwhA9q4TBJD7AGrJQDhqMehVGom1zRigYCwz2TnK/d2U2mXEQtBE43vrw==";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL Neo.Crypto.CheckMultisigWithECDsaSecp256r1\r\nPUSHDATA1 03dab84c1243ec01ab2500e1a8c7a1546a26d734628180b0cf64e72bf776536997\r\n");
        }

        [TestMethod]
        public void TestMethod3()
        {
            //NEO 转账的 Scripts
            var script = "EQwUvQMah0TTRtYWcYFgFy9jaOAX3AUMFL0DGodE00bWFnGBYBcvY2jgF9wFE8AMCHRyYW5zZmVyDBQlBZ7LSHjTqHX5HFHO3tMw1Fdf3kFifVtSOA==";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL System.Contract.Call\r\nPUSHDATA1 0xde5f57d430d3dece511cf975a8d37848cb9e0525\r\nPUSHDATA1 transfer\r\nPUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd\r\nPUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd\r\n");
        }

        [TestMethod]
        public void TestMethod4()
        {
            //NEO 转账的 Scripts
            var script = "AGQMFL0DGodE00bWFnGBYBcvY2jgF9wFDBS9AxqHRNNG1hZxgWAXL2No4BfcBRPADAh0cmFuc2ZlcgwUJQWey0h406h1+RxRzt7TMNRXX95BYn1bUjg=";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL System.Contract.Call\r\nPUSHDATA1 0xde5f57d430d3dece511cf975a8d37848cb9e0525\r\nPUSHDATA1 transfer\r\nPUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd\r\nPUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd\r\nPUSHINT8 100\r\n");

        }

        [TestMethod]
        public void TestMethod5()
        {
            //GAS 转账的 Scripts
            var script = "AwDkC1QCAAAADBS9AxqHRNNG1hZxgWAXL2No4BfcBQwUgUkpDXgVmkkrRX4lahSzu86gNiATwAwIdHJhbnNmZXIMFLyvQdaEx9StbuDZnalwe50fDI5mQWJ9W1I4";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL System.Contract.Call\r\nPUSHDATA1 0x668e0c1f9d7b70a99dd9e06eadd4c784d641afbc\r\nPUSHDATA1 transfer\r\nPUSHDATA1 0x2036a0cebbb3146a257e452b499a15780d294981\r\nPUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd\r\nPUSHINT64 10000000000\r\n");

        }

        [TestMethod]
        public void TestMethod6()
        {
            //NEP-5 转账的 Scripts
            var script = "AwDyBSoBAAAADBTUzRIZzo4XK1AnOCPXmaNl+raw5AwU1M0SGc6OFytQJzgj15mjZfq2sOQTwAwIdHJhbnNmZXIMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I5";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL System.Contract.Call\r\nPUSHDATA1 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9\r\nPUSHDATA1 transfer\r\nPUSHDATA1 0xe4b0b6fa65a399d7233827502b178ece1912cdd4\r\nPUSHDATA1 0xe4b0b6fa65a399d7233827502b178ece1912cdd4\r\nPUSHINT64 5000000000\r\n");
        }

        [TestMethod]
        public void TestMethod7()
        {
            //调用 NEP-5 "name" 方法的 Scripts
            var script = "EMAMBG5hbWUMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I=";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL System.Contract.Call\r\nPUSHDATA1 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9\r\nPUSHDATA1 name\r\n");
        }

        [TestMethod]
        public void TestMethod8()
        {
            //部署简单合约的 Scripts
            var script = "DW0BeyJncm91cHMiOltdLCJmZWF0dXJlcyI6eyJzdG9yYWdlIjp0cnVlLCJwYXlhYmxlIjpmYWxzZX0sImFiaSI6eyJoYXNoIjoiMHgxMzhhN2M0NTNlZTRhNDk1NzJhNDUzOTAxNDE1ZGNhNThmYTQ5MGRjIiwiZW50cnlQb2ludCI6eyJuYW1lIjoibWFpbiIsInBhcmFtZXRlcnMiOlt7Im5hbWUiOiJtZXRob2QiLCJ0eXBlIjoiU3RyaW5nIn0seyJuYW1lIjoiYXJncyIsInR5cGUiOiJBcnJheSJ9XSwicmV0dXJuVHlwZSI6IkJ5dGVBcnJheSJ9LCJtZXRob2RzIjpbXSwiZXZlbnRzIjpbXX0sInBlcm1pc3Npb25zIjpbeyJjb250cmFjdCI6IioiLCJtZXRob2RzIjoiKiJ9XSwidHJ1c3RzIjpbXSwic2FmZU1ldGhvZHMiOltdLCJleHRyYSI6bnVsbH0MCVcBAhFwIgJoQEHONSyF";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL System.Contract.Create\r\nPUSHDATA1 570102117022026840\r\nPUSHDATA2 7b2267726f757073223a5b5d2c226665617475726573223a7b2273746f72616765223a747275652c2270617961626c65223a66616c73657d2c22616269223a7b2268617368223a22307831333861376334353365653461343935373261343533393031343135646361353866613439306463222c22656e747279506f696e74223a7b226e616d65223a226d61696e222c22706172616d6574657273223a5b7b226e616d65223a226d6574686f64222c2274797065223a22537472696e67227d2c7b226e616d65223a2261726773222c2274797065223a224172726179227d5d2c2272657475726e54797065223a22427974654172726179227d2c226d6574686f6473223a5b5d2c226576656e7473223a5b5d7d2c227065726d697373696f6e73223a5b7b22636f6e7472616374223a222a222c226d6574686f6473223a222a227d5d2c22747275737473223a5b5d2c22736166654d6574686f6473223a5b5d2c226578747261223a6e756c6c7d\r\n");
        }

        [TestMethod]
        public void TestMethod9()
        {
            //部署 NEP-5 合约的 Scripts
            var script = "DfACeyJncm91cHMiOltdLCJmZWF0dXJlcyI6eyJzdG9yYWdlIjp0cnVlLCJwYXlhYmxlIjpmYWxzZX0sImFiaSI6eyJoYXNoIjoiMHhjZTdkYzY3MTIwMzM5ZDk5NTM1YTdhZmUxYjNiYWQ4OGE2YjFmNmEwIiwiZW50cnlQb2ludCI6eyJuYW1lIjoibWFpbiIsInBhcmFtZXRlcnMiOlt7Im5hbWUiOiJtZXRob2QiLCJ0eXBlIjoiU3RyaW5nIn0seyJuYW1lIjoiYXJncyIsInR5cGUiOiJBcnJheSJ9XSwicmV0dXJuVHlwZSI6IkJ5dGVBcnJheSJ9LCJtZXRob2RzIjpbeyJuYW1lIjoicHV0IiwicGFyYW1ldGVycyI6W3sibmFtZSI6Im1lc3NhZ2UiLCJ0eXBlIjoiU3RyaW5nIn1dLCJyZXR1cm5UeXBlIjoiQm9vbGVhbiJ9LHsibmFtZSI6ImdldCIsInBhcmFtZXRlcnMiOlt7Im5hbWUiOiJtZXNzYWdlIiwidHlwZSI6IlN0cmluZyJ9XSwicmV0dXJuVHlwZSI6IkludGVnZXIifSx7Im5hbWUiOiJleGlzdHMiLCJwYXJhbWV0ZXJzIjpbeyJuYW1lIjoibWVzc2FnZSIsInR5cGUiOiJTdHJpbmcifV0sInJldHVyblR5cGUiOiJCb29sZWFuIn1dLCJldmVudHMiOlt7Im5hbWUiOiJzYXZlZCIsInBhcmFtZXRlcnMiOlt7Im5hbWUiOiJhcmcxIiwidHlwZSI6IlN0cmluZyJ9LHsibmFtZSI6ImFyZzIiLCJ0eXBlIjoiSW50ZWdlciJ9XSwicmV0dXJuVHlwZSI6IlNpZ25hdHVyZSJ9XX0sInBlcm1pc3Npb25zIjpbeyJjb250cmFjdCI6IioiLCJtZXRob2RzIjoiKiJ9XSwidHJ1c3RzIjpbXSwic2FmZU1ldGhvZHMiOltdLCJleHRyYSI6bnVsbH0M31cEAhxwQel9OKAMAUCzcWkmSHgmPXgMA3B1dJckGXgMA2dldJckGHgMBmV4aXN0c5ckFCIdeRDONCZyIhl5EM40V3IiEXkQzjVxAAAAciIGEXIiAmpzIgYRcyICa0BXAwF4NFdxaSYGEHIiKyFBfvVyH3B4aFBBm/ZnzkHmPxiEeGhQDAVzYXZlZBPAQZUBb2ERciICakBXAgF4NB4Qs3BoJgYPcSISeEGb9mfOQZJd6DE0HHEiAmlAVwEBeEGb9mfOQZJd6DHYqnAiAmhAVwEBeCQFECIFeBCecCICaEBBzjUshQ==";
            var result = "";
            Program.ScriptsToOpCode(script).ForEach(p => result += p + "\r\n");
            Assert.AreEqual(result, "SYSCALL System.Contract.Create\r\nPUSHDATA1 5704021c7041e97d38a00c0140b37169264878263d780c03707574972419780c03676574972418780c06657869737473972414221d7910ce34267222197910ce34577222117910ce3571000000722206117222026a732206117322026b40570301783457716926061072222b21417ef5721f70786850419bf667ce41e63f18847868500c05736176656413c04195016f61117222026a4057020178341e10b3706826060f71221278419bf667ce41925de831341c712202694057010178419bf667ce41925de831d8aa702202684057010178240510220578109e7022026840\r\nPUSHDATA2 7b2267726f757073223a5b5d2c226665617475726573223a7b2273746f72616765223a747275652c2270617961626c65223a66616c73657d2c22616269223a7b2268617368223a22307863653764633637313230333339643939353335613761666531623362616438386136623166366130222c22656e747279506f696e74223a7b226e616d65223a226d61696e222c22706172616d6574657273223a5b7b226e616d65223a226d6574686f64222c2274797065223a22537472696e67227d2c7b226e616d65223a2261726773222c2274797065223a224172726179227d5d2c2272657475726e54797065223a22427974654172726179227d2c226d6574686f6473223a5b7b226e616d65223a22707574222c22706172616d6574657273223a5b7b226e616d65223a226d657373616765222c2274797065223a22537472696e67227d5d2c2272657475726e54797065223a22426f6f6c65616e227d2c7b226e616d65223a22676574222c22706172616d6574657273223a5b7b226e616d65223a226d657373616765222c2274797065223a22537472696e67227d5d2c2272657475726e54797065223a22496e7465676572227d2c7b226e616d65223a22657869737473222c22706172616d6574657273223a5b7b226e616d65223a226d657373616765222c2274797065223a22537472696e67227d5d2c2272657475726e54797065223a22426f6f6c65616e227d5d2c226576656e7473223a5b7b226e616d65223a227361766564222c22706172616d6574657273223a5b7b226e616d65223a2261726731222c2274797065223a22537472696e67227d2c7b226e616d65223a2261726732222c2274797065223a22496e7465676572227d5d2c2272657475726e54797065223a225369676e6174757265227d5d7d2c227065726d697373696f6e73223a5b7b22636f6e7472616374223a222a222c226d6574686f6473223a222a227d5d2c22747275737473223a5b5d2c22736166654d6574686f6473223a5b5d2c226578747261223a6e756c6c7d\r\n");
        }
    }
}
