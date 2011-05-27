GNU General Public License, version 3 (GPL-3.0). See LICENSE.txt for additional information.

Directory flattener copies all of the files in the source-directory and its subdirectories to the target-directory, 
ignoring any hierarchical folder structure. The initial use case is to bring all eBook files to a single level, so 
they can be easily dragged into apps via iTunes. I'm sure there are other uses.

Directory Flattener - Usage:
	directory-flattener [source-directory] [target-directory]
        directory-flattener [source-directory] [target-directory] [search-pattern]

	target-directory will be created if it does not exist
	search-pattern is standard Windows style expression including wildcards (*)
		only files matching the pattern will be copied.

	source-directory is not modified.