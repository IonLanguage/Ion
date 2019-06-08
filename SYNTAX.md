
<p align="center">
    <img src="https://i.ibb.co/PFS60V2/ion-white-smaller.jpg" height="200px" alt="Ion Logo" />
    <br />
    <a href="https://circleci.com/gh/IonLanguage/Ion">
        <img src="https://circleci.com/gh/IonLanguage/Ion.svg?style=svg" />
    </a>
    <a href="https://discord.gg/H3eMUXp">
        <img src="https://discordapp.com/api/guilds/572951207862206474/widget.png?style=shield" alt="Discord Server" />
    </a>
</p>

## Syntax Examples

### Table of Contents
* [Globals](#)
* [Functions](#)
* [Structs](#)
* [Lambdas](#)
* [Importing modules](#)
<hr/>

#### Globals
> ```cs
>int @counter = 0;
>
>void main() {
>    counter++;
>    // ...
>    }
>```

<hr/>

#### Functions
>```cs
>void join(char delimiter, ...) {
>    // ...
>}
>
>void main() {
>    // ...
>}
>```

<hr/>

#### Structs
>```cs
>struct Person {
>    string name;
>    int age;
>}
>
>void main() {
>    // Create the struct.
>    Person joe = new Person {
>        name: "Joe",
>        age: 25
>    };
>
>    // Access its properties.
>    joe.name;
>    joe.age;
>}
>```

<hr/>

#### Lambdas
>```cs
>delegate void WorkCallback(int status);
>
>void work(WorkCallback callback) {
>    int status;
>    // ...
>    callback(status);
>}
>
>void main() {
>    // Pass lambda as parameter.
>    work((int status) => {
>        // ...
>    });
>}
>```

<hr/>

#### Importing modules

>```cs
>// Import a module.
>import my.module;
>
>// Import a system/built-in module.
>import <os>;
>
>// Import a module with an alias.
>import my.other.module as other;
>
>// ...
>```
