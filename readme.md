# Hush Cipher

The Hush cipher is an adaptation of the Vernam cipher which proposes to solve the existing problem of securely and efficiently distributing a one-time, pre-shared key using public key cryptography. Specifically, the Hush cipher employs a combination of Diffie-Hellman key exchange and the SHA-3 Shake-256 hash algorithm to generate secure symmetric keys of arbitrary length.

## One-Time Pad

In cryptography, the one-time pad (OTP) is an encryption technique that has been mathematically proven to be 100% uncrackable, but requires the use of a one-time, pre-shared key the same size as, or longer than the message being sent.

In this technique a plaintext is paired with a random secret key (also referred to as a _one-time_ pad). Then each bit or character of the plaintext is encrypted by combining it with the corresponding bit or character from the pad using modular addition. 

If the key is truly random, at least as long as the plaintext, never reused in whole or part, and kept completely secret, then it is impossible to decrypt or break. 

### Vernam Cipher

A Vernam cipher is a symmetrical stream cipher in which the plaintext is combined with a random stream of data (or "keystream") of at least the same length, to generate a ciphertext using the Boolean "exclusive or" (XOR) function, denoted with the symbol **⊕**.

The cipher is reciprocal in that the identical keystream is used both to encipher plaintext to ciphertext and decipher ciphertext to plaintext.

Encipher: _**C = P ⊕ K**_

Decipher: _**P = C ⊕ K**_

### Problems

Despite the proof of its security, the one-time pad suffers some serious drawbacks in practice for the following reasons:

- One-time pad values must be truly random as opposed to pseudorandom.
- One-time pad values must be securely generated, shared and at least as long as the plaintext.
- One-time pad values must remain secret, used once and disposed of correctly.

One-time pads solve few practical problems in cryptography since other high quality ciphers are available whose security is not considered a major worry at present. such ciphers are almost always easier to employ than one-time pads since the amount of key material that must be properly generated and securely distributed is far smaller, and public key cryptography overcomes this problem.

### Quantum Considerations

In terms of classical computers, the difficult problems that grant asymmetric encryption its security are not considered a major worry, however quantum computers have demonstrated that these problems become trivial, and a quantum computer with enough qubits would render traditional public key cryptography obsolete. In contrast, one-time pads remain secure in post-quantum computing.

## Diffie-Hellman

Diffie-Hellman key exchange is a method of securely exchanging cryptographic keys over a public channel and is one of the earliest practical examples of public key exchange implemented within the field of cryptography. Traditionally, secure encrypted communication between two parties required key exchange by some private, secure channel. 

The Diffie-Hellman key exchange method allows two parties to have no prior knowledge of each other to jointly establish a shared secret key over an insecure channel. The key can then be used to encrypt subsequent communications using a symmetric key cipher.

## SHA-3

SHA-3 is a member of the Secure Hash Algorithm family of standards released by NIST. Whilst being part of the same family on standards, SHA-3's internal implementation is vastly different from other hash algorithm implementations such as MD5, SHA-1 and SHA-2.

SHA-3 is part of a broader cryptographic family, Keccak, which implements an approach known as "sponge construction" as opposed to more traditional "message digest" constructions used in MD5 and other SHA implementations.

Keccak's sponge construction allows any length of data to be input (or "absorbed" in sponge terminology) and can output (or "squeeze") any amount of data using a random permutation function. One of the unique features of the SHA-3 implementation known as SHAKE-128 or SHAKE-256 allows the generation of arbitrary length hashes.

## Proposal

The proposed solution uses a combination of a one-time pad (specifically a Vernam cipher), Diffie-Hellman key exchange and SHA-3 (specifically SHAKE-256).

Fundamentally, the proposed solution does not alter the implementation of the Vernam cipher, rather it aims to address the problem of efficiently creating and distributing a one-time pad using public key cryptography.

### Scenario

Alice wants to send a private message to Bob over a public channel. _Assume that this message is 1KB (or 1024 bytes) long._

**Key Exchange (Diffie-Hellman)**

1. Alice generates a public key and sends it to Bob.
2. Bob generates a public key and sends it to Alice.
3. Both Alice and Bob derive a common secret.

**Encryption (SHAKE-256 & Vernam cipher)**

4. Alice hashes the common secret, producing a 1024 byte long hash. _**K = hf(s, l)**_
5. Alice enciphers the plaintext message using the hashed common secret as a one-time pad. _**C = P ⊕ K**_
6. Alice sends the ciphertext to Bob over a public channel.
7. Alice destroys the common secret and one-time pad.

**Decryption (SHAKE-256 & Vernam cipher)**

8. Bob receives the ciphertext.
9. Bob hashes the common secret, producing a 1024 byte long hash. _**K = hf(s, l)**_
10. Bob deciphers the ciphertext message using the hashed common secret as a one-time pad. _**P = C ⊕ K**_
11. Bob destroys the common secret and one-time pad.