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

module Operator =
  let private parseItem (token: string): Operator option =
    match token with
      | "%unit"    -> Some PushUnitV
      | "%voidi"   -> Some PushVoidiF
      | "%voide"   -> Some PushVoideF
      | "%inl"     -> Some PushInlF
      | "%inr"     -> Some PushInrF
      | "%join"    -> Some PushJoinF
      | "%uniti"   -> Some PushUnitiF
      | "%unite"   -> Some PushUniteF
      | "%fst"     -> Some PushFstF
      | "%snd"     -> Some PushSndF
      | "%copy"    -> Some PushCopyF
      | "%min"     -> Some PushMinF
      | "%max"     -> Some PushMaxF
      | "%add"     -> Some PushAddF
      | "%neg"     -> Some PushNegF
      | "%mul"     -> Some PushMulF
      | "%inv"     -> Some PushInvF
      | "%exp"     -> Some PushExpF
      | "%log"     -> Some PushLogF
      | "%cos"     -> Some PushCosF
      | "%sin"     -> Some PushSinF
      | "%Void"    -> Some PushVoidT
      | "%Unit"    -> Some PushUnitT
      | "%Real"    -> Some PushRealT
      | "%left"    -> Some ConsLeftV
      | "%right"   -> Some ConsRightV
      | "%pair"    -> Some ConsPairV
      | "%seq"     -> Some ConsSeqF
      | "%plus"    -> Some ConsPlusF
      | "%star"    -> Some ConsStarF
      | "%Sum"     -> Some ConsSumT
      | "%Product" -> Some ConsProductT
      | _          -> Option.map Variable (Ident.parse token)

  let private quoteItem = function
    | PushUnitV     -> "%unit"
    | PushRealV u   -> "%todo-real"
    | PushVoidiF    -> "%voidi"
    | PushVoideF    -> "%voide"
    | PushInlF      -> "%inl"
    | PushInrF      -> "%inr"
    | PushJoinF     -> "%join"
    | PushUnitiF    -> "%uniti"
    | PushUniteF    -> "%unite"
    | PushFstF      -> "%fst"
    | PushSndF      -> "%snd"
    | PushCopyF     -> "%copy"
    | PushMinF      -> "%min"
    | PushMaxF      -> "%max"
    | PushAddF      -> "%add"
    | PushNegF      -> "%neg"
    | PushMulF      -> "%mul"
    | PushInvF      -> "%inv"
    | PushExpF      -> "%exp"
    | PushLogF      -> "%log"
    | PushCosF      -> "%cos"
    | PushSinF      -> "%sin"
    | PushVoidT     -> "%Void"
    | PushUnitT     -> "%Unit"
    | PushRealT     -> "%Real"
    | ConsLeftV     -> "%left"
    | ConsRightV    -> "%right"
    | ConsPairV     -> "%pair"
    | ConsSeqF      -> "%seq"
    | ConsPlusF     -> "%plus"
    | ConsStarF     -> "%star"
    | ConsSumT      -> "%Sum"
    | ConsProductT  -> "%Product"
    | Variable name -> Ident.quote name

  let parse (src: string): Operator list option =
    src
    |> String.split " "
    |> List.map parseItem
    |> Option.all

  let quote (src: Operator list): string =
    src
    |> List.map quoteItem
    |> String.join " "
