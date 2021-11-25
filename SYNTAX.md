# Lune Syntax Specification (v0.1.0)
Since the language is in its infancy, this is highly subject to change!

#### Comments:
Comments begin with a `#` symbol like in Python, Nim etc.

#### Identifiers:
Identifiers are defined in `camelCase`. They can contain:
- letters
- underscores
- digits (as long as the identifier doesnt start with one)

#### Functions:
```nim
proc addTwo(a : int, b : int) {

}

proc say_hello() {
    write("Hello!")
}
```

#### If and Else:
```nim
if age >= 18 {
    write("You are allowed to vote")
} else {
    write("You are not allowed to vote")
}
```

```nim
case some_value {
    of first { 
        # do something
    }
    of another {
        # do another thing
    }
    else {
        # do something else on fallback
    }
}
```