# ReadFileLinesEnumGenerator
## Overview
This project is a C# source generator designed to automatically generate enum types based on the content of specified text files.
Each enum is created using the lines in the provided files, with minimal normalization applied to ensure invalid identifiers are transformed into valid ones.

## Requirements
.NET 6 or higher

## Project Setup
1. Add a reference to this project by modifying your `.csproj` file accordingly.
2. Specify file path properties in your `.csproj` file for the generator's usage.
    - **Note**: File names must have the suffix defined as `.enum.txt` to be compatible with other source generators which also use this property to determine file selection.
```xml
<Project>
   
    <!-- Add a reference to the source generator project -->
    <ItemGroup>
        <ProjectReference Include="..\ReadFileLinesEnumGenerator\ReadFileLinesEnumGenerator.csproj">
            <!-- Enable this project as an analyzer to run the source generator -->
            <OutputItemType>Analyzer</OutputItemType>
            <!-- Include the analyzer output assembly -->
            <ReferenceOutputAssembly>true</ReferenceOutputAssembly>
        </ProjectReference>
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