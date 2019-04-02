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

open System.Text.RegularExpressions

module Lang =
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
    | ConsSumT
    | ConsProductT
    | ConsSeqF
    | ConsPlusF
    | ConsStarF
    | Variable of Ident

  type Object =
    | TypeObject of Type
    | FunctionObject of Function
    | ValueObject of Value

  type Transaction =
    | Insert of (Ident * Operator list)
    | Delete of Ident

  module Ident =
    let parse (src: string): Ident option =
      if Regex.IsMatch(src, "^[a-z]+$") then
        Some <| Ident src
      else
        None

    let quote (ident: Ident): string =
      match ident with
        | Ident value -> value

  module Operator =
    let parse (src: string): Operator list option =
      let token2op (token: string): Operator option =
        match token with
          | "%void"    -> Some PushVoidT
          | "%unit"    -> Some PushUnitT
          | "%real"    -> Some PushRealT
          | "%voidi"   -> Some PushVoidiF
          | "%voide"   -> Some PushVoideF
          | "%inl"     -> Some PushInlF
          | "%inr"     -> Some PushInrF
          | "%join"    -> Some PushJoinF
          | "%uniti"   -> Some PushUnitiF
          | "%unite"   -> Some PushUniteF
          | "%fst"     -> Some PushFstF
          | "%snd"     -> Some PushSndF
          | "%copy"    -> Some PushCopyF
          | "%min"     -> Some PushMinF
          | "%max"     -> Some PushMaxF
          | "%add"     -> Some PushAddF
          | "%neg"     -> Some PushNegF
          | "%mul"     -> Some PushMulF
          | "%inv"     -> Some PushInvF
          | "%exp"     -> Some PushExpF
          | "%log"     -> Some PushLogF
          | "%cos"     -> Some PushCosF
          | "%sin"     -> Some PushSinF
          | "%sum"     -> Some ConsSumT
          | "%product" -> Some ConsProductT
          | "%seq"     -> Some ConsSeqF
          | "%plus"    -> Some ConsPlusF
          | "%star"    -> Some ConsStarF
          | _          -> Option.map Variable (Ident.parse token)
      src
      |> String.split " "
      |> List.map token2op
      |> Option.all

    let quote (src: Operator list): string =
      let op2token = function
        | PushVoidT     -> "%void"
        | PushUnitT     -> "%unit"
        | PushRealT     -> "%real"
        | PushVoidiF    -> "%voidi"
        | PushVoideF    -> "%voide"
        | PushInlF      -> "%inl"
        | PushInrF      -> "%inr"
        | PushJoinF     -> "%join"
        | PushUnitiF    -> "%uniti"
        | PushUniteF    -> "%unite"
        | PushFstF      -> "%fst"
        | PushSndF      -> "%snd"
        | PushCopyF     -> "%copy"
        | PushMinF      -> "%min"
        | PushMaxF      -> "%max"
        | PushAddF      -> "%add"
        | PushNegF      -> "%neg"
        | PushMulF      -> "%mul"
        | PushInvF      -> "%inv"
        | PushExpF      -> "%exp"
        | PushLogF      -> "%log"
        | PushCosF      -> "%cos"
        | PushSinF      -> "%sin"
        | ConsSumT      -> "%sum"
        | ConsProductT  -> "%product"
        | ConsSeqF      -> "%seq"
        | ConsPlusF     -> "%plus"
        | ConsStarF     -> "%star"
        | Variable name -> Ident.quote name

      src |> List.map op2token |> String.join " "

  module Transaction =
    let parse (src: string): Transaction option =
      if String.startsWith "+" src then
        let tokens = String.splitN " " 2 src
        eprintfn "Transaction.parse: tokens: %A" tokens
        let header = String.tail <| List.item 0 tokens
        let body = List.item 1 tokens
        match Ident.parse header with
          | Some ident ->
            match Operator.parse body with
              | Some code ->
                Some <| Insert (ident, code)
              | None ->
                None
          | _ ->
            None
      elif String.startsWith "-" src then
        match Ident.parse <| String.tail src with
          | Some ident ->
            Some <| Delete ident
          | None ->
            None
      else
        None

    let quote (tx: Transaction): string =
      match tx with
        | Insert (key, value) ->
          let key' = Ident.quote key
          let value' = Operator.quote value
          "+" + key' + " " + value'
        | Delete key ->
          let key' = Ident.quote key
          "-" + key'
