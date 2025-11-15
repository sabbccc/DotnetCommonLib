# DotnetCommonLib

A common library collection for .NET projects, providing reusable utilities, helpers and shared core functionality to streamline development across multiple solutions.

## Table of Contents

- [Why this library?](#why-this-library)  
- [Features](#features)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Installation](#installation)  
  - [Usage](#usage)  
- [Project Structure](#project-structure)  
- [Contributing](#contributing)  
- [License](#license)  
- [Contact](#contact)  

## Why this library?

When working on multiple .NET projects, there’s often common functionality (logging, validation, configurations, extension methods, etc.) that gets duplicated across solutions.  
DotnetCommonLib centralises these utilities, enabling you to:

- Keep your codebase DRY (Don’t Repeat Yourself)  
- Maintain consistency across projects  
- Easily leverage and update shared code in one place  

## Features

Some of the utilities you’ll find in this library include:

- Core extension methods for .NET types (strings, collections, tasks, etc)  
- Common validation logic  
- Shared configuration and options handling  
- Logging abstractions (if applicable)  
- Utility helpers for async, error-handling, mapping, etc  

*(You can replace/add specific features according to what’s actually implemented in `Common.Core`.)*

## Getting Started

### Prerequisites

- .NET 8.0 or later (or whichever version you target)  
- Visual Studio / VS Code / JetBrains Rider (or other IDE)  
- Basic familiarity with .NET, C#, NuGet and library referencing  

### Installation

You can add this library into your project in one of two ways:

1. **Project reference**  
   Clone/download the repository and add a project reference to `Common.Core`.  
   ```bash
   git clone https://github.com/sabbccc/DotnetCommonLib.git
   cd DotnetCommonLib

## Project Structure
```
DotnetCommonLib/
├── .vs/                     ← Visual Studio settings (ignore)  
├── Common.Core/            ← Main library project  
│   ├── Common.Core.slnx  
│   ├── src/                 ← Source code  
│   ├── tests/               ← (Optional) Unit tests  
├── .gitignore  
└── README.md                ← This file  
```

`Common.Core` is the central project that houses all shared code.  
Future libraries (e.g., **Common.Data**, **Common.Web**) can be added following the same pattern.

---

## Contributing

Contributions are welcome! Here’s how you can help:

1. **Fork** the repository  
2. **Create a new branch**:
   - `feature/YourFeature`
   - `bugfix/IssueNumber`
3. Make your changes and add appropriate tests (if applicable)  
4. Submit a **pull request** with a clear description of your changes  

Please ensure your code follows existing style guidelines and passes all existing tests.

---

## License

This project is licensed under the **MIT License** — see the  
[MIT License](LICENSE) file for details.

---

## Contact

Maintained by **[sabbccc](https://github.com/sabbccc)**.  
Feel free to open issues or pull requests for improvements, bug fixes, or suggestions.


