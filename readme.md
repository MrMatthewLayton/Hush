# Hush Cipher

| Key           | Value                                             |
| ------------- | ------------------------------------------------- |
| Author        | Matthew Layton                                    |
| Date          | February 28th, 2019                               |
| Alma mater    | University of Bedfordshire (BSc Computer Science) |
| Email address | matthew.layton@live.co.uk                         |

The Hush cipher is an attempt to solve the existing problems of key exchange when using the Vernam cipher, using a combination of Diffie-Hellman key exchange and SHA-3 Shake 256 to generate secure, arbitrary length symmetric keys.

## One-Time-Pad & Vernam Ciphers

In cryptography, the one-time pad (OTP) is an encryption technique that has been mathematically proven to be 100% uncrackable, but requires the use of a one-time pre-shared key the same size as, or longer than the message being sent. 

In this technique a plaintext is paired with a random secret key (also referred to as a _one-time_ pad). Then each bit or character of the plaintext is encrypted by combining it with the corresponding bit or character from the pad using modular addition. 

If the key is truly random, at least as long as the plaintext, never reused in whole or part, and kept completely secret, then it is impossible to decrypt or break. 

A patented version of the one-time pad called the Vernam cipher uses XOR operations over each plaintext and pad bit or character.

## Problems

Despite the proof of its security, the one-time pad suffers some serious drawbacks in practice because it requires:

- Truly random, as opposed to pseudo-random, one-time pad values, which is a non-trivial requirement.
- Secure generation and exchange of one-time pad values, which must be at least as long as the message. The security of the one-time pad is only as secure as the exchange of the pre-shared key.
- Careful treatment to make sure that the one-time pad values continue to remain secret, and are disposed of correctly, preventing any reuse in whole or part - hence "one time".

One-time pads solve few current practical problems in cryptography. High quality ciphers are widely available and their security is not considered a major worry at present. Such ciphers are almost always easier to employ than one-time pads; the amount of key material that must be properly generated and securely distributed is far smaller, and public key cryptography overcomes this problem.

Quantum computers have been shown by Peter Shor and others to be much faster at solving some of the difficult problems that grant asymmetric encryption its security. If quantum computers are built with enough qubits, and overcoming some limitations to error-correction; traditional public key cryptography will become obsolete. One-time pads, however, will remain secure. See quantum cryptography and post-quantum cryptography for further discussion of the ramifications of quantum computers to information security.

## Solution

As suggested, public key cryptography overcomes the problem of how much key material is required to be securely distributed in contrast to securely distributing a one-time, pre-shared private key.

Public key cryptography is part of the proposed solution. In the given example, Diffie-Hellman key exchange is used to exchange public keys, however this only solves half of the problem, since Diffie-Hellman public keys are unlikely to be long enough to use as one-time, pre-shared private keys.

The second part of the proposed solution is to use SHA-3 which is implemented using a sponge construction rather than a message digest. In the given example, SHAKE-256 is used because it allows arbitrary length hashes to be generated. In this case, a one-time, pre-shared private key can be derived in secret by absorbing the Diffie-Hellman public key, and squeezing out a hash of the desired message length.

_**privateKey = shake(publicKey, length)**_

