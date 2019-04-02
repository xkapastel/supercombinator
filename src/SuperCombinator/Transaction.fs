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

module Transaction =
  let private parseItem (src: string): Transaction option =
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

  let private quoteItem (tx: Transaction): string =
    match tx with
      | Insert (key, value) ->
        let key' = Ident.quote key
        let value' = Operator.quote value
        "+" + key' + " " + value'
      | Delete key ->
        let key' = Ident.quote key
        "-" + key'

  let parse (src: string): Transaction list option =
    src
    |> String.split "\n"
    |> List.map parseItem
    |> Option.all

  let quote (xs: Transaction list): string =
    xs
    |> List.map quoteItem
    |> String.join "\n"
