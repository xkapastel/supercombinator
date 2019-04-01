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

module Option =
  let okOr (err: 'e) (option: Option<'a>): Result<'a, 'e> =
    match option with
      | Some value ->
        Ok value
      | None ->
        Error err
  
  let errorOr (value: 'a) (option: Option<'e>): Result<'a, 'e> =
    match option with
      | Some err ->
        Error err
      | None ->
        Ok value

  let all (xs: 'a option list): 'a list option =
    let cons (value: 'a option) (xs: 'a list): 'a list option =
      match value with
        | None   -> None
        | Some x -> Some <| x :: xs

    let step (state: 'a list option) (value: 'a option): 'a list option =
      match state with
        | None    -> None
        | Some xs -> cons value xs

    let init = Some []

    List.fold step init <| List.rev xs
