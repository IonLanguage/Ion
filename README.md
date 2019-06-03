<p align="center">
    <img src="https://i.ibb.co/ZJJGJmn/ion-full.jpg" alt="Ion Logo" />
    <br />
    <a href="https://circleci.com/gh/IonLanguage/Ion">
        <img src="https://circleci.com/gh/IonLanguage/Ion.svg?style=svg" />
    </a>
    <a href="https://discord.gg/H3eMUXp">
        <img src="https://discordapp.com/api/guilds/133049272517001216/widget.png?style=shield" alt="Discord Server" />
    </a>
</p>

A language implemented in C# using LLVM 5.0 bindings.

**What is Ion?** Ion is a high-level (and low-level), strongly-typed language implemented using the LLVM project, currently undergoing early development.

**What is the purpose of this project?** A research project. The purpose of this language is to serve as the basis for learning, and experimenting with compiler design. However, the language is to be fully implemented and functional.

**Why should I use Ion?** If you're a fan of fast development, and simply want your development workflow to be smooth, yet rigid when necessary, Ion is a language for you.

**How can I learn compiler design?** We strongly recommend you start your journey into the mysterious and complex world of compiler design through [this book](http://www.informatik.uni-bremen.de/agbkb/lehre/ccfl/Material/ALSUdragonbook.pdf). Wikipedia is also your best friend.

**What is the technology stack used for this project?** The C# language is the main language used to implement both the core compiler library (this repository) and the CLI utility. LLVM (using C# bindings) is used for code generation. NUnit is used for both compiler and CLI tests, although a partial custom testing framework (based upon NUnit) is currently being implemented in order to satisfy the specific unit testing needs of the projects.

File extension: `.ion`

### 📋 Table of Contents

* [Requirements](#-requirements)
* [Installation](#-installation)
* [Getting started](#-getting-started)
    * [Windows](#windows)
    * [Linux](#linux)
* [Core principles](#-core-principles)
* [Examples](#-examples)
* [Naming convention](#-naming-convention)

### 🧩 Requirements

> [Microsoft Visual C++ Redistributable 2015](https://www.microsoft.com/en-us/download/details.aspx?id=48145) (**Windows only**)

### 📦 Installation

Head over to the releases page on the CLI utility's repository to get your installer!

> [View releases](https://github.com/IonLanguage/Ion.CLI/releases)


### 🚀 Getting started

Make sure you have Ion installed on your machine at this point.

#### Windows

Use the following instructions to initialize and run a project in Windows.

```shell
cmd> mkdir myproject
cmd> cd myproject
cmd> ion init
cmd> mkdir Src
cmd> notepad Src/main.ion
```

Paste the following source code:

```c#
extern int puts(string message);

void main() {
    puts("Hello world!");
}
```

Now, save your file and close the Notepad editor.

You're now ready to run your program! Use the following command to compile & run your program:

```shell
cmd> ion run
```

#### Linux

Use the following instructions to initialize and run a project in Linux.

```shell
$ mkdir myproject
$ cd myproject
$ ion init
$ mkdir Src
$ nano Src/main.ion
```

Paste the following source code:

```c#
extern int puts(string message);

void main() {
    puts("Hello world!");
}
```

Now, save your file by pressing `CTRL + X`, then pressing `Y` and finally `ENTER`.

You're now ready to run your program! Use the following command to compile & run your program:

```shell
$ ion run
```

### 📜 Core principles

1. **Simplicity**. The language should be simple, or as powerful as the programmer wishes. This means that some symbols and patterns are optional and infered by the compiler.

2. **Fully managed**.

3. **Flexible**. The language should contain tools and shortcuts to make the programming experience smooth, not rigid. Pipes are the best example of a planned feature that will add flexibility in the development environment.

### ✨ Syntax highlighting

> [Notepad++](Notepad%2B%2B%20Ion.xml)

> [Visual Studio Code](https://github.com/IonLanguage/Ion.VSCode) (**Undergoing early development**)

### 📚 Examples

Hello world

```cs
extern int puts(string message);

void main() {
    puts("Hello world!");
}
```

Fibonacci sequence

```cs
int fib(number) {
    if (number <= 1) {
        return 1;
    }

    return fib(number - 1) + fib(number - 2);
}
```

Formatted output

```cs
extern int printf(string format, ...);

void main() {
    printf("%d + 2 = 3", 1);
}
```

Struct

```cs
extern int printf(string format, ...);

struct Person {
    string name;
    int age;
}

void main() {
    Person joe = new Person {
        name: "Joe",
        age: 25
    };

    printf("Joe's age in 5 years will be: %d.", joe.age + 5);
}
```

### ⚖ Naming convention

#### Functions

All function names should be in PascalCase.

```rust
void main() {
    //
}
```

#### Classes

Class names should be in PascalCase, and members in camelCase.

```c#
extern int printf(string message, ...);

class Example {
    public string name = "John Doe";

    void sayHello() {
        printf("Hello, " + this.name);
    }
}
```

#### Attributes

Attributes are considered proxy functions, thus they should be treated as functions and be named in PascalCase.

```c
[Transform(0)]
int main() {
    // Return value will be transformed to '0'.
    return 1;
}
```
