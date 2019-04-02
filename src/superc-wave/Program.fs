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

open System
open System.IO
open SuperCombinator
open Waveform

[<EntryPoint>]
let main argv =
  let db = Database.init()
  let input = stdin.ReadToEnd()
  let mutable lines: string list = String.split " " input
  let mutable error: DbError option = None
  while Option.isNone error && not(List.isEmpty lines) do
    let line = List.head lines
    lines <- List.tail lines
    error <- db.Apply line
  match error with
    | Some error ->
      printfn "ERROR: %A" error
      1
    | None ->
      match db.BuildWaveform "stdout" with
        | Ok wave ->
          let cfg  = { rate = 8000; length = 4 }
          let samples = Waveform.render cfg wave
          let stream = Console.OpenStandardOutput()
          stream.Write(samples, 0, Array.length samples)
          0
        | Error error ->
          printfn "ERROR: %A" error
          1
