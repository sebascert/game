#!/bin/bash

scrip_dir=$(readlink -f "$0" | xargs dirname)
target="$scrip_dir/../.gitignore"

# gitignore url targets
unity_source="https://raw.githubusercontent.com/github/gitignore/main/Unity.gitignore"
windows_source="https://raw.githubusercontent.com/github/gitignore/main/Global/Windows.gitignore"
macos_source="https://raw.githubusercontent.com/github/gitignore/main/Global/macOS.gitignore"

# build target from local .gitignore and fetching latest .gitignore sources
{
    if [[ -f .gitignore ]]; then
        echo "# --- local.gitignore ---"
        cat "$scrip_dir/.gitignore"
        echo "# --- end local.gitignore ---"
    fi

    echo "# --- unity.gitignore ---"
    curl -s "$unity_source"
    echo "# --- end unity.gitignore ---"

    echo -e "# --- windows.gitignore ---"
    curl -s "$windows_source"
    echo "# --- end windows.gitignore ---"

    echo -e "# --- macos.gitignore ---"
    curl -s "$macos_source"
    echo "# --- end macos.gitignore ---"
} > "$target"
