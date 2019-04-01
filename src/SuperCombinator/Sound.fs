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

module Sound =
  type RenderOptions =
    { rate: int
      length: int }

  let build (func: Function): Result<ISound, DbError> =
    Error <| TodoError "Sound.build"

  let render (options: RenderOptions) (sound: ISound): Result<byte[], DbError> =
    Error <| TodoError "Sound.render"
