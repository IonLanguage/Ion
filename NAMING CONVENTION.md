#### Functions

All function names should be in camelCase.

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

Attributes are considered proxy functions, thus they should be treated as functions and be named in camelCase.

```c
[Transform(0)]
int main() {
    // Return value will be transformed to '0'.
    return 1;
}
```
