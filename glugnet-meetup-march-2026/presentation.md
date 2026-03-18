---
marp: true
title: Avoiding null! in C#
paginate: true
theme: default
author: Al Rodriguez
footer: '@ProgrammerAL at programmerAL.com'
---

<style>
section::before {
  /* display: block; */
  content: url('https://raw.githubusercontent.com/ProgrammerAL/Presentations-2026/main/common-images/duende-logo.png');
  width: 10px;
  height: 10px;

  position: absolute;
  right: 60px;
  bottom: -5px;
}
</style>

# Avoiding null! in C#

with AL Rodriguez

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Shameless Self Promotion

- @ProgrammerAL and https://ProgrammerAL.com
- Customer Success Engineer at Duende
- Freelance Affiliate
  - globalGlob(**/*) aka https://globalGlob.dev

![bg right 80%](presentation-images/presentation_link_qrcode.png)

---

# Why are we here?

- Talk about null handling in C#
- Enable Nullable Reference Types (NRT)

---

# Null

- Nothing, no value, a default
- Classes in C# are null by default
- Wrap `if()` checks around code to ensure not null

---
```csharp
private string Combine(string a, string b, string c)
{
  var aShort = a.Substring(0, 2);
  var bShort = b.Substring(0, 2);
  var cShort = c.Substring(0, 2);

  return $"{aShort} {bShort} -- {cShort}";
}
```
---
```csharp
private string Combine(string a, string b, string c)
{
  if(a != null && b != null && c != null)
  {
    var aShort = a.Substring(0, 2);
    var bShort = b.Substring(0, 2);
    var cShort = c.Substring(0, 2);

    return $"{aShort} {bShort} -- {cShort}";
  }

  return "";
}
```
---
```csharp
public string FormatStrings(string a, string b, string c)
{
  if(a != null && b != null && c != null)
  {
    return Combine(a, b, c);
  }

  return "";
}

private string Combine(string a, string b, string c)
{
  var aShort = a.Substring(0, 2);
  var bShort = b.Substring(0, 2);
  var cShort = c.Substring(0, 2);

  return $"{aShort} {bShort} -- {cShort}";
}
```
---
```csharp
public string FormatStrings(string a, string b, string c)
{
  if(a != null && b != null && c != null)
  {
    return Combine(a, b, c);
  }

  return "";
}

public string DefaultCombine(string a, string b)
{
    return Combine(a, b, "Combined");
}

private string Combine(string a, string b, string c)
{
  var aShort = a.Substring(0, 2);
  var bShort = b.Substring(0, 2);
  var cShort = c.Substring(0, 2);

  return $"{aShort} {bShort} -- {cShort}";
}
```
---
# Nullable Reference Types (NRTs)

- Introduced in .NET 5 (2019!)
- **Warning**: Early on, More thinking for developer (Shift Left)

---

# What are NRTs?

- Nullable Reference Types
- Compiler check for null value at Compile Time
- Compiler Warning when compiler thinks something __*can be*__ null

```xml
<PropertyGroup>
  <Nullable>enable</Nullable>
<PropertyGroup>
```

---

# More on NRTs

- NO CHANGE IN FUNCTIONALITY
- Added syntax for developers
- More thinking ahead of time

---

# Using NRTs

- Adds syntax for null mapping (`!` and `?`)
- Adds attributes for compiler hints

---

# Using `?` with NRTS

- `?` means a variable May Be Null
- Excluding `?` means it is assumed to be Not Null

* `string? a = null;`
* `string? b = "abc";`
* `string c = "abc";`
* `string d = null;//Compiler Warning`

---

# Using `!` with NRTs

- `!` tells compiler to ignore a definite/possible null
- Should be a last resort

* `string a = null;//Compiler Warning`
* `string b = null!;`

---

```csharp
public string FormatStrings(string? a, string? b, string? c)
{
  if(a != null && b != null && c != null)
  {
    return Combine(a, b, c);
  }

  return "";
}

public string DefaultCombine(string? a, string? b)
{
    //Compiler Warnings
    return Combine(a, b, "Combined");
}

public string LyingDefaultCombine(string? a, string? b)
{
    //No Warnings
    return Combine(a!, b!, "Combined");
}

private string Combine(string a, string b, string c)
{
  var aShort = a.Substring(0, 2);
  var bShort = b.Substring(0, 2);
  var cShort = c.Substring(0, 2);

  return $"{aShort} {bShort} -- {cShort}";
}
```
---

# Attributes

- 11 Attributes (by last count)
- Helpers to tell the compiler what to do in situations
  - Helpful when the code isn't clear
- Full List: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/nullable-analysis

---

# Try Pattern

```csharp
if(TryParseNumber("abc-123"), out int? myNumber)
{
  //Compiler Warning
  Console.WriteLine(myNumber);
}

public bool TryParseNumber(string text, out int? number)
{
  ...
}
```

---

# \[NotNullWhen\]

```csharp
public bool TryParseNumber(string text, [NotNullWhen(true)] out int? number)
{
  ...
}
```

---

# Even more \[NotNullWhen\]

```csharp
if(TryParseNumber("abc-123"), out int? myNumber)
{
  Console.WriteLine(myNumber);
}

public bool TryParseNumber(string text, [NotNullWhen(true)] int? number)
{
  ...
}
```

---

# \[MemberNotNull\]

```csharp
var person = new Person();
var personDto = LoadPersonInfo();
person.InitPerson(myDto);
Console.WriteLine(person.Name);

public class Person
{
  public Person() {}

  public string? Name { get; private set; }

  [MemberNotNull(nameof(Name))]
  public void InitPerson(PersonDto dto)
  {
    Name = dto.Name;
  }
}
```

---

# Attributes Are Useful

- Full List: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/nullable-analysis

---
# Serialization

- Needs to handle being `new`ed up in code and deserialized
  - Code needs to enforce null rules
  - Attributes for deserializarion
- `required` keyword and `[Required]` attribute are different
  - Keyword is for compile time
  - Attribute is for runtime deserialization
- This means duplicated logic

---

# Demo Time

---

# Check for Null at "Ingress of the App"

- Check all entities coming into your code
  - HTTP Requests
  - External Service Calls
  - Database Calls*
- Require data should be default
  - Not always possible, more thinking now (shift-left)

---

# In Closing - Embrace NRTs

- NRTs find bugs
- A lot of code hassle can be mitigated

---

# Shameless Self Promotion

- @ProgrammerAL and https://ProgrammerAL.com
- Customer Success Engineer at Duende
- Freelance Affiliate
  - globalGlob(**/*) aka https://globalGlob.dev

![bg right 80%](presentation-images/presentation_link_qrcode.png)
