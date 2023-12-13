## Requirements

A generic config-driven project that can be published and used to generate diffs and extract specific ticket references.

This can logically be broken up into two phases:
> 1. Phase one → Take in a raw.txt file containing commit references and extract the ticket references
> 2. Phase two → Generate the actual raw ticket references using git commands

We will mostly be focusing on Phase One in the rest of this document.

## Limitations and assumptions

###### 1. Dedicated Repos for diffs
For Phase Two we will assume that we have a dedicated set of repositories for the diff generation.  The intention is to ensure that the user's work is not affected by the diff generation logic.  It is possible to enhance this in the future to ensure that there is never any impact on the user's work (including in the scenario of a catastrophic failure)

###### 2. Commits limitation
Our logic is reliant on developers committing accurately.  Any logic relating to extracting and grouping tickets will be based on the given input (commit messages made by devs).  Note we can maybe add an option to the config to flag commit messages without correct references so that these may be dealt with manually (this same logic could be used to track repeating offenders if so desired).

## Technical Design Overview

#### 1. Config-driven solution
###### Overview
The config must decide everything.  This is to ensure that different projects can take the program and modify the config for their specific scenario without having to rewrite the core program functionality.  

The config file can be something simple like JSON and can be outputted to the .exe's location so that it's very easy to modify when the program has been published.

###### Global config
The global config items can include the following (this can be revised and enhanced as needed):
```json
// Generate the diffs for this range (note you can leave these as null, but then you will have to specify it at the repo-level)
"diffRange": {
	// Select EITHER a branch or a tag for the from and the to
	"from": { "branch": null, "tag": "12.0.4"},
	"to": { "branch": "main", "tag": null}
},

// Customisation for the commit ticket reference options
"commitOptions": {
	"captureCommitsWithoutReferences": true,
	"groupReferencesByHeader": false,
},

// The difference commit references to extract
"commitReferences": [
	{
		"header": "Features", // Can be used with commitOptions:groupReferencesByHeader
		"pattern": "(FEAT)-d{3,4}", // Pattern to match it
		"subItems": ["(DEV)-d{3,4}"] // Ticket references that should be grouped under the feature reference
	}
]
```

###### Repository config
The repository-level config that can override the global config.
```json
"repositories": [
	{
		"name": "Web", // Friendly name for the repository (could possibly omit this and just take the computed repo name)
		"path": "C:\\Clients\\Project\\Automation\\Diffs\\Repos\\Web-Repo", // The path to the repository
		"diffRange": {
			// Can optionally set these values here if you need to override the global values
			"from": { "branch": null, "tag": null},
			"to": { "branch": "dev", "tag": null} // In this example only the 'to' branch is overriden
		}
	}
]
```
