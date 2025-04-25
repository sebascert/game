#! /bin/bash

scrip_dir=$(readlink -f "$0" | xargs dirname)
docs_dir="$scrip_dir"

output="$docs_dir/docs.pdf"
metadata="$docs_dir/metadata.yaml"

# the order in which sources are listed is the same as the rendering order
sources=(
    "portada.md"
)

for i in "${!sources[@]}"; do
    sources[i]="$docs_dir/${sources[$i]}"
done

pandoc --metadata-file="$metadata" "${sources[@]}" -o "$output"
