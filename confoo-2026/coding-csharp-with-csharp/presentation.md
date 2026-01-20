---
marp: true
title: Coding C# with C#
paginate: true
theme: default
author: AL Rodriguez
footer: '@ProgrammerAL and programmerAL.com'
---

# Coding C# with C#

with AL Rodriguez

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# About Me (AL Rodriguez)

- @ProgrammerAL
- https://ProgrammerAL.com
- Customer Success Engineer at Duende

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Why are we here?

- Create C# analyzer with C#
- Generate C# code with C#
- Everything Mentioned is Free

---

# History Recap: C# Compiler

* 2000: Compiler written in C++
  - Big-Bang features added each year
  - Tech debt added over time
* 2011: Roslyn Compiler released
  - Full rewrite in C# with knowledge of how C# is used
  - Added a compiler API

---

# Rosyln Analyzer

- Keyword: Analyzer
- Checks code for rules
  - Errors, Warnings, Suggestions, etc
- Reads code syntax
  - Not at all like reflection

---

# Are they used often? Yes!

- Code analysis built-in is all Roslyn Analyzers
  - https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview
- Many 3rd Party Analyzer NuGet packages
  - SonarAnalyzer.CSharp
  - Meziantou.Analyzer
  - Roslynator.Analyzers
  - StyleCop.Analyzers
  - SerilogAnalyzer
  - xunit.analyzers
  - MongoDB.Analyzer

---

# Helpful Tools: Roslyn Syntax Visualization

- https://sharplab.io
- https://www.linqpad.net
- Extension in Visual Studio: https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/syntax-visualizer

---

# Demo Time

---

# Extra Credit: Roslyn Analyzer Code Fix

- Analyzers can edit code to comply with the rule
- Analyzer generates the code change, user approves it

---

# Source Code Generation: T4 Templates

- Generates files at compile time
- Run when project is compiled
- Difficult to share with people
- Not recommended anymore

---

# C# Source Generator

- Code created in-memory at compile time
  - Can write to files if flag enabled in project, all or nothing
- Additive only, cannot modify code
- Same syntax for recognizing code

---

# Are they used often? Yes!

- Code analysis built-in is all Roslyn Analyzers
- Many 3rd Party Analyzer NuGet packages
- .NET Team adding them for AoT support, or just to remove reflection

---

# Useful Source Generator Links

- Cookbook: https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.cookbook.md
- Andrew Lock's blog series: https://andrewlock.net/series/creating-a-source-generator

---

# 2 Types of Source Generators

- Incremental
- 

---

# Demo Time

---

# Review

- Add custom hooks to compiler
  - Roslyn Analyzers to check code
  - Source Generators to add code
- API is specific to parsing code tree

---

![bg 70%](presentation-images/presentation_link_qrcode.png)
![bg 70%](presentation-images/sessionize-feedback.png)
