### SDK/Dotnet package

```shell
$ sudo apt install dotnet-sdk-2.2=2.2.105-1
```

### SDK Path for "Invalid SDK" error

```shell
$ sudo nano /etc/profile.d/dotnet.sh
```

```shell
# ---

export DOTNET_ROOT=/opt/dotnet

# OR

export DOTNET_ROOT=/usr/share/dotnet

# OR use to find path:

$ dpkg-query -L dotnet-sdk-2.2

# ---

export MSBuildSDKsPath=$DOTNET_ROOT/sdk/$(${DOTNET_ROOT}/dotnet --version)/Sdks
export PATH=${PATH}:${DOTNET_ROOT}
```

Make sure to restart/open a new shell (to apply ENV variables).
Also, ignore upgrades to the dotnet package (for now, until this issue is fixed).

Keep track of this issue [here](https://github.com/OmniSharp/omnisharp-vscode/issues/2965).

### Testing out IR code

If you'd like to run your emitted IR code, you can either:

1. Install [llc](https://llvm.org/docs/CommandGuide/llc.html).

2. Use [online compiler](https://kripken.github.io/llvm.js/demo.html).

I'd recommend the 2nd option if you're on Windows.
