### Ion language

A language implemented in C# using LLVM 5.0 bindings.

File extension: `.ion`

### Syntax

Hello world application:

```cpp
extern int printf(string format, ...);

void main()
{
    printf("Hello world!");
}
```

### Core principles

1. **Simplicity**. The language should be simple, or as powerful as the programmer wishes. This means that some symbols and patterns are optional and infered by the compiler.

2. **Flexible**. The language should contain tools and shortcuts to make the programming experience smooth, not rigid. Pipes are the best example of a planned feature that will add flexibility in the development environment.

An example usage of pipes:

```rust
...
"Hello %s", "world!" | printf;

// Is equivalent to:

printf("Hello %s", "world!");
...
```
3. **Built-in DOM support**.

Inspired by React.js' JSX syntax, the language will have built-in DOM and HTML support.

```rust
int main() 
{
    Web.Mount(<div>Built-in HTML syntax is awesome!</div>);

    return 0;
}
```

This feature, along with decorators & anonymous functions, will come super handy when building APIs!

```rust
string @name = "John Doe";

[@Web.Route("/")]
void GetRoot() {
    return <p>Hello, {@name}.</p>; // Hello, John Doe.
}
```

4. **Portable**.

### Examples

#### Hello world

```c#
extern int printf(string message, ...);

int main()
{
    printf("Hello world!");

    return 0;
}
```

#### Fibonacci sequence algorithm implementation

```c#
int fibonacci(number)
{
    if (number <= 1)
    {
        return 1;
    }

    return fibonacci(number - 1) + fibonacci(number - 2);
}
```

### Naming convention

#### Functions

All function names should be in PascalCase.

```rust
void main()
{
    //
}
```

#### Classes

Class names should be in PascalCase, and members in camelCase.

```c#
import Core.Console;

class Example
{
    pub string name = "John Doe";

    void SayHello()
    {
        Console.Log("Hello, " + this.name);
    }
}
```

#### Attributes

Attributes are considered proxy functions, thus they should be treated as functions and be named in PascalCase.

```c
[Transform(0)]
int main()
{
    // Return value will be transformed to '0'.
    return 1;
}
```

### Installation

Head over to the releases page on the CLI utility's repository to get your installer!

> [View releases](https://github.com/IonLanguage/Ion.CLI/releases)


### Getting started

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
extern int printf(string, ...);

int main()
{
    printf("Hello world!");

    return 0;
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
extern int printf(string, ...);

int main()
{
    printf("Hello world!");

    return 0;
}
```

Now, save your file by pressing `CTRL + X`, then pressing `Y` and finally `ENTER`.

You're now ready to run your program! Use the following command to compile & run your program:

```shell
$ ion run
```
