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

module Container =
  type FileContainer(path: string) =
    interface IContainer with
      member self.Apply src =
        Error Todo

      member self.Quote () =
        ""

      member self.BuildSound src =
        Error Todo

      member self.BuildImage src =
        Error Todo

      member self.BuildModel src =
        Error Todo
  
  let load (name: string): IContainer =
    let mutable data = Environment.GetEnvironmentVariable "XDG_DATA_HOME"
    if isNull data then
      let home = Environment.GetEnvironmentVariable "HOME"
      data <- Path.Combine(home, ".local", "share")
    let superc_root = Path.Combine(data, "supercombinator")
    printfn "Container.load: searching %s" superc_root
    let local = Path.Combine(superc_root, "src", name)
    (FileContainer local) :> IContainer
