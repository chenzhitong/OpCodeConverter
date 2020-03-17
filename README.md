# OpCodeConverter

OpCode 转换工具

### 示例

------

常见的 invocation

input: 

```
DECKiNs7nm9rKamTRSQpjuRHmmKZlX0n1m89FfDzOvgcQIe7JfMnsg/4Ss1yHVwTxpmDjs1GWRcRyntZ06S81fIF
```

output: 

```
PUSHDATA1 8a88db3b9e6f6b29a9934524298ee4479a6299957d27d66f3d15f0f33af81c4087bb25f327b20ff84acd721d5c13c699838ecd46591711ca7b59d3a4bcd5f205
```


output(raw): 

```
PUSHDATA1 LENGTH:64 8a88db3b9e6f6b29a9934524298ee4479a6299957d27d66f3d15f0f33af81c4087bb25f327b20ff84acd721d5c13c699838ecd46591711ca7b59d3a4bcd5f205
```

------

常见的 verification

input: 

```
DCEDrHZSlAddpveSfJa/49P2SuNoDF61D4L1UXCp8b6lna0LQQqQatQ=
```

output:

```
PUSHDATA1 03ac765294075da6f7927c96bfe3d3f64ae3680c5eb50f82f55170a9f1bea59dad
SYSCALL 0a906ad4
```

output(raw): 

```
PUSHDATA1 LENGTH:33 03ac765294075da6f7927c96bfe3d3f64ae3680c5eb50f82f55170a9f1bea59dad
SYSCALL 0a906ad4
```

------

NEO 转账的 Scripts

input: 

```
AgDh9QUMFHuv2LNVxgojhF87HIzMuK8GKakRDBTUzRIZzo4XK1AnOCPXmaNl+raw5BPADAh0cmFuc2ZlcgwU+fgUl8P5tiupP3PHEdQbHu/1DCNBYn1bUjk=
```

output:

```
PUSHINT32 100000000
PUSHDATA1 0x11a92906afb8cc8c1c3b5f84230ac655b3d8af7b
PUSHDATA1 0xe4b0b6fa65a399d7233827502b178ece1912cdd4
PUSHDATA1 transfer
PUSHDATA1 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9
SYSCALL 627d5b52
```

output(raw): 

```
PUSHINT32 00e1f505
PUSHDATA1 LENGTH:20 7bafd8b355c60a23845f3b1c8cccb8af0629a911
PUSHDATA1 LENGTH:20 d4cd1219ce8e172b50273823d799a365fab6b0e4
PUSHDATA1 LENGTH:8 7472616e73666572
PUSHDATA1 LENGTH:20 f9f81497c3f9b62ba93f73c711d41b1eeff50c23
SYSCALL 627d5b52
```



------

NEP-5 转账的 Scripts

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
SYSCALL 627d5b52
```

output(raw): 

```
PUSHINT64 00f2052a01000000
PUSHDATA1 LENGTH:20 d4cd1219ce8e172b50273823d799a365fab6b0e4
PUSHDATA1 LENGTH:20 d4cd1219ce8e172b50273823d799a365fab6b0e4
PUSHDATA1 LENGTH:8 7472616e73666572
PUSHDATA1 LENGTH:20 f9f81497c3f9b62ba93f73c711d41b1eeff50c23
SYSCALL 627d5b52
```



------

调用 NEP-5 "name" 方法的 Scripts

NEP-5 的 ScriptHash 为 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9

**input:**

```
EMAMBG5hbWUMFPn4FJfD+bYrqT9zxxHUGx7v9QwjQWJ9W1I=
```

**output:** 

```
PUSHDATA1 name
PUSHDATA1 0x230cf5ef1e1bd411c7733fa92bb6f9c39714f8f9
SYSCALL 627d5b52
```

**output(raw):** 

```
PUSHDATA1 LENGTH:4 6e616d65
PUSHDATA1 LENGTH:20 f9f81497c3f9b62ba93f73c711d41b1eeff50c23
SYSCALL 627d5b52
```

------

