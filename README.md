# MarkdownBuilder

## How Use It

The command line will search in the folder for a "index.md" file who will be used as base.

To include files into it, use the following pattern :
```
#my_file#
```
The command line will then search for a "my_file.md" file into the dedicated folder (root + subfolder).

At the end, a new markdown file will be generated will all the includes completed.

## How Install

Process :
1. Build the project to get the exe file or unzip the "Release" folder
2. Put the exe file in a dedicated folder
3. Register the exe file path in your environment variables
4. You succeed if the following command pass
```
MarkdownBuilder --help
```

## Commands
A simple command line to merge the content of multiple markdown files in one output file.
Options :
  *--directory* : The base directory where the command line will search the markdown files. Default : The directory where the command line is executed.
  *--subFolder* : The sub folder where the annex markdown files are stored. Default: Same level as *directory*
  *--output* : The output file name generated when markdown files are merged. Default : "output.md"
  
  
