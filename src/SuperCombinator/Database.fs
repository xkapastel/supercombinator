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

open System
open System.IO

open Lang
open Norm

module Database =
  type private Db() =
    let mutable data: Map<string, Operator list> = Map.ofSeq []

    interface IDatabase with
      member self.Apply src =
        let mutable lines: string list = String.split "\n" src
        let mutable error: DbError option = None
        while Option.isNone error && not(List.isEmpty lines) do
          let line = List.head lines
          lines <- List.tail lines
          match Transaction.parse line with
            | Some (Insert ((Ident name), value)) ->
              data <- Map.add name value data
            | Some (Delete (Ident name)) ->
              data <- Map.remove name data
            | None ->
              error <- Some <| ParseError line
        error

      member self.Quote () =
        let step (state: string list) (key: string) (value: Operator list): string list =
          let body = Operator.quote value
          let line = "+" + key + " " + body
          line :: state

        String.join "\n" <| Map.fold step [] data

      member self.BuildWaveform src =
        Error <| TodoError "Db#BuildWaveform"

  let init (): IDatabase =
    Db() :> IDatabase
