os="$(uname)"

if [[ "$os" == "Linux" ]]; then
    export UNITYYAML="$HOME/Unity/Hub/Editor/6000.0.43f1/Editor/Data/Tools/UnityYAMLMerge"
elif [[ "$os" =~ (MINGW|MSYS|CYGWIN) ]]; then
    export UNITYYAML="C:/Program Files (x86)/Unity/Editor/Data/Tools/UnityYAMLMerge.exe"
else
    echo "Unknown OS: $os"
fi
