# OpCodeConverter

Neo3 的 OpCode 转换工具，可以将交易中的 Script 转换为易读易懂的 OpCode，支持交易的 script 字段，或者 witness.invocation、witnesses.verification 字段。

输入为 Base64 编码的 script，输出为 List\<string\>  格式的 OpCode 列表。

### 示例

------

### 常见的 invocation

input: 

```
DECKiNs7nm9rKamTRSQpjuRHmmKZlX0n1m89FfDzOvgcQIe7JfMnsg/4Ss1yHVwTxpmDjs1GWRcRyntZ06S81fIF
```

output: 

```
PUSHDATA1 8a88db3b9e6f6b29a9934524298ee4479a6299957d27d66f3d15f0f33af81c4087bb25f327b20ff84acd721d5c13c699838ecd46591711ca7b59d3a4bcd5f205
```

### 常见的 verification

input: 

```
EQwhA9q4TBJD7AGrJQDhqMehVGom1zRigYCwz2TnK/d2U2mXEQtBE43vrw==
```

output:

```
PUSHDATA1 03dab84c1243ec01ab2500e1a8c7a1546a26d734628180b0cf64e72bf776536997
SYSCALL Neo.Crypto.CheckMultisigWithECDsaSecp256r1
```

### NEO 转账的 Scripts

input: 

```
EQwUvQMah0TTRtYWcYFgFy9jaOAX3AUMFL0DGodE00bWFnGBYBcvY2jgF9wFE8AMCHRyYW5zZmVyDBQlBZ7LSHjTqHX5HFHO3tMw1Fdf3kFifVtSOA==
```

output:

```
PUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd
PUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd
PUSHDATA1 transfer
PUSHDATA1 0xde5f57d430d3dece511cf975a8d37848cb9e0525
SYSCALL System.Contract.Call
```

------

### NEO 转账的 Scripts(2)

input: 

```
EQwUvQMah0TTRtYWcYFgFy9jaOAX3AUMFL0DGodE00bWFnGBYBcvY2jgF9wFE8AMCHRyYW5zZmVyDBQlBZ7LSHjTqHX5HFHO3tMw1Fdf3kFifVtSOA==
```

output:

```
PUSHINT8 100
PUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd
PUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd
PUSHDATA1 transfer
PUSHDATA1 0xde5f57d430d3dece511cf975a8d37848cb9e0525\r\nSYSCALL System.Contract.Call
```

### GAS 转账的 Scripts

input: 

```
AwDkC1QCAAAADBS9AxqHRNNG1hZxgWAXL2No4BfcBQwUgUkpDXgVmkkrRX4lahSzu86gNiATwAwIdHJhbnNmZXIMFLyvQdaEx9StbuDZnalwe50fDI5mQWJ9W1I4
```

output:

```
PUSHINT64 10000000000
PUSHDATA1 0x05dc17e068632f1760817116d646d344871a03bd
PUSHDATA1 0x2036a0cebbb3146a257e452b499a15780d294981
PUSHDATA1 transfer
PUSHDATA1 0x668e0c1f9d7b70a99dd9e06eadd4c784d641afbc
SYSCALL System.Contract.Call
```

------

### NEP-5 转账的 Scripts

NEP-5 的 ScriptHash 为 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9

input:

```
AwDyBSoBAAAADBTUzRIZzo4XK1AnOCPXmaNl+raw5AwU1M0SGc6OFytQJzgj15mjZfq2sOQTwAwIdHJhbnNmZXIMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I5
```

output:

```
PUSHINT64 5000000000
PUSHDATA1 0xe4b0b6fa65a399d7233827502b178ece1912cdd4
PUSHDATA1 0xe4b0b6fa65a399d7233827502b178ece1912cdd4
PUSHDATA1 transfer
PUSHDATA1 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9
SYSCALL System.Contract.Call
```

------


### 调用 NEP-5 "name" 方法的 Scripts

NEP-5 的 ScriptHash 为 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9

input:

```
EMAMBG5hbWUMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I=
```

output: 

```
PUSHDATA1 name
PUSHDATA1 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9
SYSCALL System.Contract.Call
```

------

