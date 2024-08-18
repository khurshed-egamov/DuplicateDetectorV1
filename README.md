# DuplicateDetectorV1

DuplicateDetectorV1 is a console application built with C# that recursively scans a specified directory for duplicate files by computing their MD5 hashes. The results are saved in a text file, listing all duplicate files found.

## Features

- Scans directories recursively for duplicate files
- Uses MD5 hashing to detect duplicates
- Outputs results to a user-specified text file
- Includes a simple loading screen animation while scanning

## Usage

1. Clone or download the repository to your local machine.

2. Open the project in your preferred C# development environment (e.g., Visual Studio).

3. Run the application. You will be prompted to enter the path of the directory you want to scan.

   
Bash

    Enter path: C:\path\to\directory
    
4. The application will search for duplicate files in the specified directory and all its subdirectories.

5. After the scan is complete, you will be prompted to enter a filename to save the results.

   
Bash

    Enter filename to save results (duplicates): results
    
    The results will be saved in a text file named results.txt in the same directory where the application is run.

6. Check the results file for a list of duplicate files detected.

## Example

Here's what the results file might look like:
[08/18/2024 15:30]
C:\path\to\directory\file1.txt
    C:\path\to\directory\subdir\file1_copy.txt
    C:\path\to\directory\subdir2\file1_duplicate.txt

C:\path\to\directory\image1.png
    C:\path\to\directory\subdir\image1_copy.png
## Dependencies

This project uses:

- System.Net
- System.IO
- System.Collections
- System.Security.Cryptography

## Notes

- The application relies on MD5 hashing, which, while fast and efficient, is not collision-resistant. It's suitable for detecting duplicates in this context but may not be secure for other uses.
- The application currently only handles files that can be read and hashed without errors.
