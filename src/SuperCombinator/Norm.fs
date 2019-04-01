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

module Norm =
  let exec (env: Word -> Operator list option) (src: Operator list) (data: Object list): Result<Object list, DbError> =
    Error <| TodoError "Norm.exec"

  let eval (func: Function) (value: Value): Result<Value, DbError> =
    Error <| TodoError "Norm.eval"

  let infer (func: Function): Result<Type, DbError> =
    Error <| TodoError "Norm.infer"
