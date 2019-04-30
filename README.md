#### Ion Language

A language implemented in C# using LLVM 5.0 bindings.

File extension: `.ion`

Syntax examples coming soon.

#### Core Principles

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
fn main(): int {
    Web.mount(<div>Built-in HTML syntax is awesome!</div>);

    return 0;
}
```

This feature, along with decorators & anonymous functions, will come super handy when building APIs!

```rust
str @name = "John Doe";

@Web.route("/") {
    return <p>Hello, {@name}.</p>; // Hello, John Doe.
}
```

4. **Portable**.

#### Development Roadmap

- [ ] Basic control flow (if, else)
- [X] Expressions
- [X] Functions
- [ ] Classes
- [ ] Interfaces
- [ ] Structures
- [ ] Decorators
- [ ] Anonymous functions
- [ ] Object blueprints
- [ ] Async support
- [ ] Delegates
- [ ] JSON integration + support
- [ ] External definitions
- [ ] Import functionality
- [ ] Advanced flow control (switch, etc.)
- [ ] Package manager
- [ ] Package definition (package manager)
- [ ] Web API
- [ ] Built-in DOM support
- [ ] Compilation to JavaScript
- [ ] Syntax highlighting (Visual Studio Code)
- [ ] Cross-platform installer utility
- [ ] Self-hosted codebase
