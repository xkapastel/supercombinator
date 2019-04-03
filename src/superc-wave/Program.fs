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
open Audio

[<EntryPoint>]
let main argv =
  let db = Database.init()
  let input = stdin.ReadToEnd()
  match Transaction.parse input with
    | Some tx ->
      db.Apply tx
      match Operator.parse "audio" with
        | Some src ->
          match db.BuildFunction src with
            | Ok func ->
              let cfg = { rate = 8000; length = 4; }
              let samples = Audio.render cfg func
              let stream = Console.OpenStandardOutput()
              eprintfn "Rendering..."
              stream.Write(samples, 0, Array.length samples)
              0
            | Error err ->
              eprintfn "ERROR: %A" err
              1
        | None ->
          eprintfn "ERROR: Couldn't build audio"
          1
    | None ->
      eprintfn "ERROR: Invalid transaction"
      1
