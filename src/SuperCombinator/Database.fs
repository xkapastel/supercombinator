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

open Norm

module Database =
  type private Db() =
    let mutable data: Map<string, Operator list> = Map.ofSeq []

    interface IDatabase with
      member self.Apply xs =
        for transaction in xs do
          match transaction with
            | Insert ((Ident name), value) ->
              data <- Map.add name value data
            | Delete (Ident name) ->
              data <- Map.remove name data

      member self.Quote () =
        let step state key value =
          Insert ((Ident key), value) :: state
        Map.fold step [] data

      member self.BuildFunction src =
        let env name = None
        match Norm.exec env src [] with
          | Ok [FunctionObject func] ->
            Ok func
          | Error err ->
            Error err
          | _ ->
            Error <| TypeError "IDatabase#BuildFunction"

      member self.BuildWaveform src =
        Error <| TodoError "Db#BuildWaveform"

  let init (): IDatabase =
    Db() :> IDatabase
