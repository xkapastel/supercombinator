# SuperCombinator
<a href="https://liberapay.com/xkapastel/donate"><img alt="Donate using Liberapay" src="https://liberapay.com/assets/widgets/donate.svg"></a> [![ko-fi](https://www.ko-fi.com/img/donate_sm.png)](https://ko-fi.com/T6T5QRUW)

SuperCombinator is an environment for multimedia synthesis: developers
create text, audio and video content with functional programming and
machine learning.

- [Getting Started](#getting-started)
- [SuperC](#superc)
- [Waveforms](#waveforms)
- [Pixel Fields](#pixel-fields)
- [Distance Fields](#distance-fields)
- [Neural Grammars](#neural-grammars)
- [Related Projects](#related-projects)

## Getting Started
SuperCombinator's primary abstraction is a *database* of
bytecode. This is a key-value store, where the keys are symbols and
the values are sequences of operations that manipulate objects
(e.g. types, functions and values). Developers use *patches*, lists of
transactions, to modify the state of the database. Patches are encoded
as text, one transaction per line:

* `+foo bar baz qux...` is a transaction that defines the key `foo`
  with value `bar baz qux...`.

* `-foo` is a transaction that deletes the key `foo`.

SuperCombinator uses Unix-style filters to create multimedia. All of
these filters receive a patch on standard input, and a distinguished
key as a command line argument:

* `superc-wave` expects the distinguished key to build a function of
  type `Real -> Real`, i.e. a function from time to sample
  intensity. It uses this function to write a stream of raw PCM data
  to standard output.

* `superc-pixel` expects the distinguished key to build a function of
  type `R^2 -> R^4`, i.e. a function from two dimensional space to
  RGBA color space. It uses this function to write a stream of raw
  RGBA pixel data to standard output.

## SuperC
SuperC is the programming language that powers SuperCombinator. It's a
concatenative language: words denote operators, and juxtaposition is
sequential composition.

SuperC is nonstandard in the sense that if you want to solve some
problem with a program `A`, you do not write it down; instead, you
write a program that builds `A`. In this sense, SuperC programs are
really *metaprograms* that build other programs.

## Waveforms

## Pixel Fields
A pixel field is a map from two dimensional space to a colorspace,
typically RGBA.

## Distance Fields
A distance field is a map from a coordinate space to a coordinate's
distance from the surface of some shape. They can be 2D or 3D, so
e.g. a 3D distance field is a map from three dimensional space to a
coordinate's distance from the surface of a 3D shape.

## Neural Grammars

## Related Projects
SuperCombinator gets its name from SuperCollider, and also the PLT
concept of "supercombinators", a compilation technique for functional
programs.

IBNIZ was a big influence on this project.
