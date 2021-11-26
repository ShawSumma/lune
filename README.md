# Lune
Small language that will eventually compile to C, or maybe machine code.

Progress:
- [x] Lexing
- [ ] Parsing
- [ ] AST generation

## Hello World
```nim
var message : str = "Hello world"

proc main() {
    write(message, "\n")
}
```

## Syntax Highlighting
A *very* work in progress VS Code extension is provided under `tool/lune-vscode` which supports
- basic syntax highlighting
- commenting using `Ctrl+/` like any other language
- smart brace folding

## Running
```sh
% dotnet run
```
