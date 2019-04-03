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

namespace SuperCombinator

[<AutoOpen>]
module Library =
  type Ident = Ident of string

  type Type =
    | Void
    | Unit
    | Real
    | Sum of (Type * Type)
    | Product of (Type * Type)

  type Function =
    | Voidi
    | Voide
    | Inl
    | Inr
    | Join
    | Uniti
    | Unite
    | Fst
    | Snd
    | Copy
    | Min
    | Max
    | Add
    | Neg
    | Mul
    | Inv
    | Exp
    | Log
    | Cos
    | Sin
    | Seq of (Function * Function)
    | Plus of (Function * Function)
    | Star of (Function * Function)

  type Value =
    | Unit
    | Real of float
    | Left of Value
    | Right of Value
    | Pair of (Value * Value)

  type Operator =
    | PushUnitV
    | PushRealV of float
    | PushVoidiF
    | PushVoideF
    | PushInlF
    | PushInrF
    | PushJoinF
    | PushUnitiF
    | PushUniteF
    | PushFstF
    | PushSndF
    | PushCopyF
    | PushMinF
    | PushMaxF
    | PushAddF
    | PushNegF
    | PushMulF
    | PushInvF
    | PushExpF
    | PushLogF
    | PushSinF
    | PushCosF
    | PushVoidT
    | PushUnitT
    | PushRealT
    | ConsLeftV
    | ConsRightV
    | ConsPairV
    | ConsSeqF
    | ConsPlusF
    | ConsStarF
    | ConsSumT
    | ConsProductT
    | Variable of Ident

  type Object =
    | TypeObject of Type
    | FunctionObject of Function
    | ValueObject of Value

  type Transaction =
    | Insert of (Ident * Operator list)
    | Delete of Ident

  type DbError =
    | TodoError of string
    | ParseError of string
    | TypeError of string

  type IWaveform =
    abstract member Sample: float -> float

  type IDatabase =
    abstract member Apply: Transaction list -> unit
    abstract member Quote: unit -> Transaction list
    abstract member BuildFunction: Operator list -> Result<Function, DbError>
    abstract member BuildWaveform: string -> Result<IWaveform, DbError>
