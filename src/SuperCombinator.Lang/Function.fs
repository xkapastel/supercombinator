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

open System

module Function =
  let parse (src: string): Result<Function, ParseError> =
    let mutable words: string list = String.split " " src
    let mutable stack: Function list = []
    let mutable error: ParseError option = None
    while Option.isNone error && not(List.isEmpty words) do
      let word = List.head words
      words <- List.tail words
      match word with
        | "%uniti" ->
          stack <- Uniti :: stack
        | "%unite" ->
          stack <- Unite :: stack
        | "%fst" ->
          stack <- Fst :: stack
        | "%snd" ->
          stack <- Snd :: stack
        | "%copy" ->
          stack <- Copy :: stack
        | "%voidi" ->
          stack <- Voidi :: stack
        | "%voide" ->
          stack <- Voide :: stack
        | "%inl" ->
          stack <- Inl :: stack
        | "%inr" ->
          stack <- Inr :: stack
        | "%join" ->
          stack <- Join :: stack
        | "%seq" ->
          match stack with
            | fst :: snd :: rest ->
              stack <- Seq (fst, snd) :: rest
            | _ ->
              error <- Some Misc
        | "%conj" ->
          match stack with
            | fst :: snd :: rest ->
              stack <- Conj (fst, snd) :: rest
            | _ ->
              error <- Some Misc
        | "%disj" ->
          match stack with
            | fst :: snd :: rest ->
              stack <- Disj (fst, snd) :: rest
            | _ ->
              error <- Some Misc
        | _ ->
          error <- Some Misc
    match error with
      | Some err ->
        Error err
      | None ->
        match stack with
          | [func] ->
            Ok func
          | _ ->
            Error Misc

  let rec quote (func: Function): string =
    match func with
      | Uniti -> "%uniti"
      | Unite -> "%unite"
      | Fst   -> "%fst"
      | Snd   -> "%snd"
      | Copy  -> "%copy"
      | Voidi -> "%voidi"
      | Voide -> "%voide"
      | Inl   -> "%inl"
      | Inr   -> "%inr"
      | Join  -> "%join"
      | Seq (fst, snd) ->
        String.concat " " [(quote fst); (quote snd); "%seq"]
      | Conj (fst, snd) ->
        String.concat " " [(quote fst); (quote snd); "%conj"]
      | Disj (fst, snd) ->
        String.concat " " [(quote fst); (quote snd); "%disj"]

  let rec eval (func: Function) (value: Value): Result<Value, RuntimeError> =
    match func with
      | Uniti ->
        Ok <| Pair (value, Unit)
      | Unite ->
        match value with
          | Pair (fst, Unit) ->
            Ok fst
          | _ ->
            Error RuntimeError.Type
      | Fst ->
        match value with
          | Pair (fst, _) ->
            Ok fst
          | _ ->
            Error RuntimeError.Type
      | Snd ->
        match value with
          | Pair (_, snd) ->
            Ok snd
          | _ ->
            Error RuntimeError.Type
      | Copy ->
        Ok <| Pair (value, value)
      | Voidi ->
        Ok <| Left value
      | Voide ->
        match value with
          | Left body ->
            Ok body
          | _ ->
            Error RuntimeError.Type
      | Inl ->
        Ok <| Left value
      | Inr ->
        Ok <| Right value
      | Join ->
        match value with
          | Left body ->
            Ok body
          | Right body ->
            Ok body
          | _ ->
            Error RuntimeError.Type
      | Seq (lhs, rhs) ->
        Result.bind (eval rhs) <| eval lhs value
      | Conj (lhs, rhs) ->
        match value with
          | Pair (fst, snd) ->
            Result.zip Pair (eval lhs fst) (eval rhs fst)
          | _ ->
            Error RuntimeError.Type
      | Disj (lhs, rhs) ->
        match value with
          | Left body ->
            Result.map Left <| eval lhs body
          | Right body ->
            Result.map Right <| eval rhs body
          | _ ->
            Error RuntimeError.Type
