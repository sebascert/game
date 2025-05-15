# game

## Formatting Source Code

Non formatted source code won't be merged, use the `dotnet format` cli util for
formatting, which is installed along with the [dotnet
SDK](https://learn.microsoft.com/en-us/dotnet/core/install/).

Format with:

```bash
dotnet format Assembly-CSharp.csproj
```

> See [Generating Assembly-CSharp.csproj](#generating-assembly-csharpcsproj) if
> it's missing.

## Code Style

Code style rules are specified in `.editorconfig` file, which are used for
formatting. The configurations is mostly based on the [microsoft
defaults](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/code-style-rule-options).

## Source Control Config

The repo configurations based on [this
guide](https://gist.github.com/j-mai/4389f587a079cb9f9f07602e4444a6ed) for
using git/github with Unity.

Git config uses UnityYAMLMerge utility for merging special unity files, git
needs the environment variable `UNITYYAML` to be configured with the path to
UnityYAMLMerge executable.

To configure the environment variables in your system, run:

```bash
source scripts/set_env.sh
```

## Generating Assembly-CSharp.csproj

The `Assembly-CSharp.csproj` is a `MSBuild project file`, which contains
compilation and linkage information for the game scripts under `Assets`, one
way to generate it is to configure the path of a C# editor (like vscode or
visual studio) under `preferences/External Tools/External Script Editor`, and
then click on `Regenerate project file`.
