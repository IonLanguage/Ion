#### Table of contents

[TODO](#)

#### Functions

```cs
void join(char delimiter, ...) {
    // ...
}

void main() {
    // ...
}
```

#### Structs

```cs
struct Person {
    string name;
    int age;
}

void main() {
    // Create the struct.
    Person joe = new Person {
        name: "Joe",
        age: 25
    };

    // Access its properties.
    joe.name;
    joe.age;
}
```

#### Lambdas

```cs
delegate void WorkCallback(int status);

void work(WorkCallback callback) {
    int status;
    // ...
    callback(status);
}

void main() {
    // Pass lambda as parameter.
    work((int status) => {
        // ...
    });
}
```

#### Importing modules

```cs
// Import a module.
import my.module;

// Import a system/built-in module.
import <os>;

// Import a module with an alias.
import my.other.module as other;

// ...
```
