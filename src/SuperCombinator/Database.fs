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

module Database =
  type private Db() =
    interface IDatabase with
      member self.Apply src =
        None

      member self.Quote () =
        ""

      member self.BuildSound src =
        Error <| TodoError "Db#BuildSound"

      member self.BuildImage src =
        Error <| TodoError "Db#BuildImage"

      member self.BuildModel src =
        Error <| TodoError "Db#BuildModel"

  let init (): IDatabase =
    Db() :> IDatabase
