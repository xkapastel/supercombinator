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

open Lang
open Norm

module Waveform =
  type RenderOptions =
    { rate: int
      length: int }

  type private Wave =
    | Wave of Function
    interface IWaveform with
      member self.Sample time =
        match self with
          | Wave func ->
            match Norm.eval func <| Real time with
              | Ok (Real sample) ->
                sample
              | _ ->
                printfn "Waveform.fs: WARNING: type error"
                0.0

  let build (func: Function): Result<IWaveform, DbError> =
    Ok (Wave func :> IWaveform)

  let render (cfg: RenderOptions) (wave: IWaveform): byte[] =
    let numSamples = cfg.rate * cfg.length
    let buf: byte[] = Array.create numSamples (byte 0)
    for i in 1..numSamples do
      let time: float = (float i) / (float cfg.rate)
      buf.[i] <- byte(255.0 * wave.Sample time)
    buf
