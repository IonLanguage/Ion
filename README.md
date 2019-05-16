### Ion language

A language implemented in C# using LLVM 5.0 bindings.

File extension: `.ion`

Syntax examples coming soon.

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

@Web.Route("/") {
    return <p>Hello, {@name}.</p>; // Hello, John Doe.
}
```

4. **Portable**.

### Naming convention

#### Functions

All functions should be in PascalCase.

```rust
void main()
{
    //
}
```

#### Classes

Class names should be in PascalCase, and members in camelCase.

```cpp
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

### Local installation

If you'd like to try out the language at its current, early state, follow the short guide for your platform below.

#### Windows

Please install [Linux sub-system for Windows](https://docs.microsoft.com/en-us/windows/wsl/install-win10) and follow the Linux installation steps.

#### Linux

1. Since the required CLI utility to compile source code is written in C#, you will need .NET Core SDK installed on your machine.

    [Click here to view how to install .NET Core SDK](https://dotnet.microsoft.com/download/linux-package-manager/ubuntu16-04/sdk-current)

    Select your Linux distribution and follow the steps provided.

    Ensure you've correctly installed .NET Core by running:

    ```shell
    $ dotnet --version
    ```

2. Now, you require the CLI utility to invoke the compiler.

    [Click here to view IonCLI's releases](https://github.com/IonLanguage/Ion.CLI/releases)

    Download the latest release, and follow the installation instructions contained within the `README.md` file (they're pretty straight forward!).

3. You may now invoke the CLI utility and compile source files using:

    ```shell
    $ ion --ir
    ```

4. Finally, you'd want to run your programs. You can use `lli`, a command included in the LLVM toolchain to directly execute IR code, for this. After compilation using the IonCLI, you may issue the following command to run your program:

    ```shell
    $ lli l.bin/program.ll
    ```

Congratulations! You've installed all requirements to use the Ion language on your machine.

The installation process is currently somewhat complicated, however we are working on making this much, much simpler.
