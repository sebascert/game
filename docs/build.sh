#! /bin/bash

scrip_dir=$(readlink -f "$0" | xargs dirname)
docs_dir="$scrip_dir"

output="$docs_dir/docs.pdf"
metadata="$docs_dir/metadata.yaml"

# paths relative to docs_dir
# the listed order is the same as the rendering order
sources=(
    "portada.md"
    "tematica.md"
    "mecanicas.md"
)

for i in "${!sources[@]}"; do
    sources[i]="$docs_dir/${sources[$i]}"
done

pandoc --metadata-file="$metadata" "${sources[@]}" -o "$output"
