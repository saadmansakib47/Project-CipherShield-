# Project-CipherShield

## Introduction

Welcome to CipherShield, a security based console application developed in C# using Visual Studio. This application allows users to generate passwords according to their requirement, assess the strength of their password, encrypt and decrypt passwords/texts using different encryption algorithms and finally hash their passwords/texts using different hashing algorithms that have been implemented from scratch.

## Motivation Behind the Project

CipherShield was created out of our passion for CyberSecurity and Cryptogrphic functions. We wanted to learn about Password salting and implement various Encryption and Hashing algorithms from scratch following the OOP concepts and SOLID principles. This project can serve as an educational tool to help users understand password security best practices.

# Getting Started

## Installation

To run the Project-CipherShiled on your local machine, follow these steps:

1. Ensure you have Git installed. If not, download and install it from [https://git-scm.com/downloads](https://git-scm.com/downloads).

2. Clone the GitHub repository to your local machine.

    ```bash
    git clone https://github.com/saadmansakib47/Project-CipherShield-.git
    ```

3. Navigate to the project directory.

    ```bash
    cd Project-CipherShield-
    ```

4. Install the project dependencies.

    ```bash
    npm install
    ```

## Usage

Choose one of the 3 main features of our project-

### Password Toolbox

#### Password Generator

Generates password based on user inputs such as if the user wants to include uppercase or lowercase characters, numbers, special symbols and the length of the password etc.

#### Strength Checker

Checks the strenth of the password that the user inputs and shows 4 types of feedback which are:

- Randomness
- Pattern Detection
- Repetition
- Length

It also shows an overall strenth score and strenth feedback which is calculated based on the above 4 metrics.

The scoring range info is:

- Score < 460 : Very Weak
- 460 ≤ Score < 500 : Weak
- 500 ≤ Score < 600 : Moderate
- 600 ≤ Score < 670 : Strong
- 670 ≤ Score  : Very Strong

### Encryption

Encrypts and Decrypts the text/password that the user inputs using common encryption algorithms.

#### Substitution Cipher (Vignere Cipher)

1. **Encrypt:** Users have to input a message along with a key
2. **Decrypt:** Users have to input the encrypted text along with the key

#### Transposition Cipher (RailFence Cipher)

1. **Encrypt:** Users have to input a message(in capital form) along with the number of rails
2. **Decrypt:** Users have to input the encrypted text along with the number of rails

#### Block Cipher (DES)

1. **Encrypt:** Users have to input a 16 character key in hexadecimal format along with the message
2. **Decrypt:** Users have to input the 16 character key in hexadecimal format along with the encrypted text

#### RSA

1. **Encrypt:** The program generates a private key(d, n) and a public key(e, n). The user have to input the message and gets the ciphertext back based on those keys.
2. **Decrypt:** Users have to input the public exponent(e), private exponent(d), modulus(n) and the ciphertext to get the original message back.

### Hashing

Hashes the text/password that the user inputs using common hashing algorithms.

#### MD5
- The user enters a palintext and gets the hashed message back.

#### SHA-256
- The user enters a palintext and gets the hashed message back.

## GitHub Repository

The Project-CipherShield- GitHub repository can be found at [https://github.com/saadmansakib47/Project-CipherShield-.git](https://github.com/saadmansakib47/Project-CipherShield-.git).

## Developers

- [Nazifa Tasneem](https://github.com/nazifatasneem13)
- [Nusrat Siddique Tuli](https://github.com/ns-tuli)
- [Saadman Sakib](https://github.com/saadmansakib47)

This project was developed by us for the Software Project Lab 1 (SWE 4301) during the 3rd semester at the Islamic University of Technology(IUT).
