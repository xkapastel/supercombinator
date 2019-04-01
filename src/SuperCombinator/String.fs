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

module String =
  let inline replace (fst: string) (snd: string) (str: string): string =
    str.Replace(fst, snd)

  let inline split (sep: string) (str: string): string list =
    str.Split(sep, StringSplitOptions.RemoveEmptyEntries)
    |> Array.toList

  let inline splitN (sep: string) (count: int) (str: string): string list =
    str.Split(sep, count, StringSplitOptions.RemoveEmptyEntries)
    |> Array.toList

  let inline startsWith (prefix: string) (str: string): bool =
    str.StartsWith(prefix)

  let inline tail (str: string): string =
    str.Substring(1)

  let inline join (sep: string) (xs: string list): string =
    String.Join(sep, xs)
