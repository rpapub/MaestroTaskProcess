#!/usr/bin/env python3
"""
C# Source File Aggregator for LLM Ingestion

Author: Christian Prior-Mamulyan
Email: cprior@gmail.com
License: CC-BY

Description:
This script recursively scans a directory for `.cs` files,
reads their contents, and concatenates them into a single
output suitable for LLM ingestion. Each file is wrapped with
clear START/END delimiters including its full path.
"""

import sys
import os

SEPARATOR_TEMPLATE = "===== {} FILE: {} =====\n"

def find_cs_files(root_dir):
    """
    Recursively finds all .cs files under a directory.

    Args:
        root_dir (str): Directory to start search.

    Returns:
        list[str]: List of full file paths to .cs files.
    """
    cs_files = []
    for dirpath, _, filenames in os.walk(root_dir):
        for fname in filenames:
            if fname.endswith(".cs"):
                cs_files.append(os.path.join(dirpath, fname))
    return cs_files

def aggregate_files(file_paths):
    """
    Reads and wraps each file with delimiters.

    Args:
        file_paths (list[str]): List of .cs file paths.

    Returns:
        str: Concatenated string of all files with separators.
    """
    parts = []
    for path in file_paths:
        try:
            with open(path, 'r', encoding='utf-8') as f:
                content = f.read()
            parts.append(SEPARATOR_TEMPLATE.format("START OF", path))
            parts.append(content)
            parts.append('\n' + SEPARATOR_TEMPLATE.format("END OF", path))
        except Exception as e:
            print(f"Failed to read {path}: {e}", file=sys.stderr)
    return '\n'.join(parts)

def main(project_root, output_file):
    """
    Main function to generate the output from .cs files.

    Args:
        project_root (str): Root path of the project directory.
        output_file (str): Path to the output file.
    """
    project_root = os.path.abspath(os.path.normpath(project_root))
    output_file = os.path.abspath(os.path.normpath(output_file))

    cs_files = find_cs_files(project_root)
    combined = aggregate_files(cs_files)
    with open(output_file, 'w', encoding='utf-8') as f:
        f.write(combined)


if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Usage: python aggregate_cs_files.py <project_root> <output_file>")
        sys.exit(1)
    main(sys.argv[1], sys.argv[2])
