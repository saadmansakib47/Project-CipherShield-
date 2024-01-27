# Project-CipherShield

## Introduction

Welcome to CipherShield, a security based console application developed in C# using Visual Studio. This application allows users to generate passwords according to their requirement, assess the strength of their password, encrypt and decrypt passwords/texts using different encryption algorithms and finally hash their passwords/texts using different hashing algorithms that have been implemented from scratch.

## Motivation Behind the Project

CipherShield was created out of our passion for CyberSecurity and Cryptogrphic functions. We wanted to learn about Password salting and implement various Encryption and Hashing algorithms from scratch following the OOP concepts and SOLID principles. This project can serve as an educational tool to help users understand password security best practices.

# Getting Started

## Installation & Running the application

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
5. Make sure you have Visual Studio installed, if not then install it from [https://visualstudio.microsoft.com/downloads/](https://visualstudio.microsoft.com/downloads/)

6. Open Visual Studio. In Visual Studio, click on File > Open > Project/Solution

7. Navigate to the directory where you cloned the Project-CipherShield- repository, select the solution file (.sln), and click Open.

8. Once the project is open in Visual Studio, locate the Run button (usually a green play button) in the toolbar or press F5 to build and run the application.

## Usage

Choose one of the 3 main features of our project-

![Main Page](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/b6c3d4db-fb80-40bd-b110-92b83bc72463)

### Password Toolbox

#### Password Generator

Generates password based on user inputs such as if the user wants to include uppercase or lowercase characters, numbers, special symbols and the length of the password etc.

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/a02a09f8-a708-47ea-b1eb-1a3da75c8b91)


#### Strength Checker

Checks the strenth of the password that the user inputs and shows 4 types of feedback which are:

- Randomness
- Pattern Detection
- Repetition
- Length

It also shows an overall strenth score and strenth feedback which is calculated based on the above 4 metrics.

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/52e20959-36d4-42b7-8246-1507bcde2561)


### Encryption

Encrypts and Decrypts the text/password that the user inputs using common encryption algorithms.

#### Substitution Cipher (Vignere Cipher)

1. **Encrypt:** Users have to input a message along with a key

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/5a3e97a1-30ad-43b0-8f66-7a21a549b370)


2. **Decrypt:** Users have to input the encrypted text along with the key

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/9122b4d2-ca44-41ff-8e05-b6860185bfc0)


#### Transposition Cipher (RailFence Cipher)

1. **Encrypt:** Users have to input a message(in capital form) along with the number of rails

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/77b75d78-7c5c-4a4a-a5b6-e6161b9b2eda)


2. **Decrypt:** Users have to input the encrypted text along with the number of rails

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/1b32f08b-d08d-4b83-b325-b0f040cfa17b)

#### Block Cipher (DES)

1. **Encrypt:** Users have to input a 16 character key in hexadecimal format along with the message

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/0044cdae-b465-416a-b9ca-1f0a3b457e55)


2. **Decrypt:** Users have to input the 16 character key in hexadecimal format along with the encrypted text

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/e430535b-b019-4795-b079-2a2ab679f05d)

#### RSA

1. **Encrypt:** The program generates a private key`(d, n)` and a public key`(e, n)`. The user have to input the message and gets the ciphertext back based on those keys.

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/2a0df001-6f2a-4a7d-87ab-ef539cb15313)


2. **Decrypt:** Users have to input the public exponent`(e)`, private exponent`(d)`, modulus`(n)` and the ciphertext to get the original message back.

![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/da4da787-72e3-4c13-b12f-4cba1ea0e00e)

### Hashing

Hashes the text/password that the user inputs using common hashing algorithms.

#### MD5 & SHA-256
- The user enters a palintext and gets the hashed message back.


![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/e0eb0fba-938c-4dd0-aae3-7093cb5e7621)


![image](https://github.com/saadmansakib47/Project-CipherShield-/assets/112499963/4eb65033-e5ad-47af-8624-6a0de07c46bf)


## Developers

- [Nazifa Tasneem](https://github.com/nazifatasneem13)
- [Nusrat Siddique Tuli](https://github.com/ns-tuli)
- [Saadman Sakib](https://github.com/saadmansakib47)

This project was developed by us for the Software Project Lab 1 (SWE 4301) during the 3rd semester at the Islamic University of Technology(IUT).
