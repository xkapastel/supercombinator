// This file is a part of SuperCombinator.
// Copyright (C) 2019 Matthew Blount

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.

// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public
// License along with this program.  If not, see
// <https://www.gnu.org/licenses/.

namespace SuperCombinator.Lang

[<AutoOpen>]
module Default =
  type Function =
    | Uniti
    | Unite
    | Fst
    | Snd
    | Copy
    | Voidi
    | Voide
    | Inl
    | Inr
    | Join
    | Seq of (Function * Function)
    | Conj of (Function * Function)
    | Disj of (Function * Function)

  type Value =
    | Unit
    | Pair of (Value * Value)
    | Left of Value
    | Right of Value

  type ParseError =
    | Todo
    | Misc

  type RuntimeError =
    | Todo
    | Type
