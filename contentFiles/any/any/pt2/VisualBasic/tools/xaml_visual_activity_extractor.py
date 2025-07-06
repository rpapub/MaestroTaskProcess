#!/usr/bin/env python3
"""
XAML Graphical Activity Extractor

Author: Christian Prior-Mamulyan
Email: cprior@gmail.com
License: CC-BY

Description:
This script parses a UiPath XAML workflow file and identifies
graphically represented activities based on structural rules
and a metadata tag blacklist. The output lists visible activities
with their hierarchy path and display names.
"""

import xml.etree.ElementTree as ET
import sys

# --- Blacklist of non-visual tags ---
# These tags represent metadata or structural elements that are not shown
# in the visual workflow designer (e.g., variable declarations, layout hints).
BLACKLIST_TAGS = {
    "Members",
    "HintSize",
    "Property",
    "TypeArguments",
    "WorkflowFileInfo",
    "Annotation",
    "ViewState",
    "Collection",
    "Dictionary",
    "ActivityAction",
}

def is_graphical(elem):
    """
    Determines if a given XML element represents a graphically shown activity.

    Args:
        elem (Element): XML element from the XAML tree.

    Returns:
        bool: True if the element should be shown in the visual designer.
    """
    tag = elem.tag.split('}')[-1]
    return tag not in BLACKLIST_TAGS and (
        "DisplayName" in elem.attrib or tag in {
            "Sequence", "TryCatch", "Flowchart", "Parallel", "StateMachine"
        }
    )

def extract_visuals(elem, depth=0, path=""):
    """
    Recursively extracts graphically represented activities from the XML tree.

    Args:
        elem (Element): Current XML element to process.
        depth (int): Nesting depth for visual indentation.
        path (str): Accumulated path of tags to this element.

    Returns:
        list[dict]: List of visual activity metadata.
    """
    tag = elem.tag.split('}')[-1]
    display = elem.attrib.get("DisplayName", "")
    node_path = f"{path}/{tag}" if path else tag

    results = []
    if is_graphical(elem):
        results.append({
            "Tag": tag,
            "DisplayName": display,
            "Path": node_path,
            "Depth": depth,
            "Attributes": elem.attrib
        })

    for child in elem:
        results.extend(extract_visuals(child, depth + 1, node_path))
    return results

def main(xaml_path):
    """
    Main function that parses the XAML file and prints graphical activities.

    Args:
        xaml_path (str): Path to the .xaml file.
    """
    tree = ET.parse(xaml_path)
    root = tree.getroot()
    visual_activities = extract_visuals(root)

    for act in visual_activities:
        indent = "  " * act["Depth"]
        name = act["DisplayName"] or "<no DisplayName>"
        print(f"{indent}- [{act['Tag']}] {name} (Path: {act['Path']})")

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python parse_graphical_xaml.py <file.xaml>")
        sys.exit(1)
    main(sys.argv[1])