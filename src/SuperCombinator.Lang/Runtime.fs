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

module Runtime =
  type Type =
    | Void
    | Unit
    | Real
    | Disj of (Type * Type)
    | Conj of (Type * Type)

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
    | Min
    | Max
    | Add
    | Neg
    | Mul
    | Inv
    | Exp
    | Log
    | Seq of (Function * Function)
    | Disj of (Function * Function)
    | Conj of (Function * Function)

  type Value =
    | Unit
    | Real of Value
    | Left of Value
    | Right of Value
    | Pair of (Value * Value)

  type Operator =
    | PushUnitiF
    | PushUniteF
    | PushFstF
    | PushSndF
    | PushCopyF
    | PushVoidiF
    | PushVoideF
    | PushInlF
    | PushInrF
    | PushJoinF
    | PushVoidT
    | PushUnitT
    | PushRealT
    | ConsDisjT
    | ConsConjT
    | Var of string

  type Object =
    | Otyp of Type
    | Ofun of Function
    | Oval of Value

  let run (code: Operator list) (data: Object list): Result<Object list, Fault> =
    Error Todo

  let eval (func: Function) (value: Value): Result<Value, Fault> =
    Error Todo
