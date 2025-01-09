# ReadFileLinesEnumGenerator
## Overview
This project is a C# source generator designed to automatically generate enum types based on the content of specified text files.
Each enum is created using the lines in the provided files, with minimal normalization applied to ensure invalid identifiers are transformed into valid ones.

## Requirements
.NET 6 or higher

## Project Setup

1. Add a package reference to this project by running `dotnet add package ReadFileLinesEnumGenerator`.
2. Update the properties in your `.csproj` file to enable this project as a source generator.
3. Specify file path properties in your `.csproj` file for the generator to process.
    - **Note**: File names must have the `.enum.txt` suffix to maintain compatibility with other source generators that
      use this property for file selection.
```xml
<Project>
   
    <!-- Add a reference to the source generator project -->
    <ItemGroup>
       <PackageReference Include="ReadFileLinesEnumGenerator" Version="1.0.0">
            <!-- Enable this project as an analyzer to run the source generator -->
            <OutputItemType>Analyzer</OutputItemType>
            <!-- Include the analyzer output assembly -->
            <ReferenceOutputAssembly>true</ReferenceOutputAssembly>
       </PackageReference>
    </ItemGroup>

    <!-- Specify additional files for the generator -->
    <ItemGroup>
        <AdditionalFiles Include="myEnum.enum.txt" />
    </ItemGroup>
   
</Project>
```

## Usage
```csharp
using ReadFileLinesEnumGenerator.Generated;

// The 'myEnum' type is automatically defined using the content of the "myEnum.enum.txt" file
// under the ReadFileLinesEnumGenerator.Generated namespace. 
// Each line in the file is converted into a valid identifier through minimal normalization.
myEnum selectedEnumValue = myEnum.A_0;

// An extension method is also provided to retrieve the original line.
string originalEnumValue = myEnum.A_0.ToStringValue();
```

## Disclaimer
This project has been developed and tested using JetBrains Rider on macOS Sonoma.
Functionality on other environments is not guaranteed.
This project is licensed under the MIT License.