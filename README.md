# game

## Source Control Config

The repo configurations for git and github are based on [this
guide](https://gist.github.com/j-mai/4389f587a079cb9f9f07602e4444a6ed) for
using git/github with Unity.

Git config uses UnityYAMLMerge utility for merging special unity files, git
needs the environment variable `UNITYYAML` to be configured with the path to
UnityYAMLMerge executable.

To configure the environment variables in your system, run:
```bash
source scripts/set_env.sh
```
