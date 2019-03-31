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

[<AutoOpen>]
module Library =
  type Point2D = Point2D of (float * float)
  type Point3D = Point3D of (float * float * float)
  type RGBA = RGBA of (float * float * float * float)

  type Fault =
    | Todo
    | Misc of string

  type ISound =
    abstract member Sample: float -> float

  type IImage =
    abstract member Pixel: Point2D -> RGBA

  type IModel =
    abstract member Distance: Point3D -> float

  type IContainer =
    abstract member Apply: string -> Result<unit, Fault>
    abstract member Quote: unit -> string
    abstract member BuildSound: string -> Result<ISound, Fault>
    abstract member BuildImage: string -> Result<IImage, Fault>
    abstract member BuildModel: string -> Result<IModel, Fault>
