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

namespace SuperCombinator.Lang

module Runtime =
  type Type =
    | VoidT
    | UnitT
    | RealT
    | DisjT of (Type * Type)
    | ConjT of (Type * Type)

  type Function =
    | UnitiF
    | UniteF
    | FstF
    | SndF
    | CopyF
    | VoidiF
    | VoideF
    | InlF
    | InrF
    | JoinF
    | MinF
    | MaxF
    | AddF
    | NegF
    | MulF
    | InvF
    | ExpF
    | LogF
    | SeqF of (Function * Function)
    | DisjF of (Function * Function)
    | ConjF of (Function * Function)

  type Value =
    | UnitV
    | RealV of Value
    | LeftV of Value
    | RightV of Value
    | PairV of (Value * Value)

  type Operator =
    | PushUnitiF
    | PushUniteF
    | PushFstF
    | PushSndF
    | PushCopyF
    | PushVoidiF
    | PushVoideF
    | PushInlF
    | PushInrF
    | PushJoinF
    | PushVoidT
    | PushUnitT
    | PushRealT
    | ConsSeqF
    | ConsDisjF
    | ConsConjF
    | ConsDisjT
    | ConsConjT
    | ExecVar of string

  type Object =
    | TypeObject of Type
    | FunctionObject of Function
    | ValueObject of Value

  let exec (code: Operator list) (data: Object list): Result<Object list, Fault> =
    printfn "exec: [%A] [%A]" code data
    let mutable code: Operator list = code
    let mutable stack: Object list = data
    let mutable fault: Fault option = None

    let crash (err: Fault) =
      fault <- Some err

    let pushT (typ: Type) =
      stack <- TypeObject typ :: stack

    let pushF (func: Function) =
      stack <- FunctionObject func :: stack

    let consF2 (map: Function * Function -> Function) =
      match stack with
        | (FunctionObject fst) :: (FunctionObject snd) :: rest ->
          let dst = map(fst, snd)
          stack <- FunctionObject dst :: rest
        | _ ->
          crash <| Misc "consF2 expected two function objects"

    let consT2 (map: Type * Type -> Type) =
      match stack with
        | (TypeObject fst) :: (TypeObject snd) :: rest ->
          let dst = map(fst, snd)
          stack <- TypeObject dst :: rest
        | _ ->
          crash <| Misc "consT2 expected two type objects"

    while Option.isNone fault && not(List.isEmpty code) do
      let operator = List.head code
      code <- List.tail code
      match operator with
        | PushVoidT  -> pushT VoidT
        | PushUnitT  -> pushT UnitT
        | PushRealT  -> pushT RealT
        | PushVoidiF -> pushF VoidiF
        | PushVoideF -> pushF VoideF
        | PushInlF   -> pushF InlF
        | PushInrF   -> pushF InrF
        | PushJoinF  -> pushF JoinF
        | PushUnitiF -> pushF UnitiF
        | PushUniteF -> pushF UniteF
        | PushFstF   -> pushF FstF
        | PushSndF   -> pushF SndF
        | PushCopyF  -> pushF CopyF
        | ConsDisjT  -> consT2 DisjT
        | ConsConjT  -> consT2 ConjT
        | ConsSeqF   -> consF2 SeqF
        | ConsDisjF  -> consF2 DisjF
        | ConsConjF  -> consF2 ConjF
        | _          -> crash Todo
    Option.errorOr stack fault

  let eval (func: Function) (value: Value): Result<Value, Fault> =
    match func with
      | UnitiF -> Ok <| PairV (value, UnitV)
      | UniteF ->
        match value with
          | PairV (fst, UnitV) ->
            Ok fst
          | _ ->
            Error <| Misc "UniteF tag error"
      | _ -> Error Todo

  let infer (func: Function): Result<Type, Fault> =
    Error Todo
