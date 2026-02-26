---
marp: true
title: Coding C# with C#
paginate: true
theme: default
author: AL Rodriguez
footer: '@ProgrammerAL at programmerAL.com'
---

<style>
section::before {
  /* display: block; */
  content: url('https://raw.githubusercontent.com/ProgrammerAL/Presentations-2026/main/confoo-2026/coding-csharp-with-csharp/presentation-images/duende-logo.png');  width: 10px;
  height: 10px;

  position: absolute;
  right: 60px;
  bottom: -5px;
}
</style>

# Coding C# with C#

with AL Rodriguez

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# About Me

- @ProgrammerAL
- https://ProgrammerAL.com
- Customer Success Engineer at Duende

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Why are we here? To Review!

- C# analyzers
- C# source code generation
- How these are already built into C#
* Note: Everything Mentioned is Free

---

# History Recap: C# Compiler

* 2000: Compiler written in C++
  - Big-Bang features added each update
  - Tech debt added over time
* 2011: Roslyn Compiler released
  - Full rewrite in C#
    - With knowledge of how C# is used
  - Added hooks into the compilation process

---

# Roslyn Syntax Tree

![90%](presentation-images/walkthrough-csharp-syntax-figure.png)

---

# Helpful Tools: Roslyn Syntax Visualization

- https://sharplab.io
- Built into Rider: Syntax Tree Visualizer
- Extension in Visual Studio: https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/syntax-visualizer

---

# Rosyln Analyzer

- Keyword: Analyzer
- Checks code for rules
  - Errors, Warnings, Suggestions, etc
- Reads code syntax using syntax tree

---

# Are they used often? Yes!

- Code analysis built-in is all Roslyn Analyzers
  - https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview
- Many 3rd Party Analyzer NuGet packages
  - SonarAnalyzer.CSharp
  - Roslynator.Analyzers
  - StyleCop.Analyzers
  - SerilogAnalyzer
  - xunit.analyzers
  - MongoDB.Analyzer

---

# Demo Time

- Existing Analyzer:
  - https://github.com/ProgrammerAL/required-auth.analyzer
- Scenario:
  - Require `[Authorize]`/`[Anonymous]` attribute in controller files

---

# Extra Credit: Roslyn Analyzer Code Fix

- Analyzers can edit code to comply with the rule
- Analyzer generates the code change, user approves it

---

# C# Source Generator

- Code created in-memory at compile time
  - Can write to files if flag enabled in project, all or nothing
- Written using same Roslyn Syntax Tree API as Analyzers
- Additive only, cannot modify code

---

# Are they used often? Yes!

- .NET Team adding them for AoT support, or to remove reflection, for performance
- Public List:
  - https://github.com/amis92/csharp-source-generators

---

# Useful Source Generator Links

- Cookbook: https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.cookbook.md
- Andrew Lock's blog series: https://andrewlock.net/series/creating-a-source-generator

---

# 2 Types of Source Generators

- Incremental
  - Always do this one
  - Added in .NET 6
  - v2 of the API
- Non-Incremental
  - Added in .NET 5
  - v1 of the API

---

# Demo Time

- Existing Generator:
  - https://github.com/ProgrammerAL/public-interface-generator
- Scenario:
  - Generate interface code from a class
  - Only use it for internal interfaces needed for unit tests

---

# Review

- Add custom hooks to compiler
  - Roslyn Analyzers to check code
  - Source Generators to add code
- API is specific to parsing code tree

---

<style>
  /* use on containing element to display images side by side */
  .inline-images {
      display: flex;
      height: 90%; /* control location on y-axis */
      justify-content: space-between;
      align-items: center;
  }

  .inline-text {
      display: flex;
      justify-content: space-between;
      align-items: center;
      color: var(--h1-color);
      font-size: 1.6em;
      font-weight: var(--base-text-weight-semibold, 600);
  }
</style>

<div class="inline-text">
  <p>Content</p>
  <p>Feedback</p>
</div>

<div class="inline-images">
  <div>

![w:500](presentation-images/presentation_link_qrcode.png)

  </div>
  <div>

![w:500](presentation-images/feedback-qr.png)

  </div>
</div>
