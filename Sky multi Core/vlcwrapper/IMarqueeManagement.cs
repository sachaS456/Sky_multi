/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

namespace Sky_multi_Core.VlcWrapper
{
    public interface IMarqueeManagement
    {
        bool Enabled { get; set; }
        int Color { get; set; }
        string Text { get; set; }
        int Opacity { get; set; }
        int Position { get; set; }
        int Refresh { get; set; }
        int Size { get; set; }
        int Timeout { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
